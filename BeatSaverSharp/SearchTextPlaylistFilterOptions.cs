using System;

namespace BeatSaverSharp
{
    public class SearchTextPlaylistFilterOptions
    {
        public static SearchTextFilterOption Latest => new SearchTextFilterOption { SortOrder = SortingOptions.Latest };
        public static SearchTextFilterOption Rating => new SearchTextFilterOption { SortOrder = SortingOptions.Rating };
        public static SearchTextFilterOption Relevance => new SearchTextFilterOption { SortOrder = SortingOptions.Relevance };

        public SearchTextPlaylistFilterOptions() { }

        public SearchTextPlaylistFilterOptions(string search)
        {
            Query = search;
        }
        
        /// <summary>
        /// Start the search with this date.
        /// </summary>
        public DateTime? From { get; set; }
        
        /// <summary>
        /// End the search after this date.
        /// </summary>
        public DateTime? To { get; set; }
        
        /// <summary>
        /// Show only curated playlists
        /// </summary>
        [QueryKeyName("curated")]
        public bool Curated { get; set; }
        
        /// <summary>
        /// If empty playlists should be included or not
        /// </summary>
        [QueryKeyName("includeEmpty")]
        public bool IncludeEmpty { get; set; }
        
        /// <summary>
        /// Keywords to search with.
        /// </summary>
        [QueryKeyName("q")]
        public string? Query { get; set; }
        
        /// <summary>
        /// How to sort the search.
        /// </summary>
        public SortingOptions? SortOrder { get; set; }
    }
}