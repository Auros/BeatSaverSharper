using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BeatMapsSharp.Models.Pages
{
    public class SerializablePage
    {
        [JsonProperty("docs")]
        public ReadOnlyCollection<Beatmap> Docs { get; internal set; } = null!;
    }
}