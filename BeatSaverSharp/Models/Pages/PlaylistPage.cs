using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models.Pages
{
    public class PlaylistPage : BeatSaverObject
    {
        private readonly int _pageNumber;
        public readonly Playlist _playlist;
        public readonly IReadOnlyList<OrderedBeatmap> _beatmaps;

        public bool Empty => _beatmaps.Count is 0;
        
        public PlaylistPage(int pageNumber, Playlist playlist, IReadOnlyList<OrderedBeatmap> beatmaps)
        {
            _pageNumber = pageNumber;
            _playlist = playlist;
            _beatmaps = beatmaps;
        }

        public Task<PlaylistPage?> Previous(CancellationToken token = default, bool skipCacheCheck = false) =>
            Client.Playlist(_playlist.ID, token, _pageNumber - 1, skipCacheCheck);

        public Task<PlaylistPage?> Next(CancellationToken token = default, bool skipCacheCheck = false) =>
            Client.Playlist(_playlist.ID, token, _pageNumber + 1, skipCacheCheck);
    }
}