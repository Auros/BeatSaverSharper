using Newtonsoft.Json;
using System;

namespace BeatMapsSharp.Models
{
    /// <summary>
    /// A BeatMaps user.
    /// </summary>
    public class User : BeatSaverObject, IEquatable<User>
    {
        /// <summary>
        /// The avatar URL of the user.
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; internal set; } = null!;

        /// <summary>
        /// The hash of the avatar URL.
        /// </summary>
        [JsonProperty("hash")]
        public string? Hash { get; internal set; } = null!;

        /// <summary>
        /// The name of the user.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; } = null!;

        /// <summary>
        /// The unique ID of the user.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; internal set; }

        /// <summary>
        /// The mapping stats of the user.
        /// </summary>
        [JsonProperty("stats")]
        public UserStats? Stats { get; internal set; }


        // Equality Methods

        public bool Equals(User? other) => ID == other?.ID;
        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object? obj) => Equals(obj as User);
        public static bool operator ==(User left, User right) => Equals(left, right);
        public static bool operator !=(User left, User right) => Equals(left, right);

    }
}