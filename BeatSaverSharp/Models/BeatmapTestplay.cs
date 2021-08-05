using Newtonsoft.Json;
using System;

namespace BeatSaverSharp.Models
{
    /// <summary>
    /// Represents a playtest of a beatmap.
    /// </summary>
    public class BeatmapTestplay : BeatSaverObject
    {
        /// <summary>
        /// The time the testplay was initialized.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; internal set; }

        /// <summary>
        /// The the time feedback was given.
        /// </summary>
        [JsonProperty("feedback")]
        public string? Feedback { get; internal set; }

        /// <summary>
        /// The time at which feedback was given.
        /// </summary>
        [JsonProperty("feedbackAt")]
        public DateTime? FeedbackAt { get; internal set; }

        /// <summary>
        /// A video related to the feedback.
        /// </summary>
        [JsonProperty("video")]
        public string? Video { get; internal set; }

        /// <summary>
        /// The user who submitted the testplay.
        /// </summary>
        [JsonProperty("user")]
        public User User { get; internal set; } = null!;

        internal BeatmapTestplay() { }
    }
}