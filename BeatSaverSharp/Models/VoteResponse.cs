using Newtonsoft.Json;

namespace BeatMapsSharp.Models
{
    public class VoteResponse
    {
        [JsonProperty("error")]
        public string? Error { get; set; }

        [JsonProperty("Success")]
        public bool Successful { get; set; }
    }
}