using Newtonsoft.Json;
using System;

namespace BeatSaverSharp.Models
{
    /// <summary>
    /// User stats.
    /// </summary>
    public class UserStats
    {
        /// <summary>
        /// The total amount of upvotes that have accumulated on a user's profile.
        /// </summary>
        [JsonProperty("totalUpvotes")]
        public int TotalUpvotes { get; internal set; }

        /// <summary>
        /// The total amount of downvotes that have accumulated on a user's profile.
        /// </summary>
        [JsonProperty("totalDownvotes")]
        public int TotalDownvotes { get; internal set; }

        /// <summary>
        /// The total amount of maps a user has uploaded.
        /// </summary>
        [JsonProperty("totalMaps")]
        public int TotalMaps { get; internal set; }

        /// <summary>
        /// The amount of ranked maps a user has uploaded.
        /// </summary>
        [JsonProperty("rankedMaps")]
        public int RankedMaps { get; internal set; }

        /// <summary>
        /// The averaged beats per minute (BPM) of all of a user's uploaded maps.
        /// </summary>
        [JsonProperty("avgBpm")]
        public float AverageBPM { get; internal set; }

        /// <summary>
        /// The averaged score (or rating) of all of a user's uploaded maps.
        /// </summary>
        [JsonProperty("avgScore")]
        public float AverageScore { get; internal set; }

        /// <summary>
        /// The averaged duration of all of a user's uploaded maps.
        /// </summary>
        [JsonProperty("avgDuration")]
        public float AverageDuration { get; internal set; }

        /// <summary>
        /// The time that the user's first map was uploaded.
        /// </summary>
        [JsonProperty("firstUpload")]
        public DateTime? FirstUpload { get; internal set; }

        /// <summary>
        /// The time that the user's most recent map was uploaded.
        /// </summary>
        [JsonProperty("lastUpload")]
        public DateTime? LastUpload { get; internal set; }

        /// <summary>
        /// The difficulty statistics of all of a user's uploaded maps.
        /// </summary>
        [JsonProperty("diffStats")]
        public UserDifficultyStats? DifficultyStats { get; internal set; }

        internal UserStats() { }

        public bool Equals(UserStats? other)
        {
            if (other is null)
                return false;

            if (AverageBPM != other.AverageBPM)
                return false;

            if (AverageDuration != other.AverageDuration)
                return false;

            if (AverageScore != other.AverageScore)
                return false;

            if (FirstUpload != other.FirstUpload)
                return false;

            if (LastUpload != other.LastUpload)
                return false;

            if (RankedMaps != other.RankedMaps)
                return false;

            if (TotalDownvotes != other.TotalDownvotes)
                return false;

            if (TotalMaps != other.TotalMaps)
                return false;

            if (TotalUpvotes != other.TotalUpvotes)
                return false;

            return true;
        }
    }
}