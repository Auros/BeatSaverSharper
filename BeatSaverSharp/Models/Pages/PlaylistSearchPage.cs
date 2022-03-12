using System.Collections.Generic;

namespace BeatSaverSharp.Models.Pages
{
    public class PlaylistSearchPage : BeatSaverObject
    {
        private readonly int _pageNumber;
        private readonly SearchTextPlaylistFilterOptions? _searchTextPlaylistFilterOptions;
        public IReadOnlyList<Playlist> Playlists { get; }

        public bool Empty => Playlists.Count is 0;
        
        public PlaylistSearchPage(int pageNumber, SearchTextPlaylistFilterOptions? searchTextPlaylistFilterOptions, IReadOnlyList<Playlist> playlists)
        {
            _pageNumber = pageNumber;
            Playlists = playlists;
            _searchTextPlaylistFilterOptions = searchTextPlaylistFilterOptions;
        }
    }
}