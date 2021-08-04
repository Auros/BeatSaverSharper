using BeatMapsSharp;
using BeatMapsSharp.Models.Pages;
using Newtonsoft.Json;
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
        public static async Task Main(string[] args)
        {
            using BeatSaver beatSaver = new();
            
            var page = await beatSaver.Latest();
            _ = page;
            var page2 = await page.Next();
            _ = page2;
            var page3 = await page2.Next();
            _ = page3;
            var page2Again = await page3.Previous();
            _ = page2Again;
            
        }
    }
}