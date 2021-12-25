using BeatSaverSharp.Http;
using BeatSaverSharp.Models;
using BeatSaverSharp.Models.Pages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BeatSaverSharp
{
    /// <summary>
    /// A client for interfacing with the BeatSaver API
    /// </summary>
    public class BeatSaver : IDisposable
    {
        internal bool IsDisposed { get; set; }

        private BeatSaverOptions _options;
        private readonly IHttpService _httpService;
        private readonly object _bLock = new object();
        private readonly object _uLock = new object();
        private static readonly (string, PropertyInfo)[] _filterProperties;
        private readonly ConcurrentDictionary<int, User> _fetchedUsers = new ConcurrentDictionary<int, User>();
        private readonly ConcurrentDictionary<string, User> _fetchedUsernames = new ConcurrentDictionary<string, User>();
        private readonly ConcurrentDictionary<string, Beatmap> _fetchedBeatmaps = new ConcurrentDictionary<string, Beatmap>();
        private readonly ConcurrentDictionary<string, Beatmap> _fetchedHashedBeatmaps = new ConcurrentDictionary<string, Beatmap>();

        static BeatSaver()
        {
            // We cache the reflection properties and their names.
            var properties = typeof(SearchTextFilterOption).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            _filterProperties = new (string, PropertyInfo)[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                var activeProperty = properties[i];
                var queryKeyName = activeProperty.GetCustomAttribute<SearchTextFilterOption.QueryKeyNameAttribute>();
                _filterProperties[i] = (queryKeyName?.Name ?? activeProperty.Name, activeProperty);
            }
        }

        public BeatSaver(BeatSaverOptions beatSaverOptions)
        {
            _options = beatSaverOptions;
            string userAgent = $"{beatSaverOptions.ApplicationName}/{beatSaverOptions.Version}";
#if RELEASE_UNITY
            _httpService = new UnityWebRequestService()
            {
                BaseURL = beatSaverOptions.BeatSaverAPI.ToString(),
                Timeout = beatSaverOptions.Timeout,
                UserAgent = userAgent,
            };
#else
            _httpService = new HttpClientService(beatSaverOptions.BeatSaverAPI.ToString(), beatSaverOptions.Timeout, userAgent);
#endif
        }

        public BeatSaver(string applicationName, Version version) : this(new BeatSaverOptions(applicationName, version))
        {

        }

#region Beatmaps

        public async Task<Beatmap?> Beatmap(string key, CancellationToken token = default, bool skipCacheCheck = false)
        {
            key = key.ToLowerInvariant();
            if (!skipCacheCheck && _fetchedBeatmaps.TryGetValue(key, out Beatmap? beatmap))
                return beatmap;

            return await FetchBeatmap("maps/id/" + key, token).ConfigureAwait(false);
        }

        public async Task<Dictionary<string, Beatmap>> BeatmapByHash(params string[] hash)
        {
            return await BeatmapByHash(default, false, hash);
        }

        public async Task<Dictionary<string, Beatmap>> BeatmapByHash(CancellationToken token, bool skipCacheCheck, params string[] hash)
        {
            var grouped = hash.GroupBy(x => !skipCacheCheck && _fetchedBeatmaps.ContainsKey(x));
            var result = new Dictionary<string, Beatmap>();
            foreach (var grouping in grouped)
            {
                if (grouping.Key)
                {
                    // In cache
                    foreach (var s in grouping)
                    {
                        result.Add(s, _fetchedBeatmaps[s]);
                    }
                }
                else
                {
                    // Maximum 50 hashes per request
                    var chunks = grouping
                        .Select((v, i) => new {v, groupIndex = i / 50})
                        .GroupBy(x => x.groupIndex)
                        .Select(g => g.Select(x => x.v));

                    foreach (var chunk in chunks)
                    {
                        var asList = chunk.ToList();
                        if (asList.Count == 1)
                        {
                            var song = await BeatmapByHash(hash.First(), token);
                            if (song != null)
                            {
                                result.Add(song.LatestVersion.Hash, song);
                            }
                        }
                        else
                        {
                            var newBeatmaps = await FetchBeatmaps("maps/hash/" + string.Join(",", asList), token)
                                .ConfigureAwait(false);
                            if (newBeatmaps == null) continue;
                            foreach (var keyValuePair in newBeatmaps)
                            {
                                result.Add(keyValuePair.Key, keyValuePair.Value);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public async Task<Beatmap?> BeatmapByHash(string hash, CancellationToken token = default, bool skipCacheCheck = false)
        {
            if (string.IsNullOrWhiteSpace(hash))
                return null;
            hash = hash.ToUpperInvariant();

            if (!skipCacheCheck && _fetchedHashedBeatmaps.TryGetValue(hash, out Beatmap? beatmap))
                return beatmap;
            return await FetchBeatmap("maps/hash/" + hash, token).ConfigureAwait(false);
        }

        private async Task<Beatmap?> FetchBeatmap(string url, CancellationToken token = default)
        {
            var response = await _httpService.GetAsync(url, token).ConfigureAwait(false);
            if (!response.Successful)
                return null;

            Beatmap beatmap = await response.ReadAsObjectAsync<Beatmap>().ConfigureAwait(false);
            GetOrAddBeatmapToCache(beatmap, out beatmap);
            return beatmap;
        }

        private async Task<Dictionary<string, Beatmap>?> FetchBeatmaps(string url, CancellationToken token = default)
        {
            var response = await _httpService.GetAsync(url, token).ConfigureAwait(false);
            if (!response.Successful)
                return null;

            Dictionary<string, Beatmap> beatmap = await response.ReadAsObjectAsync<Dictionary<string, Beatmap>>().ConfigureAwait(false);
            foreach (var keyValuePair in beatmap)
            {
                GetOrAddBeatmapToCache(keyValuePair.Value, out _);
            }
            return beatmap;
        }

#endregion

#region Paged Beatmaps

        public async Task<Page?> LatestBeatmaps(UploadedFilterOptions? options = default, CancellationToken token = default)
        {
            if (options == null)
                options = new UploadedFilterOptions(null, false);
            StringBuilder sb = new StringBuilder();
            sb.Append("maps/latest?automapper=");
            sb.Append(options.IncludeAutomappers);
            if (options.StartDate.HasValue)
                sb.Append("&before=").Append(options.StartDate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            if (options.Before.HasValue)
                sb.Append("&after=").Append(options.Before.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            if (options.Sort.HasValue)
            {
                string sort = options.Sort.Value switch
                {
                    LatestFilterSort.CREATED => "CREATED",
                    LatestFilterSort.UPDATED => "UPDATED",
                    LatestFilterSort.LAST_PUBLISHED => "LAST_PUBLISHED",
                    _ => "FIRST_PUBLISHED",
                };
                sb.Append("&sort=").Append(sort);
            }

            var result = await GetBeatmapsFromPage(sb.ToString(), token).ConfigureAwait(false);
            if (result is null)
                return null;

            return new UploadedPage(options, result)
            {
                Client = this
            };
        }

        public async Task<Page?> UploaderBeatmaps(int uploaderID, int page = 0, CancellationToken token = default)
        {
            var result = await GetBeatmapsFromPage($"maps/uploader/{uploaderID}/{page}", token).ConfigureAwait(false);
            if (result is null)
                return null;

            return new UploaderPage(page, uploaderID, result)
            {
                Client = this
            };
        }

        public async Task<Page?> SearchBeatmaps(SearchTextFilterOption? searchOptions = default, int page = 0, CancellationToken token = default)
        {
            string searchURL = $"search/text/{page}";

            if (searchOptions != null)
            {
                List<string> queryProps = new List<string>();
                foreach (var property in _filterProperties)
                {
                    var filterValue = property.Item2.GetValue(searchOptions);
                    if (filterValue != null)
                    {
                        // DateTimes need to be formatted to conform to ISO
                        if (filterValue is DateTime dateTime)
                            filterValue = dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ"); // yyyy-MM-dd

                        var encoded = HttpUtility.UrlEncode(filterValue.ToString(), Encoding.Default);
                        queryProps.Add($"{property.Item1}={encoded}");
                    }
                }

                // Aggregating an empty list == exception 
                if (queryProps.Count != 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append('?').Append(queryProps.Aggregate((a, b) => $"{a}&{b}"));
                    searchURL += sb.ToString();
                }
            }

            var result = await GetBeatmapsFromPage(searchURL, token).ConfigureAwait(false);
            if (result is null)
                return null;

            return new SearchPage(page, searchOptions, result)
            {
                Client = this
            };
        }

#endregion

#region Users

        public async Task<User?> User(int id, CancellationToken token = default, bool skipCacheCheck = false)
        {
            if (!skipCacheCheck && _fetchedUsers.TryGetValue(id, out User? user))
            {
                GetOrAddUserToCache(user, out user);
                return user;
            }

            var response = await _httpService.GetAsync("users/id/" + id, token).ConfigureAwait(false);
            if (!response.Successful)
                return null;

            user = await response.ReadAsObjectAsync<User>().ConfigureAwait(false);
            GetOrAddUserToCache(user, out user);
            return user;
        }

        public async Task<User?> User(string name, CancellationToken token = default, bool skipCacheCheck = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            name = name.ToLowerInvariant();
            if (!skipCacheCheck && _fetchedUsernames.TryGetValue(name, out User? user))
            {
                GetOrAddUserToCache(user, out user);
                return user;
            }

            var response = await _httpService.GetAsync("users/name/" + name, token).ConfigureAwait(false);
            if (!response.Successful)
                return null;

            var fetchedUser = await response.ReadAsObjectAsync<User>().ConfigureAwait(false);
            if (fetchedUser is null)
                return null;

            GetOrAddUserToCache(fetchedUser, out user);
            return user;
        }

#endregion

#region Voting

        /// <summary>
        /// Submits a vote on a map.
        /// </summary>
        /// <param name="levelHash">The hash of the map to vote on.</param>
        /// <param name="voteType">The voting type (whether it's an upvote or downvote).</param>
        /// <param name="platform">The platform in which the vote is coming from. This should be related to the <paramref name="platformID"/></param>
        /// <param name="platformID">The platform ID of the user submitting this vote. This should correspond to <paramref name="platform"/></param>
        /// <param name="proof">The proof (secret, auth ticket, etc) which can be used to verify people.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<VoteResponse> Vote(string levelHash, Vote.Type voteType, Vote.Platform platform, string platformID, string proof, CancellationToken token = default)
        {
            var vote = new Vote(levelHash, voteType, platform, platformID, proof);
            var response = await _httpService.PostAsync("vote", vote, token).ConfigureAwait(false);
            if (!response.Successful)
            {
                string reason;
                if (response.Code == 500)
                    reason = "Server error";
                else if (response.Code == 401)
                    reason = "Invalid auth ticket";
                else if (response.Code == 404)
                    reason = "Beatmap not found";
                else if (response.Code == 400)
                    reason = "Bad Request";
                else
                    reason = $"{nameof(BeatSaverSharp)}: Unknown";

                return new VoteResponse { Successful = false, Error = reason };
            }
            return await response.ReadAsObjectAsync<VoteResponse>().ConfigureAwait(false);
        }

#endregion

#region Byte Fetching

        internal async Task<byte[]?> DownloadZIP(BeatmapVersion version, CancellationToken token = default, IProgress<double>? progress = null)
        {
            var response = await _httpService.GetAsync(version.DownloadURL, token, progress).ConfigureAwait(false);
            if (!response.Successful)
                return null;
            return await response.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        internal async Task<byte[]?> DownloadCoverImage(BeatmapVersion version, CancellationToken token = default, IProgress<double>? progress = null)
        {
            var response = await _httpService.GetAsync(version.CoverURL, token, progress).ConfigureAwait(false);
            if (!response.Successful)
                return null;
            return await response.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        internal async Task<byte[]?> DownloadPreview(BeatmapVersion version, CancellationToken token = default, IProgress<double>? progress = null)
        {
            var response = await _httpService.GetAsync(version.PreviewURL, token, progress).ConfigureAwait(false);
            if (!response.Successful)
                return null;
            return await response.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

#endregion

        private void ProcessCache()
        {
            if (_options.MaximumCacheSize == null || _options.MaximumCacheSize == 0)
                return;

            if (_fetchedUsers.Count > _options.MaximumCacheSize)
            {
                _fetchedUsers.TryRemove(_fetchedUsers.Keys.GetEnumerator().Current, out _);
            }

            if (_fetchedUsernames.Count > _options.MaximumCacheSize)
            {
                _fetchedUsernames.TryRemove(_fetchedUsernames.Keys.GetEnumerator().Current, out _);
            }

            if (_fetchedBeatmaps.Count > _options.MaximumCacheSize)
            {
                _fetchedBeatmaps.TryRemove(_fetchedBeatmaps.Keys.GetEnumerator().Current, out _);
            }

            if (_fetchedHashedBeatmaps.Count > _options.MaximumCacheSize)
            {
                _fetchedHashedBeatmaps.TryRemove(_fetchedHashedBeatmaps.Keys.GetEnumerator().Current, out _);
            }
        }

        private async Task<IReadOnlyList<Beatmap>?> GetBeatmapsFromPage(string url, CancellationToken token = default)
        {
            var response = await _httpService.GetAsync(url, token).ConfigureAwait(false);
            if (!response.Successful)
                return null;

            var page = await response.ReadAsObjectAsync<SerializableSearch>().ConfigureAwait(false);
            if (page.Docs.Count == 0)
                return null;

            // We do this so there's only one source of a Beatmap object that exists
            // This way, Beatmaps will constantly have their properties updated as soon
            // as something else sees a newer version of it.
            List<Beatmap> beatmapList = new List<Beatmap>();
            foreach (var beatmap in page.Docs)
            {
                GetOrAddBeatmapToCache(beatmap, out Beatmap selfOrCached);
                beatmapList.Add(selfOrCached);
            }

            return new ReadOnlyCollection<Beatmap>(beatmapList);
        }

        /// <summary>
        /// Gets or adds a map to the cache. This will give the beatmap properties their client object if necessary.
        /// </summary>
        /// <param name="beatmap">The beatmap to get or add.</param>
        /// <param name="cachedAndOrBeatmap">The added or fetched Beatmap</param>
        /// <returns>Returns true if it was added. Returns false if it was already cached.</returns>
        private bool GetOrAddBeatmapToCache(Beatmap beatmap, out Beatmap cachedAndOrBeatmap)
        {
            if (!_options.Cache)
            {
                cachedAndOrBeatmap = beatmap;
                return false;
            }
            ProcessCache();

            lock (_bLock)
            {
                if (_fetchedBeatmaps.TryGetValue(beatmap.ID, out Beatmap? cachedBeatmap))
                {
                    if (beatmap.Automapper != cachedBeatmap.Automapper)
                        cachedBeatmap.Automapper = beatmap.Automapper;

                    if (beatmap.Curator != cachedBeatmap.Curator)
                        cachedBeatmap.Curator = beatmap.Curator;

                    if (beatmap.Description != cachedBeatmap.Description)
                        cachedBeatmap.Description = beatmap.Description;

                    if (beatmap.Metadata != cachedBeatmap.Metadata)
                        cachedBeatmap.Metadata = beatmap.Metadata;

                    if (beatmap.Name != cachedBeatmap.Name)
                        cachedBeatmap.Name = beatmap.Name;

                    if (beatmap.Qualified != cachedBeatmap.Qualified)
                        cachedBeatmap.Qualified = beatmap.Qualified;

                    if (beatmap.Ranked != cachedBeatmap.Ranked)
                        cachedBeatmap.Ranked = beatmap.Ranked;

                    if (beatmap.Stats != cachedBeatmap.Stats)
                        cachedBeatmap.Stats = beatmap.Stats;

                    if (beatmap.Uploaded != cachedBeatmap.Uploaded)
                        cachedBeatmap.Uploaded = beatmap.Uploaded;

                    // Setting it to null will cause the LatestVersion property to revalidate the next time it's called.
                    cachedBeatmap.LatestVersion = null!;
                    cachedBeatmap.Versions = beatmap.Versions;
                    PopulateWithClient(cachedBeatmap);

                    cachedAndOrBeatmap = cachedBeatmap;
                    return false;
                }
                else
                {
                    _fetchedBeatmaps.TryAdd(beatmap.ID, beatmap);
                    cachedAndOrBeatmap = beatmap;

                    foreach (var version in beatmap.Versions)
                    {
                        _fetchedHashedBeatmaps.TryAdd(version.Hash, beatmap);
                    }

                    PopulateWithClient(cachedAndOrBeatmap);
                    return true;
                }
            }
        }

        private bool GetOrAddUserToCache(User user, out User cachedAndOrUser)
        {
            if (!_options.Cache)
            {
                cachedAndOrUser = user;
                return false;
            }
            ProcessCache();

            lock (_uLock)
            {
                if (_fetchedUsers.TryGetValue(user.ID, out User? cachedUser))
                {
                    if (user.Hash != cachedUser.Hash)
                        cachedUser.Hash = user.Hash;

                    if (user.Name != cachedUser.Name)
                        cachedUser.Name = user.Name;

                    if (user.Avatar != cachedUser.Avatar)
                        cachedUser.Avatar = user.Avatar;

                    // Update the stats field on any updated user objects.
                    if (user.Stats != null && cachedUser.Stats is null)
                    {
                        cachedUser.Stats = user.Stats;
                    }
                    else if (cachedUser.Stats != null)
                    {
                        if (!cachedUser.Stats.Equals(user.Stats))
                            cachedUser.Stats = user.Stats;
                    }
                    cachedAndOrUser = cachedUser;
                    return false;
                }
                else
                {
                    string usernameLowercase = user.Name.ToLower();
                    if (_fetchedUsernames.TryGetValue(usernameLowercase, out User? cachedUsername))
                    {
                        // If the stats object is NOT null, that means this is a complete user object and should be added to the ID cache.
                        if (user.Stats != null)
                        {
                            cachedUsername.Stats = user.Stats;
                            _fetchedUsers.TryAdd(user.ID, cachedUsername);
                        }
                        cachedAndOrUser = cachedUsername;
                    }
                    else
                    {
                        cachedAndOrUser = user;
                        _fetchedUsernames.TryAdd(usernameLowercase, cachedAndOrUser);
                    }

                    if (!cachedAndOrUser.HasClient)
                        cachedAndOrUser.Client = this;
                    return true;
                }
            }
        }

        internal void PopulateWithClient(Beatmap beatmap)
        {
            if (!beatmap.HasClient)
                beatmap.Client = this;

            if (!beatmap.Uploader.HasClient)
            {
                GetOrAddUserToCache(beatmap.Uploader, out var uploader);
                beatmap.Uploader = uploader;
            }

            foreach (var version in beatmap.Versions)
            {
                if (!version.HasClient)
                {
                    version.Client = this;
                    if (version.Testplays != null)
                    {
                        foreach (var testplay in version.Testplays)
                        {
                            if (!testplay.HasClient)
                                testplay.Client = this;
                            if (!testplay.User.HasClient)
                            {
                                GetOrAddUserToCache(testplay.User, out var testplayer);
                                testplay.User = testplayer;
                            }
                        }
                    }
                }
            }
        }

        public void Clear()
        {
            _fetchedUsers.Clear();
            _fetchedUsernames.Clear();
            _fetchedBeatmaps.Clear();
            _fetchedHashedBeatmaps.Clear();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (_httpService is IDisposable disposable)
                disposable.Dispose();
            IsDisposed = true;
        }
    }
}