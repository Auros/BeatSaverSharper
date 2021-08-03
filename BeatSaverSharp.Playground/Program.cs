using BeatMapsSharp;
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

            var makeAWish = await beatSaver.BeatmapByHash("e8c771635e9f2eaad953972a7453bb3469e86038");
            _ = makeAWish;

            makeAWish = await beatSaver.Beatmap("1a32d");
            _ = makeAWish;
        }
    }
}