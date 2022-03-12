using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models.Pages
{
    public class PlaylistDetail : BeatSaverObject
    {
        private readonly int _pageNumber;
        public Playlist Playlist { get; }
        public IReadOnlyList<OrderedBeatmap> Beatmaps { get; }

        public bool Empty => Beatmaps.Count is 0;
        
        public PlaylistDetail(int pageNumber, Playlist playlist, IReadOnlyList<OrderedBeatmap> beatmaps)
        {
            _pageNumber = pageNumber;
            Playlist = playlist;
            Beatmaps = beatmaps;
        }

        public Task<PlaylistDetail?> Previous(CancellationToken token = default, bool skipCacheCheck = false) =>
            Client.Playlist(Playlist.ID, token, _pageNumber - 1, skipCacheCheck);

        public Task<PlaylistDetail?> Next(CancellationToken token = default, bool skipCacheCheck = false) =>
            Client.Playlist(Playlist.ID, token, _pageNumber + 1, skipCacheCheck);
    }
}