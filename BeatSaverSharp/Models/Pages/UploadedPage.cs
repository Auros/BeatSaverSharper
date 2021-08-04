using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatMapsSharp.Models.Pages
{
    internal sealed class UploadedPage : Page
    {
        private Page? PreviousPage { get; set; }
        private readonly UploadedFilterOptions _query;
        
        public UploadedPage(ref UploadedFilterOptions query, IReadOnlyList<Beatmap> beatmaps, Page? previousPage = null) : base(beatmaps)
        {
            _query = query;
            PreviousPage = previousPage;
        }

        public override async Task<Page?> Next(CancellationToken? token = null)
        {
            if (Beatmaps.Count == 0)
                return null;

            Beatmap oldest = Beatmaps[^1];
            UploadedFilterOptions options = new UploadedFilterOptions(oldest.Uploaded, _query.IncludeAutomappers);
            var nextPage = await Client.UploadedBeatmaps(options, token).ConfigureAwait(false);
            if (nextPage is UploadedPage nextLatest)
                nextLatest.PreviousPage = this;
            return nextPage;
        }

        public override Task<Page?> Previous(CancellationToken? token = null)
        {
            return Task.FromResult(PreviousPage);
        }
    }
}