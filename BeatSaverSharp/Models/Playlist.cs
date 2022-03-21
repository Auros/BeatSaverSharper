using System;
using System.Threading;
using System.Threading.Tasks;
using BeatSaverSharp.Models.Pages;
using Newtonsoft.Json;

namespace BeatSaverSharp.Models
{
    public class Playlist : BeatSaverObject, IEquatable<Playlist>
    {
        /// <summary>
        /// The time at which this playlist was created.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; internal set; }
        
        /// <summary>
        /// The time at which this playlist was curated.
        /// </summary>
        [JsonProperty("curatedAt")]
        public DateTime? CuratedAt { get; internal set; }
        
        /// <summary>
        /// The user who curated this map.
        /// </summary>
        [JsonProperty("curator")]
        public User? Curator { get; internal set; }
        
        /// <summary>
        /// The time at which this playlist was deleted.
        /// </summary>
        [JsonProperty("deletedAt")]
        public DateTime? DeletedAt { get; internal set; }
        
        /// <summary>
        /// The description of the playlist.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; } = null!;
        
        /// <summary>
        /// The name of the playlist.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; } = null!;
        
        /// <summary>
        /// The owner of the playlist.
        /// </summary>
        [JsonProperty("owner")]
        public User Owner { get; internal set; } = null!;
        
        /// <summary>
        /// The ID of the playlist.
        /// </summary>
        [JsonProperty("playlistId")]
        public int ID { get; internal set; }
        
        /// <summary>
        /// The relative URL of where the cover image is stored.
        /// </summary>
        [JsonProperty("playlistImage")]
        public string CoverURL { get; internal set; } = null!;
        
        /// <summary>
        /// The relative URL of where to download this playlist
        /// </summary>
        [JsonProperty("downloadURL")]
        public string DownloadURL { get; internal set; } = null!;
        
        /// <summary>
        /// If the playlist is public or not.
        /// </summary>
        [JsonProperty("public")]
        public bool Public { get; internal set; }
        
        /// <summary>
        /// The last time one of the songs in the playlist was updated.
        /// </summary>
        [JsonProperty("songsChangedAt")]
        public DateTime? SongsChangedAt { get; internal set; }
        
        /// <summary>
        /// The time at which the playlist was last updated.
        /// </summary>
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; internal set; }

        public Task<PlaylistDetail?> GetPlaylistDetail(CancellationToken token = default, int page = 0, bool skipCacheCheck = false) => Client.Playlist(ID, token, page, skipCacheCheck);

        public Task<byte[]?> DownloadPlaylist(CancellationToken token = default, IProgress<double>? progress = null) => Client.DownloadPlaylist(this, token, progress);

        public Task<byte[]?> DownloadCoverImage(CancellationToken token = default, IProgress<double>? progress = null) => Client.DownloadCoverImage(this, token, progress);

        #region Equality

        public bool Equals(Playlist? other) => ID == other?.ID;

        public override bool Equals(object? obj) => Equals(obj as Playlist);

        public override int GetHashCode() => ID.GetHashCode();

        #endregion
    }
}