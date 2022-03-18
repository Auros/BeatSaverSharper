using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models.Pages
{
    public class PlaylistUserPage : PlaylistPage
    {
        private readonly int _user;
        private readonly int _pageNumber;
        
        public PlaylistUserPage(int pageNumber, int user, IReadOnlyList<Playlist> playlists) : base(playlists)
        {
            _user = user;
            _pageNumber = pageNumber;
        }

        public override Task<PlaylistPage?> Previous(CancellationToken token = default) =>
            Client.UserPlaylists(_user, _pageNumber - 1, token);

        public override Task<PlaylistPage?> Next(CancellationToken token = default) =>
            Client.UserPlaylists(_user, _pageNumber + 1, token);
    }
}