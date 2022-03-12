using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models.Pages
{
    internal class PlaylistSearchPage : PlaylistPage
    {
        private readonly int _pageNumber;
        private readonly SearchTextPlaylistFilterOptions? _searchTextPlaylistFilterOptions;

        public PlaylistSearchPage(int pageNumber, SearchTextPlaylistFilterOptions? searchTextPlaylistFilterOptions, IReadOnlyList<Playlist> playlists) : base(playlists)
        {
            _pageNumber = pageNumber;
            _searchTextPlaylistFilterOptions = searchTextPlaylistFilterOptions;
        }

        public override Task<PlaylistPage?> Previous(CancellationToken token = default) =>
            Client.SearchPlaylists(_searchTextPlaylistFilterOptions, _pageNumber - 1, token);

        public override Task<PlaylistPage?> Next(CancellationToken token = default) =>
            Client.SearchPlaylists(_searchTextPlaylistFilterOptions, _pageNumber + 1, token);
    }
}