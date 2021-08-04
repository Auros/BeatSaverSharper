using BeatMapsSharp;
using System;
using System.Threading.Tasks;

// This project is a development project as I work on BeatSaverSharp v3.
// It should be deleted upon release, if you are reading this, I haven't
// deleted it for some reason, and you should remind me to delete it.
namespace BeatSaverSharp.Playground
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using BeatSaver beatSaver = new();

            var oddloop = await beatSaver.UploaderBeatmaps(1071);
            var oddloop2 = await oddloop.Next();
            var oddloopPrevious = await oddloop2.Previous();
            Console.WriteLine(oddloop.Beatmaps[0].Name);
            Console.WriteLine(oddloop2.Beatmaps[0].Name);
            Console.WriteLine(oddloopPrevious.Beatmaps[0].Name);
        }
    }
}