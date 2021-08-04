using Newtonsoft.Json;

namespace BeatSaverSharp.Models
{
    public class Vote
    {
        [JsonProperty("auth")]
        internal Auth Validation { get; }

        [JsonProperty("direction")]
        public bool Direction { get; }

        [JsonProperty("hash")]
        public string Hash { get; }

        internal Vote(string levelHash, Type voteType, Platform platform, string platformID, string proof)
        {
            if (platform == Platform.Steam)
            {
                Validation = new Auth { SteamID = platformID, Proof = proof };
            }
            else
            {
                Validation = new Auth { OculusID = platformID, Proof = proof };
            }
            Direction = voteType == Type.Upvote ? true : false;
            Hash = levelHash;
        }

        internal class Auth
        {
            [JsonProperty("oculusId")]
            public string OculusID { get; set; } = string.Empty;

            [JsonProperty("proof")]
            public string Proof { get; set; } = string.Empty;

            [JsonProperty("steamId")]
            public string SteamID { get; set; } = string.Empty;

        }

        public enum Type
        {
            Upvote,
            Downvote
        }

        public enum Platform
        {
            Steam,
            Oculus
        }
    }
}