using BeatMapsSharp.Http;
using BeatMapsSharp.Models;
using BeatMapsSharp.Models.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeatMapsSharp
{
    public class BeatSaver : IDisposable
    {
        internal bool IsDisposed { get; set; }
        private readonly IHttpService _httpService;
        private readonly ConcurrentDictionary<string, Beatmap> _fetchedBeatmaps = new ConcurrentDictionary<string, Beatmap>();
        private readonly ConcurrentDictionary<string, Beatmap> _fetchedHashedBeatmaps = new ConcurrentDictionary<string, Beatmap>();

        public BeatSaver()
        {
            _httpService = new HttpClientService("https://beatmaps.io/api/", TimeSpan.FromSeconds(30), "BeatSaverSharp/3.0.0");
        }

        #region Beatmaps

        public async Task<Beatmap?> Beatmap(string key, CancellationToken? token = null)
        {
            if (_fetchedBeatmaps.TryGetValue(key, out Beatmap? beatmap))
                return beatmap;

            return await FetchBeatmap("maps/id/" + key, token);
        }

        public async Task<Beatmap?> BeatmapByHash(string hash, CancellationToken? token = null)
        {
            if (string.IsNullOrWhiteSpace(hash))
                return null;

            if (_fetchedHashedBeatmaps.TryGetValue(hash, out Beatmap? beatmap))
                return beatmap;

            return await FetchBeatmap("maps/hash/" + hash, token);
        }

        private async Task<Beatmap?> FetchBeatmap(string url, CancellationToken? token = null)
        {
            var response = await _httpService.GetAsync(url, token).ConfigureAwait(false);
            if (!response.Successful)
                return null;

            Beatmap beatmap = await response.ReadAsObjectAsync<Beatmap>();
            GetOrAddBeatmapToCache(beatmap, out beatmap);
            return beatmap;
        }

        #endregion

        #region Paged Beatmaps

        public async Task<Page?> Latest(LatestFilterOptions options = default, CancellationToken? token = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("maps/latest?automapper=");
            sb.Append(options.IncludeAutomappers);
            if (options.StartDate.HasValue)
                sb.Append("&after=").Append(options.StartDate.Value.ToString("O"));

            var result = await GetBeatmapsFromPage(sb.ToString(), token).ConfigureAwait(false);
            if (result is null)
                return null;

            return new LatestPage(options, result)
            {
                Client = this
            };
        }

        #endregion

        private async Task<IReadOnlyList<Beatmap>?> GetBeatmapsFromPage(string url, CancellationToken? token = null)
        {
            var response = await _httpService.GetAsync(url, token).ConfigureAwait(false);
            if (!response.Successful)
                return null;

            var page = await response.ReadAsObjectAsync<SerializablePage>();
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
            if (_fetchedBeatmaps.TryGetValue(beatmap.ID, out Beatmap? cachedBeatmap))
            {
                // TODO: Refresh beatmap, probably.
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

        private void PopulateWithClient(Beatmap beatmap)
        {
            if (!beatmap.HasClient)
                beatmap.Client = this;

            if (!beatmap.Uploader.HasClient)
                beatmap.Uploader.Client = this;

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
                                testplay.User.Client = this;
                        }
                    }
                }
            }
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