using Newtonsoft.Json;

namespace BeatSaverSharp.Models
{
    public class VoteResponse
    {
        [JsonProperty("error")]
        public string? Error { get; set; }

        [JsonProperty("Success")]
        public bool Successful { get; set; }

        internal VoteResponse() { }
    }
}