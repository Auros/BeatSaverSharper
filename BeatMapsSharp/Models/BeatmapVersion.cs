using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace BeatMapsSharp.Models
{
    /// <summary>
    /// Represents a specific version of a Beatmap
    /// </summary>
    public class BeatmapVersion : BeatSaverObject, IEquatable<BeatmapVersion>
    {
        /// <summary>
        /// The time this version was creeated.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; internal set; }

        /// <summary>
        /// The feedback for this version.
        /// </summary>
        [JsonProperty("feedback")]
        public string? Feedback { get; internal set; }

        /// <summary>
        /// The sage score.
        /// </summary>
        [JsonProperty("sageScore")]
        public short SageScore { get; internal set; }

        /// <summary>
        /// The map's hash.
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; internal set; } = null!;

        /// <summary>
        /// The map's key.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; internal set; } = null!;

        /// <summary>
        /// The state of this version.
        /// </summary>
        [JsonProperty("state")]
        public VersionState State { get; internal set; }

        /// <summary>
        /// The time at which the most recent testplay was set for this version.
        /// </summary>
        [JsonProperty("testplayAt")]
        public DateTime? TestplayAt { get; internal set; }

        /// <summary>
        /// The testplays associated with this version.
        /// </summary>
        [JsonProperty("testplays")]
        public ReadOnlyCollection<BeatmapTestplay>? Testplays { get; internal set; }

        /// <summary>
        /// The relative URL of where the cover image is stored.
        /// </summary>
        [JsonProperty("coverURL")]
        public string CoverURL { get; internal set; } = null!;

        /// <summary>
        /// The difficulties in this map.
        /// </summary>
        [JsonProperty("diffs")]
        public BeatmapDifficulty Difficulties { get; internal set; } = null!;

        /// <summary>
        /// The relative URL of where to download this version.
        /// </summary>
        [JsonProperty("downloadURL")]
        public string DownloadURL { get; internal set; } = null!;


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