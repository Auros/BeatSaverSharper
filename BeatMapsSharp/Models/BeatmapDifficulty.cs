using Newtonsoft.Json;

namespace BeatMapsSharp.Models
{
    /// <summary>
    /// A difficulty for a beatmap.
    /// </summary>
    public class BeatmapDifficulty
    {
        /// <summary>
        /// The amount of bombs in the difficulty.
        /// </summary>
        public int Bombs { get; set; }
        
        /// <summary>
        /// The characteristic of the difficulty.
        /// </summary>
        public BeatmapCharacteristic Characteristic { get; set; }
        
        /// <summary>
        /// Does this difficulty have Chroma support?
        /// </summary>
        public bool Chroma { get; set; }

        /// <summary>
        /// Does this difficulty have Cinema support?
        /// </summary>
        public bool Cinema { get; set; }

        /// <summary>
        /// The difficulty of this difficulty.
        /// </summary>
        public _BeatmapDifficulty Difficulty { get; set; }

        /// <summary>
        /// The amount of mapping events in this difficulty.
        /// </summary>
        public int Events { get; set; }

        /// <summary>
        /// The length of this song.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Does this difficulty support mapping extensions?
        /// </summary>
        [JsonProperty("me")]
        public bool MappingExtensions { get; set; }

        /// <summary>
        /// Does this difficulty support noodle extensions?
        /// </summary>
        [JsonProperty("ne")]
        public bool NoodleExtensions { get; set; }

        /// <summary>
        /// The Note Jump Speed of this difficulty.
        /// </summary>
        public float NJS { get; set; }

        /// <summary>
        /// The amount of notes in this difficulty.
        /// </summary>
        public int Notes { get; set; }

        /// <summary>
        /// The notes per second of this difficulty.
        /// </summary>
        public double NPS { get; set; }

        /// <summary>
        /// The amount of obstacles in this difficulty.
        /// </summary>
        public int Obstacles { get; set; }

        /// <summary>
        /// The spawn offset for this difficulty.
        /// </summary>
        public float Offset { get; set; }

        /// <summary>
        /// Parity information for this difficulty.
        /// </summary>
        [JsonProperty("paritySummary")]
        public BeatmapParitySummary Parity { get; set; } = null!;

        /// <summary>
        /// The length of this song in seconds.
        /// </summary>
        public double Seconds { get; set; }

        /// <summary>
        /// The star rating of this difficulty.
        /// </summary>
        public float? Stars { get; set; }

        public enum BeatmapCharacteristic
        {
            Standard,
            OneSaber,
            NoArrows,
            _90Degree,
            _360Degree,
            Lightshow,
            Lawless
        }

        public enum _BeatmapDifficulty
        {
            Easy,
            Normal,
            Hard,
            Expert,
            ExpertPlus
        }
    }
}