using System;

namespace BeatSaverSharp
{
    /// <summary>
    /// Provides options for filtering BeatSaver by Latest
    /// </summary>
    public class UploadedFilterOptions
    {
        /// <summary>
        /// Start searching for songs that released before or during this date.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Whether or not to include automapped beatmaps in your search.
        /// </summary>
        public bool IncludeAutomappers { get; set; }

        public UploadedFilterOptions(DateTime? startDate, bool includeAutomappers)
        {
            StartDate = startDate;
            IncludeAutomappers = includeAutomappers;
        }
    }
}