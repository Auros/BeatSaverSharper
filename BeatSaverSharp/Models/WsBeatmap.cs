using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BeatSaverSharp.Models
{
    public class WsBeatmap
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")] 
        public WsMessageType Type { get; set; }

        [JsonProperty("msg")] 
        public Beatmap Map { get; set; } = null!;
    }

    public enum WsMessageType
    {
        MAP_UPDATE,
        MAP_DELETE
    }
}