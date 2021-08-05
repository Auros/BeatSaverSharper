using Newtonsoft.Json;

namespace BeatSaverSharp.Models
{
    /// <summary>
    /// Contains map detail metadata. The information stored here is inferred from map's Info.dat file.
    /// </summary>
    public class BeatmapMetadata
    {
        /// <summary>
        /// The beats per minute (BPM) of the map, as defined in the map's Info.dat
        /// </summary>
        [JsonProperty("bpm")]
        public float BPM { get; internal set; }

        /// <summary>
        /// The song duration in (beats? seconds).
        /// </summary>
        [JsonProperty("duration")]
        public int Duration { get; internal set; }

        /// <summary>
        /// The name of the song, as defined in the map's Info.dat
        /// </summary>
        [JsonProperty("songName")]
        public string SongName { get; internal set; } = null!;

        /// <summary>
        /// The song's sub name, as defined in the map's Info.dat
        /// </summary>
        [JsonProperty("songSubName")]
        public string SongSubName { get; internal set; } = null!;

        /// <summary>
        /// The name of the song author, as defined in the map's Info.dat
        /// </summary>
        [JsonProperty("songAuthorName")]
        public string SongAuthorName { get; internal set; } = null!;

        /// <summary>
        /// The name of the level author, as defined in the map's Info.dat
        /// </summary>
        [JsonProperty("levelAuthorName")]
        public string LevelAuthorName { get; internal set; } = null!;

        internal BeatmapMetadata() { }
    }
}