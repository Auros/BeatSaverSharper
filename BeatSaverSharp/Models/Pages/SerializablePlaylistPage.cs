using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace BeatSaverSharp.Models.Pages
{
    internal class SerializablePlaylistPage
    {
        [JsonProperty("maps")]
        public ReadOnlyCollection<OrderedBeatmap> Beatmaps { get; internal set; } = null!;
        
        [JsonProperty("playlist")]
        public Playlist Playlist { get; internal set; } = null!;
    }
}