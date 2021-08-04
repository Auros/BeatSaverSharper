using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models.Pages
{
    internal class SearchPage : Page
    {
        private readonly int _pageNumber;
        private readonly SearchTextFilterOption? _searchTextFilterOptions;

        public SearchPage(int pageNumber, SearchTextFilterOption? searchTextFilterOption, IReadOnlyList<Beatmap> beatmaps) : base(beatmaps)
        {
            _pageNumber = pageNumber;
            _searchTextFilterOptions = searchTextFilterOption;
        }

        public override Task<Page?> Next(CancellationToken token = default)
        {
            return Client.SearchBeatmaps(_searchTextFilterOptions, _pageNumber + 1, token);
        }

        public override Task<Page?> Previous(CancellationToken token = default)
        {
            if (_pageNumber == 0)
                return Task.FromResult<Page?>(null);
            return Client.SearchBeatmaps(_searchTextFilterOptions, _pageNumber - 1, token);
        }
    }
}