using System;

namespace BeatMapsSharp
{
    /// <summary>
    /// Provides options for filtering BeatSaver by Latest
    /// </summary>
    public struct LatestFilterOptions
    {
        /// <summary>
        /// Start searching for songs that released before or during this date.
        /// </summary>
        public DateTime? StartDate { get; }

        /// <summary>
        /// Whether or not to include automapped beatmaps in your search.
        /// </summary>
        public bool IncludeAutomappers { get; }

        public LatestFilterOptions(DateTime? startDate, bool includeAutomappers)
        {
            StartDate = startDate;
            IncludeAutomappers = includeAutomappers;
        }
    }
}