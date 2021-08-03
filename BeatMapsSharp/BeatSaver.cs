using BeatMapsSharp.Http;
using BeatMapsSharp.Models;
using System;

namespace BeatMapsSharp
{
    public class BeatSaver : IDisposable
    {
        internal bool IsDisposed { get; set; }
        private readonly IHttpService _httpService;

        public BeatSaver()
        {
            _httpService = new HttpClientService("https://api.beatmaps.io/api/", TimeSpan.FromSeconds(30), "BeatSaverSharp/3.0.0");
        }

        private void PopulateWithClient(Beatmap beatmap)
        {
            if (!beatmap.HasClient)
                beatmap.Client = this;

            if (!beatmap.User.HasClient)
                beatmap.User.Client = this;

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