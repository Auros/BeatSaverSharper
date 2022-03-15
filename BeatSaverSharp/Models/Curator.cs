using System;
using Newtonsoft.Json;

namespace BeatSaverSharp.Models
{
    [Obsolete("The Curator model is obsolete and has been replaced with the normal User model.")]
    public class Curator : IEquatable<Curator>
    {
        /// <summary>
        /// Id of the curator.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; internal set; }

        /// <summary>
        /// Name of the curator.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; } = null!;

        /// <summary>
        /// Link of the avatar of the curator.
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; internal set; } = null!;

        /// <summary>
        /// User account type of the curator.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; internal set; } = null!;

        internal Curator() { }
        
        // Equality Methods

        public bool Equals(Curator? other) => Id == other?.Id;
        public override int GetHashCode() => Id.GetHashCode();
        public override bool Equals(object? obj) => Equals(obj as Curator);
        public static bool operator ==(Curator left, Curator right) => Equals(left, right);
        public static bool operator !=(Curator left, Curator right) => !Equals(left, right);
    }
}