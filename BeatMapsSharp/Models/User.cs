using System;

namespace BeatMapsSharp.Models
{
    /// <summary>
    /// A BeatMaps user.
    /// </summary>
    public class User : IEquatable<User>
    {
        /// <summary>
        /// The avatar URL of the user.
        /// </summary>
        public string Avatar { get; set; } = null!;

        /// <summary>
        /// The hash of the avatar URL.
        /// </summary>
        public string Hash { get; set; } = null!;

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// The unique ID of the user.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The mapping stats of the user.
        /// </summary>
        public UserStats? Stats { get; set; }


        // Equality Methods

        public bool Equals(User? other) => ID == other?.ID;
        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object? obj) => Equals(obj as User);
        public static bool operator ==(User left, User right) => Equals(left, right);
        public static bool operator !=(User left, User right) => Equals(left, right);

    }
}