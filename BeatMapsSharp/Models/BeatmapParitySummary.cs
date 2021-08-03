namespace BeatMapsSharp.Models
{
    /// <summary>
    /// Contains parity information of a beatmap.
    /// </summary>
    public class BeatmapParitySummary
    {
        /// <summary>
        /// The amount of errors in the map.
        /// </summary>
        public int Errors { get; set; }

        /// <summary>
        /// The amount of resets in the map.
        /// </summary>
        public int Resets { get; set; }

        /// <summary>
        /// The amount of warnings in the map.
        /// </summary>
        public int Warns { get; set; }
    }
}