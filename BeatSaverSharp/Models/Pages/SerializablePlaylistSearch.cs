using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace BeatSaverSharp.Models.Pages
{
    internal class SerializablePlaylistSearch
    {
        [JsonProperty("docs")]
        public ReadOnlyCollection<Playlist> Docs { get; internal set; } = null!;
    }
}