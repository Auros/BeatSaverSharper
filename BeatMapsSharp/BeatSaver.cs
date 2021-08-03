using BeatMapsSharp.Http;
using BeatMapsSharp.Models;
using System;
using System.Collections.Concurrent;
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
            if (_fetchedBeatmaps.TryGetValue(beatmap.ID, out Beatmap? cachedBeatmap))
            {
                // TODO: Refresh beatmap, probably.
                return cachedBeatmap;
            }
            else
            {
                _fetchedBeatmaps.TryAdd(beatmap.ID, beatmap);
            }
            
            foreach (var version in beatmap.Versions)
            {
                if (_fetchedHashedBeatmaps.TryGetValue(version.Hash, out cachedBeatmap))
                {
                    // TODO: Refresh beatmap, probably.
                    return cachedBeatmap;
                }
                else
                {
                    _fetchedHashedBeatmaps.TryAdd(version.Hash, beatmap);
                }
            }
            
            PopulateWithClient(beatmap);
            return beatmap;
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