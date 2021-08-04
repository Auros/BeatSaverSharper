using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatMapsSharp.Models.Pages
{
    internal sealed class LatestPage : Page
    {
        private Page? PreviousPage { get; set; }
        private readonly LatestFilterOptions _query;
        
        public LatestPage(LatestFilterOptions query, IReadOnlyList<Beatmap> beatmaps, Page? previousPage = null) : base(beatmaps)
        {
            _query = query;
            PreviousPage = previousPage;
        }

        public override async Task<Page?> Next(CancellationToken? token = null)
        {
            if (Beatmaps.Count == 0)
                return null;

            Beatmap oldest = Beatmaps[^1];
            LatestFilterOptions options = new LatestFilterOptions(oldest.Uploaded, _query.IncludeAutomappers);
            var nextPage = await Client.Latest(options, token).ConfigureAwait(false);
            if (nextPage is LatestPage nextLatest)
                nextLatest.PreviousPage = this;
            return nextPage;
        }

        public override Task<Page?> Previous(CancellationToken? token = null)
        {
            return Task.FromResult(PreviousPage);
        }
    }
}