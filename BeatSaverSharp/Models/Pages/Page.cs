using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models.Pages
{
    public abstract class Page : BeatSaverObject
    {
        public bool Empty => Beatmaps.Count is 0;
        public IReadOnlyList<Beatmap> Beatmaps { get; }

        public Page(IReadOnlyList<Beatmap> beatmaps)
        {
            Beatmaps = beatmaps;
        }

        public abstract Task<Page?> Previous(CancellationToken? token = null);
        public abstract Task<Page?> Next(CancellationToken? token = null);
    }
}