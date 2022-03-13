using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models.Pages
{
    internal sealed class UploadedPlaylistPage : PlaylistPage
    {
        private PlaylistPage? PreviousPage { get; set; }
        private readonly UploadedPlaylistFilterOptions _query;

        public UploadedPlaylistPage(UploadedPlaylistFilterOptions query, IReadOnlyList<Playlist> playlists, PlaylistPage? previousPage = null) : base(playlists)
        {
            _query = query;
            PreviousPage = previousPage;
        }

        public override async Task<PlaylistPage?> Previous(CancellationToken token = default)
        {
            if (PreviousPage is null && Playlists.Count > 0)
            {
                var options = new UploadedPlaylistFilterOptions(_query.StartDate, GetSortDate(Playlists[0]), _query.Sort);
                PreviousPage = await Client.LatestPlaylists(options, token).ConfigureAwait(false);
            }
            return PreviousPage;
        }

        public override async Task<PlaylistPage?> Next(CancellationToken token = default)
        {
            if (Playlists.Count == 0)
                return null;

            var oldest = Playlists[Playlists.Count - 1];
            var options = new UploadedPlaylistFilterOptions(GetSortDate(oldest));
            var nextPage = await Client.LatestPlaylists(options, token).ConfigureAwait(false);
            if (nextPage is UploadedPlaylistPage nextLatest)
                nextLatest.PreviousPage = this;
            return nextPage;
        }

        private DateTime GetSortDate(Playlist playlist)
        {
            return _query.Sort switch
            {
                LatestPlaylistFilterSort.UPDATED => playlist.UpdatedAt,
                LatestPlaylistFilterSort.SONGS_UPDATED => playlist.SongsChangedAt ?? playlist.UpdatedAt,
                _ => playlist.CreatedAt,
            };
        }
    }
}