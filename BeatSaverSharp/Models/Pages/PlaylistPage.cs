using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models.Pages
{
    public class PlaylistPage : BeatSaverObject
    {
        private readonly int _pageNumber;
        public Playlist Playlist { get; }
        public IReadOnlyList<OrderedBeatmap> Beatmaps { get; }

        public bool Empty => Beatmaps.Count is 0;
        
        public PlaylistPage(int pageNumber, Playlist playlist, IReadOnlyList<OrderedBeatmap> beatmaps)
        {
            _pageNumber = pageNumber;
            Playlist = playlist;
            Beatmaps = beatmaps;
        }

        public Task<PlaylistPage?> Previous(CancellationToken token = default, bool skipCacheCheck = false) =>
            Client.Playlist(Playlist.ID, token, _pageNumber - 1, skipCacheCheck);

        public Task<PlaylistPage?> Next(CancellationToken token = default, bool skipCacheCheck = false) =>
            Client.Playlist(Playlist.ID, token, _pageNumber + 1, skipCacheCheck);
    }
}