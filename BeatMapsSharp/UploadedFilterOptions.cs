using System;

namespace BeatMapsSharp
{
    /// <summary>
    /// Provides options for filtering BeatSaver by Latest
    /// </summary>
    public struct UploadedFilterOptions
    {
        /// <summary>
        /// Start searching for songs that released before or during this date.
        /// </summary>
        public DateTime? StartDate { get; }

        /// <summary>
        /// Whether or not to include automapped beatmaps in your search.
        /// </summary>
        public bool IncludeAutomappers { get; }

        public UploadedFilterOptions(DateTime? startDate, bool includeAutomappers)
        {
            StartDate = startDate;
            IncludeAutomappers = includeAutomappers;
        }
    }
}