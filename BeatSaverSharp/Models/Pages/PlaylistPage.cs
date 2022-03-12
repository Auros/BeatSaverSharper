using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models.Pages
{
    public abstract class PlaylistPage : PageBase<Playlist>
    {
        public IReadOnlyList<Playlist> Playlists => Items;
        public PlaylistPage(IReadOnlyList<Playlist> playlists) : base(playlists) {}
        public abstract Task<PlaylistPage?> Previous(CancellationToken token = default);
        public abstract Task<PlaylistPage?> Next(CancellationToken token = default);
    }
}