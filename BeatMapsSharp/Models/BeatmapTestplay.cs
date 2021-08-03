using System;

namespace BeatMapsSharp.Models
{
    /// <summary>
    /// Represents a playtest of a beatmap.
    /// </summary>
    public class BeatmapTestplay
    {
        /// <summary>
        /// The time the testplay was initialized.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The the time feedback was given.
        /// </summary>
        public string? Feedback { get; set; }

        /// <summary>
        /// The time at which feedback was given.
        /// </summary>
        public DateTime? FeedbackAt { get; set; }
    
        /// <summary>
        /// A video related to the feedback.
        /// </summary>
        public string? Video { get; set; }

        /// <summary>
        /// The user who submitted the testplay.
        /// </summary>
        public User User { get; set; } = null!;
    }
}