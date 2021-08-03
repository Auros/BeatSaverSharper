using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BeatMapsSharp.Models
{
    /// <summary>
    /// Represents a specific version of a Beatmap
    /// </summary>
    public class BeatmapVersion : IEquatable<BeatmapVersion>
    {
        /// <summary>
        /// The time this version was creeated.
        /// </summary>
        public DateTime CreatedAt { get; internal set; }

        /// <summary>
        /// The feedback for this version.
        /// </summary>
        public string? Feedback { get; internal set; }
    
        /// <summary>
        /// The sage score.
        /// </summary>
        public short SageScore { get; internal set; }

        /// <summary>
        /// The map's hash.
        /// </summary>
        public string Hash { get; internal set; } = null!;

        /// <summary>
        /// The map's key.
        /// </summary>
        public string Key { get; internal set; } = null!;
    
        /// <summary>
        /// The state of this version.
        /// </summary>
        public VersionState State { get; internal set; }

        /// <summary>
        /// The time at which the most recent testplay was set for this version.
        /// </summary>
        public DateTime? TestplayAt { get; internal set; }

        [JsonProperty("testplays")]
        private List<BeatmapTestplay>? BeatmapTestplays { get; set; }

        /// <summary>
        /// The testplays associated with this version.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<BeatmapTestplay>? Testplays => BeatmapTestplays?.AsReadOnly();


        // Equality Methods

        public bool Equals(BeatmapVersion? other) => Key == other?.Key;
        public override int GetHashCode() => Key.GetHashCode();
        public override bool Equals(object? obj) => Equals(obj as BeatmapVersion);
        public static bool operator ==(BeatmapVersion left, BeatmapVersion right) => Equals(left, right);
        public static bool operator !=(BeatmapVersion left, BeatmapVersion right) => Equals(left, right);

        public enum VersionState
        {
            Uploaded,
            Testplay,
            Published,
            Feedback
        }
    }
}