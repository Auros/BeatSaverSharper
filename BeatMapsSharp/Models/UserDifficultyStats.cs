namespace BeatMapsSharp.Models
{
    /// <summary>
    /// Contains statistics containing the quantity of maps a user has uploaded.
    /// </summary>
    public class UserDifficultyStats
    {
        /// <summary>
        /// The amount of easy maps a user has uploaded.
        /// </summary>
        public int Easy { get; internal set; }

        /// <summary>
        /// The amount of normal maps a user has uploaded.
        /// </summary>
        public int Normal { get; internal set; }

        /// <summary>
        /// The amount of hard maps a user has uploaded.
        /// </summary>
        public int Hard { get; internal set; }

        /// <summary>
        /// The amount of expert maps a user has uploaded.
        /// </summary>
        public int Expert { get; internal set; }

        /// <summary>
        /// The amount of expert plus maps a user has uploaded.
        /// </summary>
        public int ExpertPlus { get; internal set; }

        /// <summary>
        /// The total amount of maps a user has uploaded.
        /// </summary>
        public int Total { get; internal set; }
    }
}