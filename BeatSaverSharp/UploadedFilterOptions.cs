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
        /// Returns maps before the time specified. 
        /// </summary>
        public DateTime? Before { get; set; }

        /// <summary>
        /// Whether or not to include automapped beatmaps in your search.
        /// </summary>
        public bool IncludeAutomappers { get; set; }

        /// <summary>
        /// The sorting applied to the search.
        /// </summary>
        public LatestFilterSort? Sort { get; set; }

        public UploadedFilterOptions(DateTime? startDate, bool includeAutomappers)
        {
            StartDate = startDate;
            IncludeAutomappers = includeAutomappers;
        }

        public UploadedFilterOptions(DateTime? startDate, DateTime? beforeDate, bool includeAutomappers, LatestFilterSort? sort)
        {
            Sort = sort;
            Before = beforeDate;
            StartDate = startDate;
            IncludeAutomappers = includeAutomappers;
        }
    }


    public enum LatestFilterSort
    {
        FIRST_PUBLISHED,
        UPDATED,
        LAST_PUBLISHED,
        CREATED
    }
}