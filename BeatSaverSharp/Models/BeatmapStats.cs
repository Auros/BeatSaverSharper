using Newtonsoft.Json;

namespace BeatSaverSharp.Models
{
    public class BeatmapStats
    {
        /// <summary>
        /// The amount of plays this map has.
        /// </summary>
        [JsonProperty("plays")]
        public int Plays { get; internal set; }

        /// <summary>
        /// How many times this map was downloaded.
        /// </summary>
        [JsonProperty("downloads")]
        public int Downloads { get; internal set; }

        /// <summary>
        /// How many upvotes this map has.
        /// </summary>
        [JsonProperty("upvotes")]
        public int Upvotes { get; internal set; }

        /// <summary>
        /// How many downvotes this map has.
        /// </summary>
        [JsonProperty("downvotes")]
        public int Downvotes { get; internal set; }

        /// <summary>
        /// The score/rating of this map, expressed as a normalized float.
        /// This should be treated as a percentage.
        /// </summary>
        [JsonProperty("score")]
        public float Score { get; internal set; }

        internal BeatmapStats() { }
    }
}