namespace BeatMapsSharp.Models
{
    /// <summary>
    /// A BeatMaps user.
    /// </summary>
    public class User
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
    }
}