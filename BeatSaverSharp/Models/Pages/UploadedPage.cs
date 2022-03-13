using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models.Pages
{
    internal sealed class UploadedPage : Page
    {
        private Page? PreviousPage { get; set; }
        private readonly UploadedFilterOptions _query;

        public UploadedPage(UploadedFilterOptions query, IReadOnlyList<Beatmap> beatmaps, Page? previousPage = null) : base(beatmaps)
        {
            _query = query;
            PreviousPage = previousPage;
        }

        public override async Task<Page?> Next(CancellationToken token = default)
        {
            if (Beatmaps.Count == 0)
                return null;

            Beatmap oldest = Beatmaps[Beatmaps.Count - 1];
            UploadedFilterOptions options = new UploadedFilterOptions(oldest.Uploaded, _query.IncludeAutomappers);
            var nextPage = await Client.LatestBeatmaps(options, token).ConfigureAwait(false);
            if (nextPage is UploadedPage nextLatest)
                nextLatest.PreviousPage = this;
            return nextPage;
        }

        public override async Task<Page?> Previous(CancellationToken token = default)
        {
            if (PreviousPage is null && Beatmaps.Count > 0)
            {
                UploadedFilterOptions options = new UploadedFilterOptions(_query.StartDate, Beatmaps[0].Uploaded, _query.IncludeAutomappers, _query.Sort);
                PreviousPage = await Client.LatestBeatmaps(options, token).ConfigureAwait(false);
            }
            return PreviousPage;
        }
    }
}