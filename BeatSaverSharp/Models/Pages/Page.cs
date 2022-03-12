using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models.Pages
{
    public abstract class Page : PageBase<Beatmap>
    {
        public IReadOnlyList<Beatmap> Beatmaps => Items;
        public Page(IReadOnlyList<Beatmap> beatmaps) : base(beatmaps){}
        public abstract Task<Page?> Previous(CancellationToken token = default);
        public abstract Task<Page?> Next(CancellationToken token = default);
    }
}