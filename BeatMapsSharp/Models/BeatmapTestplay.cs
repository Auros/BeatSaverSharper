using System;

namespace BeatMapsSharp.Models
{
    /// <summary>
    /// Represents a playtest of a beatmap.
    /// </summary>
    public class BeatmapTestplay : BeatSaverObject
    {
        /// <summary>
        /// The time the testplay was initialized.
        /// </summary>
        public DateTime CreatedAt { get; internal set; }

        /// <summary>
        /// The the time feedback was given.
        /// </summary>
        public string? Feedback { get; internal set; }

        /// <summary>
        /// The time at which feedback was given.
        /// </summary>
        public DateTime? FeedbackAt { get; internal set; }
    
        /// <summary>
        /// A video related to the feedback.
        /// </summary>
        public string? Video { get; internal set; }

        /// <summary>
        /// The user who submitted the testplay.
        /// </summary>
        public User User { get; internal set; } = null!;
    }
}