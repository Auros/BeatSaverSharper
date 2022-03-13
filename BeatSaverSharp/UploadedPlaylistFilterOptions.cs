using System;

namespace BeatSaverSharp
{
    /// <summary>
    /// Provides options for filtering BeatSaver playlists by Latest
    /// </summary>
    public class UploadedPlaylistFilterOptions
    {
        /// <summary>
        ///  Start searching for playlists that released before or during this date.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Returns playlists before the time specified. 
        /// </summary>
        public DateTime? Before { get; set; }

        /// <summary>
        /// The sorting applied to the search.
        /// </summary>
        public LatestPlaylistFilterSort? Sort { get; set; }

        public UploadedPlaylistFilterOptions(DateTime? startDate = null, DateTime? beforeDate = null, LatestPlaylistFilterSort? sort = null)
        {
            StartDate = startDate;
            Sort = sort;
            Before = beforeDate;
        }
    }

    public enum LatestPlaylistFilterSort
    {
        UPDATED,
        SONGS_UPDATED,
        CREATED
    }
}