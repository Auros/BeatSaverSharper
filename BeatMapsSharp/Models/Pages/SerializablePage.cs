using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BeatMapsSharp.Models.Pages
{
    internal class SerializablePage
    {
        [JsonProperty("docs")]
        public ReadOnlyCollection<Beatmap> Docs { get; internal set; } = null!;
    }
}