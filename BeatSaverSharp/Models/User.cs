using BeatSaverSharp.Models.Pages;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaverSharp.Models
{
    /// <summary>
    /// A BeatSaver user.
    /// </summary>
    public class User : BeatSaverObject, IEquatable<User>
    {
        /// <summary>
        /// The unique ID of the user.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; internal set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; } = null!;

        /// <summary>
        /// The hash of the avatar URL.
        /// </summary>
        [JsonProperty("hash")]
        public string? Hash { get; internal set; } = null!;

        /// <summary>
        /// The avatar URL of the user.
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; internal set; } = null!;

        /// <summary>
        /// Is this user a curator?
        /// </summary>
        [JsonProperty("curator")]
        public bool Curator { get; internal set; }

        /// <summary>
        /// User account type.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; internal set; } = null!;

        /// <summary>
        /// The mapping stats of the user.
        /// </summary>
        [JsonProperty("stats")]
        public UserStats? Stats { get; internal set; }

        internal User() { }

        public Task Refresh(CancellationToken token = default)
        {
            return Client.User(ID, token, true);
        }

        /// <summary>
        /// Gets the beatmaps uploaded by the user.
        /// </summary>
        /// <param name="page">The page to get/</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<Page?> Beatmaps(int page = 0, CancellationToken token = default)
        {
            return Client.UploaderBeatmaps(ID, page, token);
        }

        // Equality Methods

        public bool Equals(User? other) => ID == other?.ID;
        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object? obj) => Equals(obj as User);
        public static bool operator ==(User? left, User? right) => Equals(left, right);
        public static bool operator !=(User? left, User? right) => !Equals(left, right);

    }
}