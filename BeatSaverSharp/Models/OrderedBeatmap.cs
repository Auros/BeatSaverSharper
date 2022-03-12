using System;
using Newtonsoft.Json;

namespace BeatSaverSharp.Models
{
    public class OrderedBeatmap : BeatSaverObject, IEquatable<OrderedBeatmap>, IComparable<OrderedBeatmap>
    {
        [JsonProperty("map")]
        public Beatmap Map { get; internal set; } = null!;
        
        /// <summary>
        /// The order of the beatmap in the playlist.
        /// </summary>
        [JsonProperty("order")]
        public float Order { get; internal set; }
        
        public OrderedBeatmap(){}

        public OrderedBeatmap(Beatmap map, float order)
        {
            Map = map;
            Order = order;
        }
        
        #region Equality & Comparison

        public bool Equals(OrderedBeatmap? other) => Map.Equals(other?.Map);

        public override bool Equals(object? obj) => Equals(obj as OrderedBeatmap);

        public override int GetHashCode() => Map.GetHashCode();
        
        public int CompareTo(OrderedBeatmap? other) => other == null ? 1 : Order.CompareTo(other.Order);
        
        #endregion
    }
}