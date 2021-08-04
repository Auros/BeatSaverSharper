using System;

namespace BeatSaverSharp.Tests
{

    public class TestHelper
    {
        public static BeatSaver Client { get; } = new(new BeatSaverOptions("BSXTests", new Version(1, 0, 0)));
    }
}