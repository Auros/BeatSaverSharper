using System.Collections.Generic;

namespace BeatSaverSharp.Models.Pages
{
    public abstract class PageBase<T> : BeatSaverObject
    {
        public bool Empty => Items.Count is 0;
        protected IReadOnlyList<T> Items { get; }
        protected PageBase(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}