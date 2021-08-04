using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BeatSaverSharp.Models.Pages
{
    internal class SerializableSearch
    {
        [JsonProperty("docs")]
        public ReadOnlyCollection<Beatmap> Docs { get; internal set; } = null!;

        [JsonProperty("user")]
        public User? User { get; internal set; }
    }
}