using Newtonsoft.Json;

namespace BeatSaverSharp.Models
{
    /// <summary>
    /// Contains statistics containing the quantity of maps a user has uploaded.
    /// </summary>
    public class UserDifficultyStats
    {
        /// <summary>
        /// The total amount of maps a user has uploaded.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; internal set; }

        /// <summary>
        /// The amount of easy maps a user has uploaded.
        /// </summary>
        [JsonProperty("easy")]
        public int Easy { get; internal set; }

        /// <summary>
        /// The amount of normal maps a user has uploaded.
        /// </summary>
        [JsonProperty("normal")]
        public int Normal { get; internal set; }

        /// <summary>
        /// The amount of hard maps a user has uploaded.
        /// </summary>
        [JsonProperty("hard")]
        public int Hard { get; internal set; }

        /// <summary>
        /// The amount of expert maps a user has uploaded.
        /// </summary>
        [JsonProperty("expert")]
        public int Expert { get; internal set; }

        /// <summary>
        /// The amount of expert plus maps a user has uploaded.
        /// </summary>
        [JsonProperty("expertPlus")]
        public int ExpertPlus { get; internal set; }

        internal UserDifficultyStats() { }
    }
}