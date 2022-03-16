using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models
{
    /// <summary>
    /// A BeatSaver beatmap.
    /// </summary>
    public class Beatmap : BeatSaverObject, IEquatable<Beatmap>
    {
        /// <summary>
        /// The ID of the map.
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; internal set; } = null!;

        /// <summary>
        /// The name (or title) of the map.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; } = null!;

        /// <summary>
        /// The description of the map.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; } = null!;

        /// <summary>
        /// The uploader of this map.
        /// </summary>
        [JsonProperty("uploader")]
        public User Uploader { get; internal set; } = null!;

        /// <summary>
        /// The metadata for this map.
        /// </summary>
        [JsonProperty("metadata")]
        public BeatmapMetadata Metadata { get; internal set; } = null!;

        /// <summary>
        /// The stats for this map.
        /// </summary>
        [JsonProperty("stats")]
        public BeatmapStats Stats { get; internal set; } = null!;

        /// <summary>
        /// The tags (genres) for this map.
        /// </summary>
        [JsonProperty("tags")]
        public ReadOnlyCollection<string> Tags { get; internal set; } = null!;

        /// <summary>
        /// The time at which this map was uploaded.
        /// </summary>
        [JsonProperty("uploaded")]
        public DateTime Uploaded { get; internal set; }

        /// <summary>
        /// Was this map made by an auto-mapper?
        /// </summary>
        [JsonProperty("automapper")]
        public bool Automapper { get; internal set; }

        /// <summary>
        /// Is this map ranked on ScoreSaber?
        /// </summary>
        [JsonProperty("ranked")]
        public bool Ranked { get; internal set; }

        /// <summary>
        /// Is this map qualified on ScoreSaber?
        /// </summary>
        [JsonProperty("qualified")]
        public bool Qualified { get; internal set; }

        /// <summary>
        /// The versions of this map.
        /// </summary>
        [JsonProperty("versions")]
        public ReadOnlyCollection<BeatmapVersion> Versions { get; internal set; } = null!;

        /// <summary>
        /// The person who curated this map.
        /// </summary>
        [Obsolete("The Curator model is obsolete. Please use .BeatmapCurator.")]
        public Curator? Curator { get; internal set; }
        
        /// <summary>
        /// The time at which this playlist was curated.
        /// </summary>
        [JsonProperty("curatedAt")]
        public DateTime CuratedAt { get; internal set; }
        
        /// <summary>
        /// The time at which this map was deleted.
        /// </summary>
        [JsonProperty("deletedAt")]
        public DateTime? DeletedAt { get; internal set; }

        [JsonProperty("curator")]
        public User? BeatmapCurator { get; internal set; }

        private BeatmapVersion? _latestVersion;

        // Fetches the latest version ordered by creation date.
        // If there's only one version, that version becomes the latest.
        [JsonIgnore]
        public BeatmapVersion LatestVersion
        {
            get
            {
                if (_latestVersion is null)
                {
                    if (Versions.Count == 1)
                    {
                        _latestVersion = Versions[0];
                        return _latestVersion;
                    }

                    BeatmapVersion? latest = null;
                    for (int i = 0; i < Versions.Count; i++)
                    {
                        var active = Versions[i];
                        if (latest is null)
                        {
                            latest = active;
                        }
                        else
                        {
                            if (active.CreatedAt > latest.CreatedAt)
                            {
                                latest = active;
                            }
                        }
                    }
                    _latestVersion = latest;
                }
                return _latestVersion!;
            }
            internal set
            {
                _latestVersion = value;
            }
        }

        internal Beatmap() { }

        public Task Refresh(CancellationToken token = default)
        {
            return Client.Beatmap(ID, token, true);
        }

        // Equality Methods

        public bool Equals(Beatmap? other) => ID == other?.ID;
        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object? obj) => Equals(obj as Beatmap);
        public static bool operator ==(Beatmap? left, Beatmap? right) => Equals(left, right);
        public static bool operator !=(Beatmap? left, Beatmap? right) => !Equals(left, right);

    }
}