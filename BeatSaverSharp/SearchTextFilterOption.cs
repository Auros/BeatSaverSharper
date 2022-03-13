using System;

namespace BeatSaverSharp
{
    /// <summary>
    /// Options for filtering through BeatSaver. All properties are nullable by default. Setting one to a value
    /// will make it so maps have to meet that value. For example, setting Chroma to true will return only Chroma maps.
    /// Setting it to false will make it so Chroma maps are never returned. Setting it to null will include all maps,
    /// whether they use Chroma or not.
    /// </summary>
    public class SearchTextFilterOption
    {
        public static SearchTextFilterOption Latest => new SearchTextFilterOption { SortOrder = SortingOptions.Latest };
        public static SearchTextFilterOption Rating => new SearchTextFilterOption { SortOrder = SortingOptions.Rating };
        public static SearchTextFilterOption Relevance => new SearchTextFilterOption { SortOrder = SortingOptions.Relevance };
        public static SearchTextFilterOption Curated => new SearchTextFilterOption { SortOrder = SortingOptions.Relevance };

        public SearchTextFilterOption()
        {

        }

        public SearchTextFilterOption(string search)
        {
            Query = search;
        }

        /// <summary>
        /// Is a map automatically generated?
        /// </summary>
        /// <remarks>
        /// Setting this to null (default) will make it so only human-made maps are returned. Setting it to false will only return
        /// auto-mapped songs. Setting it to true will include both.
        /// </remarks>
        [QueryKeyName("automapper")]
        public bool? IncludeAutomappers { get; set; }

        /// <summary>
        /// Does a map have Chroma effects?
        /// </summary>
        public bool? Chroma { get; set; }

        /// <summary>
        /// Does a map support the Cinema mod?
        /// </summary>
        public bool? Cinema { get; set; }

        /// <summary>
        /// Start the search with this date.
        /// </summary>
        public DateTime? From { get; set; }

        /// <summary>
        /// Does a map have a "full spread"? (Easy, Normal, Hard, Expert, and Expert+ maps)
        /// </summary>
        public bool? FullSpread { get; set; }

        /// <summary>
        /// The maximum BPM (beats per minute) of a map.
        /// </summary>
        public float? MaxBPM { get; set; }

        /// <summary>
        /// The maximum length (in seconds) of a map.
        /// </summary>
        public float? MaxDuration { get; set; }

        /// <summary>
        /// The maximum NPS (notes per second) of a map.
        /// </summary>
        public float? MaxNPS { get; set; }

        /// <summary>
        /// The maximum rating of a map.
        /// </summary>
        public float? MaxRating { get; set; }

        /// <summary>
        /// Does a map support Mapping Extensions effects?
        /// </summary>
        [QueryKeyName("me")]
        public bool? MappingExtensions { get; set; }

        /// <summary>
        /// The minimum BPM of a map.
        /// </summary>
        public float? MinBPM { get; set; }

        /// <summary>
        /// The minum duration (in seconds) of a map.
        /// </summary>
        public int? MinDuration { get; set; }

        /// <summary>
        /// The minimum NPS (notes per second) of a map.
        /// </summary>
        public float? MinNPS { get; set; }

        /// <summary>
        /// The minimum rating of a map.
        /// </summary>
        public float? MinRating { get; set; }

        /// <summary>
        /// Does this map support Noodle Extensions effects?
        /// </summary>
        [QueryKeyName("noodle")]
        public bool? NoodleExtensions { get; set; }

        /// <summary>
        /// Keywords to search with.
        /// </summary>
        [QueryKeyName("q")]
        public string? Query { get; set; }

        /// <summary>
        /// Is a map ranked on ScoreSaber?
        /// </summary>
        public bool? Ranked { get; set; }

        /// <summary>
        /// How to sort the search.
        /// </summary>
        public SortingOptions? SortOrder { get; set; }

        /// <summary>
        /// End the search after this date.
        /// </summary>
        public DateTime? To { get; set; }
    }
}
