using System;
using System.Collections.Generic;

namespace BeatMapsSharp.Models
{
    /// <summary>
    /// A BeatMaps beatmap.
    /// </summary>
    public class Beatmap
    {
        /// <summary>
        /// Was this map made by an auto-mapper?
        /// </summary>
        public bool Automapper { get; set; }

        /// <summary>
        /// The person who curated this map.
        /// </summary>
        public string? Curator { get; set; }

        /// <summary>
        /// The description of the map.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// The ID, or "key" of the map.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The metadata for this map.
        /// </summary>
        public BeatmapMetadata Metadata { get; set; } = null!;

        /// <summary>
        /// The name (or title) of the map.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Is this map qualified on ScoreSaber?
        /// </summary>
        public bool Qualified { get; set; }

        /// <summary>
        /// Is this map ranked on ScoreSaber?
        /// </summary>
        public bool Ranked { get; set; }

        /// <summary>
        /// The stats for this map.
        /// </summary>
        public BeatmapStats Stats { get; set; } = null!;

        /// <summary>
        /// The time at which this map was uploaded.
        /// </summary>
        public DateTime Uploaded { get; set; }

        /// <summary>
        /// The uploader of this map.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// The versions of this map.
        /// </summary>
        public IReadOnlyList<BeatmapVersion> Versions { get; set; } = null!;
    }
}