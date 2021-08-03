using Newtonsoft.Json;
using System;

namespace BeatMapsSharp.Models
{
    /// <summary>
    /// User stats.
    /// </summary>
    public class UserStats
    {
        /// <summary>
        /// The averaged beats per minute (BPM) of all of a user's uploaded maps.
        /// </summary>
        [JsonProperty("avgBpm")]
        public float AverageBPM { get; set; }
    
        /// <summary>
        /// The averaged duration of all of a user's uploaded maps.
        /// </summary>
        [JsonProperty("avgDuration")]
        public float AverageDuration { get; set; }

        /// <summary>
        /// The averaged score (or rating) of all of a user's uploaded maps.
        /// </summary>
        [JsonProperty("avgScore")]
        public float AverageScore { get; set; }

        /// <summary>
        /// The difficulty statistics of all of a user's uploaded maps.
        /// </summary>
        [JsonProperty("diffStats")]
        public UserDifficultyStats DifficultyStats { get; set; } = null!;

        /// <summary>
        /// The time that the user's first map was uploaded.
        /// </summary>
        public DateTime FirstUpload { get; set; }

        /// <summary>
        /// The time that the user's most recent map was uploaded.
        /// </summary>
        public DateTime LastUpload { get; set; }
    
        /// <summary>
        /// The amount of ranked maps a user has uploaded.
        /// </summary>
        public int RankedMaps { get; set; }

        /// <summary>
        /// The total amount of downvotes that have accumulated on a user's profile.
        /// </summary>
        public int TotalDownvotes { get; set; }

        /// <summary>
        /// The total amount of maps a user has uploaded.
        /// </summary>
        public int TotalMaps { get; set; }

        /// <summary>
        /// The total amount of upvotes that have accumulated on a user's profile.
        /// </summary>
        public int TotalUpvotes { get; set; }
    }
}
