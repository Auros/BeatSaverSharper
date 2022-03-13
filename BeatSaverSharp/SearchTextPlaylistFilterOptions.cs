using System;

namespace BeatSaverSharp
{
    /// <summary>
    /// Options for filtering playlists through BeatSaver. All properties are nullable by default. Setting one to a value
    /// will make it so maps have to meet that value. For example, setting Curated to true will return only curated playlists.
    /// Setting it to false will make it so curated playlists are never returned. Setting it to null will include all playlists,
    /// whether they are curated or not.
    /// </summary>
    public class SearchTextPlaylistFilterOptions
    {
        public static SearchTextPlaylistFilterOptions Latest => new SearchTextPlaylistFilterOptions { SortOrder = SortingOptions.Latest };
        public static SearchTextPlaylistFilterOptions Rating => new SearchTextPlaylistFilterOptions { SortOrder = SortingOptions.Rating };
        public static SearchTextPlaylistFilterOptions Relevance => new SearchTextPlaylistFilterOptions { SortOrder = SortingOptions.Relevance };
        public static SearchTextPlaylistFilterOptions Curated => new SearchTextPlaylistFilterOptions { SortOrder = SortingOptions.Curated };

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
        /// Is a playlist curated
        /// </summary>
        [QueryKeyName("curated")]
        public bool IsCurated { get; set; }
        
        /// <summary>
        /// Include empty playlists.
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