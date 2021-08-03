namespace BeatMapsSharp.Models
{
    public class BeatmapStats
    {
        /// <summary>
        /// How many times this map was downloaded.
        /// </summary>
        public int Downloads { get; internal set; }

        /// <summary>
        /// How many downvotes this map has.
        /// </summary>
        public int Downvotes { get; internal set; }

        /// <summary>
        /// How many upvotes this map has.
        /// </summary>
        public int Upvotes { get; internal set; }

        /// <summary>
        /// The amount of plays this map has.
        /// </summary>
        public int Plays { get; internal set; }

        /// <summary>
        /// The score/rating of this map, expressed as a normalized float.
        /// This should be treated as a percentage.
        /// </summary>
        public float Score { get; internal set; }
    }
}