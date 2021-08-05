using Newtonsoft.Json;

namespace BeatSaverSharp.Models
{
    /// <summary>
    /// Contains parity information of a beatmap.
    /// </summary>
    public class BeatmapParitySummary
    {
        /// <summary>
        /// The amount of errors in the map.
        /// </summary>
        [JsonProperty("errors")]
        public int Errors { get; internal set; }

        /// <summary>
        /// The amount of resets in the map.
        /// </summary>
        [JsonProperty("resets")]
        public int Resets { get; internal set; }

        /// <summary>
        /// The amount of warnings in the map.
        /// </summary>
        [JsonProperty("warns")]
        public int Warns { get; internal set; }

        internal BeatmapParitySummary() { }
    }
}