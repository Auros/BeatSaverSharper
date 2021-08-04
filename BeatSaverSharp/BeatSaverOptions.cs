using System;

namespace BeatSaverSharp
{
    public class BeatSaverOptions
    {
        public Version Version { get; set; }
        public string ApplicationName { get; set; }
        public Uri BeatSaverAPI { get; set; } = new Uri("https://api.beatsaver.com/");
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

        public BeatSaverOptions(string applicationName, string version) : this(applicationName, Version.Parse(version))
        {

        }

        public BeatSaverOptions(string applicationName, Version version)
        {
            Version = version;
            ApplicationName = applicationName;
        }
    }
}