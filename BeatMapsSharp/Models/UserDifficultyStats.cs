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
        public int Easy { get; set; }

        /// <summary>
        /// The amount of normal maps a user has uploaded.
        /// </summary>
        public int Normal { get; set; }

        /// <summary>
        /// The amount of hard maps a user has uploaded.
        /// </summary>
        public int Hard { get; set; }

        /// <summary>
        /// The amount of expert maps a user has uploaded.
        /// </summary>
        public int Expert { get; set; }

        /// <summary>
        /// The amount of expert plus maps a user has uploaded.
        /// </summary>
        public int ExpertPlus { get; set; }

        /// <summary>
        /// The total amount of maps a user has uploaded.
        /// </summary>
        public int Total { get; set; }
    }
}