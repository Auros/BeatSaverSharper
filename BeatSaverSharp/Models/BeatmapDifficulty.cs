using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace BeatSaverSharp.Models
{
    /// <summary>
    /// A difficulty for a beatmap.
    /// </summary>
    public class BeatmapDifficulty
    {
        /// <summary>
        /// The amount of bombs in the difficulty.
        /// </summary>
        [JsonProperty("bombs")]
        public int Bombs { get; internal set; }

        /// <summary>
        /// The characteristic of the difficulty.
        /// </summary>
        [JsonProperty("characteristic")]
        public BeatmapCharacteristic Characteristic { get; internal set; }

        /// <summary>
        /// Does this difficulty have Chroma support?
        /// </summary>
        [JsonProperty("chroma")]
        public bool Chroma { get; internal set; }

        /// <summary>
        /// Does this difficulty have Cinema support?
        /// </summary>
        [JsonProperty("cinema")]
        public bool Cinema { get; internal set; }

        /// <summary>
        /// The difficulty of this difficulty.
        /// </summary>
        [JsonProperty("difficulty")]
        public BeatSaverBeatmapDifficulty Difficulty { get; internal set; }

        /// <summary>
        /// The amount of mapping events in this difficulty.
        /// </summary>
        [JsonProperty("events")]
        public int Events { get; internal set; }

        /// <summary>
        /// The length of this song.
        /// </summary>
        [JsonProperty("length")]
        public double Length { get; internal set; }

        /// <summary>
        /// Does this difficulty support mapping extensions?
        /// </summary>
        [JsonProperty("me")]
        public bool MappingExtensions { get; internal set; }

        /// <summary>
        /// Does this difficulty support noodle extensions?
        /// </summary>
        [JsonProperty("ne")]
        public bool NoodleExtensions { get; internal set; }

        /// <summary>
        /// The Note Jump Speed of this difficulty.
        /// </summary>
        [JsonProperty("njs")]
        public float NJS { get; internal set; }

        /// <summary>
        /// The amount of notes in this difficulty.
        /// </summary>
        [JsonProperty("notes")]
        public int Notes { get; internal set; }

        /// <summary>
        /// The notes per second of this difficulty.
        /// </summary>
        [JsonProperty("nps")]
        public double NPS { get; internal set; }

        /// <summary>
        /// The amount of obstacles in this difficulty.
        /// </summary>
        [JsonProperty("obstacles")]
        public int Obstacles { get; internal set; }

        /// <summary>
        /// The spawn offset for this difficulty.
        /// </summary>
        [JsonProperty("offset")]
        public float Offset { get; internal set; }

        /// <summary>
        /// Parity information for this difficulty.
        /// </summary>
        [JsonProperty("paritySummary")]
        public BeatmapParitySummary Parity { get; internal set; } = null!;

        /// <summary>
        /// The length of this song in seconds.
        /// </summary>
        [JsonProperty("seconds")]
        public double Seconds { get; internal set; }

        /// <summary>
        /// The star rating of this difficulty.
        /// </summary>
        [JsonProperty("stars")]
        public float? Stars { get; internal set; }

        internal BeatmapDifficulty() { }

        public enum BeatmapCharacteristic
        {
            Standard,
            OneSaber,
            NoArrows,

            [EnumMember(Value = "90Degree")]
            _90Degree,

            [EnumMember(Value = "360Degree")]
            _360Degree,

            Lightshow,
            Lawless,
            Legacy
        }

        public enum BeatSaverBeatmapDifficulty
        {
            Easy,
            Normal,
            Hard,
            Expert,
            ExpertPlus
        }
    }
}
