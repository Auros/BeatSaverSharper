using System;

namespace BeatSaverSharp
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class QueryKeyNameAttribute : Attribute
    {
        public string Name { get; }

        public QueryKeyNameAttribute(string name)
        {
            Name = name;
        }
    }
}