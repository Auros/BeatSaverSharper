namespace BeatMapsSharp.Models
{
    public class BeatmapStats
    {
        /// <summary>
        /// How many times this map was downloaded.
        /// </summary>
        public int Downloads { get; set; }

        /// <summary>
        /// How many downvotes this map has.
        /// </summary>
        public int Downvotes { get; set; }

        /// <summary>
        /// How many upvotes this map has.
        /// </summary>
        public int Upvotes { get; set; }

        /// <summary>
        /// The amount of plays this map has.
        /// </summary>
        public int Plays { get; set; }

        /// <summary>
        /// The score/rating of this map, expressed as a normalized float.
        /// This should be treated as a percentage.
        /// </summary>
        public float Score { get; set; }
    }
}