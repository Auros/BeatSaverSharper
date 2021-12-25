using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BeatSaverSharp.Tests.TestHelper;
using System.Threading.Tasks;

namespace BeatSaverSharp.Tests
{
    [TestClass]
    public class ByHashTests
    {
        [TestMethod]
        public async Task TestSingleHash()
        {
            // Kanaria - Envy Baby (Will Stetson Cover)
            //  CoolingCloset & Nolanimations
            //  April 14th, 2021
            var envyBaby = await Client.BeatmapByHash("4529498dab9d920c1e7a3ba46507b677655b3ad7");
            Assert.IsNotNull(envyBaby);
            Assert.IsNotNull(envyBaby.Uploader);

            // Users stats should NOT be loaded through a beatmap fetch call.
            Assert.IsNull(envyBaby.Uploader.Stats);
        }

        [TestMethod]
        public async Task TestMultiHash()
        {
            var results = await Client.BeatmapByHash(
                "4529498dab9d920c1e7a3ba46507b677655b3ad7",
                "701587ce2d1e502f8352c3cfd0627c1844b0ff0a"
            );

            // Kanaria - Envy Baby (Will Stetson Cover)
            //  CoolingCloset & Nolanimations
            //  April 14th, 2021
            var envyBaby = results["4529498dab9d920c1e7a3ba46507b677655b3ad7"];
            Assert.IsNotNull(envyBaby);
            Assert.IsNotNull(envyBaby.Uploader);

            // Users stats should NOT be loaded through a beatmap fetch call.
            Assert.IsNull(envyBaby.Uploader.Stats);

            // EGOIST - RELOADED (Short Ver.)
            //  CoolingCloset
            //  January 18th, 2021
            var reloaded = results["701587ce2d1e502f8352c3cfd0627c1844b0ff0a"];
            Assert.IsNotNull(reloaded);
            Assert.IsNotNull(reloaded.Uploader);
            Assert.IsNull(reloaded.Uploader.Stats);
        }
    }
}