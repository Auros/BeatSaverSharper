using System;
using System.IO;
using System.Threading.Tasks;

// This project is a development project as I work on BeatSaverSharp v3.
// It should be deleted upon release, if you are reading this, I haven't
// deleted it for some reason, and you should remind me to delete it.
namespace BeatSaverSharp.Playground
{
    public class Program
    {
        public static async Task Main(string[] ___)
        {
            using BeatSaver beatSaver = new("BeatSaverSharp.Playground", Version.Parse("3.0.0"));

            // Toxic Violet Cubes
            //  BSWC Team (uploaded by abcbadq)
            //  August 1st, 2021
            var toxicVioletCubes = await beatSaver.Beatmap("1ad2b");
            _ = toxicVioletCubes;

            // Now we get the user directly
            var abcbadq = await beatSaver.User(390);
            _ = abcbadq;
            _ = toxicVioletCubes;

            /*var map = await beatSaver.Beatmap("28c5");
            Console.WriteLine(map.LatestVersion.Hash);
            var ver = await map.LatestVersion.DownloadPreview(progress: new ConsoleWriteLineProgress());
            await File.WriteAllBytesAsync("C:\\Users\\Auros\\Desktop\\ns.mp3", ver);
            Console.ReadLine();*/
        }

        public class ConsoleWriteLineProgress : IProgress<double>
        {
            public void Report(double value)
            {
                Console.WriteLine(value.ToString("P0"));
            }
        }
    }
}