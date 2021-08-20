using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BeatSaverSharp.Tests.TestHelper;
using System.Threading.Tasks;

namespace BeatSaverSharp.Tests
{
    [TestClass]
    public class CachingTests
    {
        [TestMethod]
        public async Task TestSmartUserCachingImplied()
        {
            // Kanaria - Envy Baby (Will Stetson Cover)
            //  CoolingCloset & Nolanimations
            //  April 14th, 2021
            var envyBaby = await Client.Beatmap("16ac1");
            Assert.IsNotNull(envyBaby);
            Assert.IsNotNull(envyBaby.Uploader);

            // Users stats should NOT be loaded through a beatmap fetch call.
            Assert.IsNull(envyBaby.Uploader.Stats);

            // EGOIST - RELOADED (Short Ver.)
            //  CoolingCloset
            //  January 18th, 2021
            var reloaded = await Client.Beatmap("12dc3");
            Assert.IsNotNull(reloaded);
            Assert.IsNotNull(reloaded.Uploader);
            Assert.IsNull(reloaded.Uploader.Stats);

            // Now we load the user in RELOADED
            await reloaded.Uploader.Refresh();

            // And we check if the stats field is populated in RELOADED.
            Assert.IsNotNull(reloaded.Uploader.Stats);
            // As well as in Envy Baby
            Assert.IsNotNull(envyBaby.Uploader.Stats);
        }

        [TestMethod]
        public async Task TestSmartUserCachingBeatmapDirect()
        {
            // Toxic Violet Cubes
            //  BSWC Team (uploaded by abcbadq) (good night abc!!!)
            //  August 1st, 2021
            var toxicVioletCubes = await Client.Beatmap("1ad2b");
            Assert.IsNotNull(toxicVioletCubes);
            Assert.IsNotNull(toxicVioletCubes.Uploader);
            Assert.IsNull(toxicVioletCubes.Uploader.Stats);

            // Now we get the user directly
            var abcbadq = await Client.User(4284943);
            Assert.IsNotNull(abcbadq);
            Assert.IsNotNull(abcbadq.Stats);

            // And the user stats property from Toxic Violet Cubes should have been updated
            Assert.IsNotNull(toxicVioletCubes.Uploader.Stats);
        }

        [TestMethod]
        public async Task TestSmartUserCachingUserInverseDirect()
        {
            // Load by ID, which also loads the user stats.
            var reaxtWeAreBack = await Client.User(4235136);
            Assert.IsNotNull(reaxtWeAreBack);
            Assert.IsNotNull(reaxtWeAreBack.Stats);

            // Now we search by username
            var reaxtBackFromWhere = await Client.User("Reaxt");
            // And its user stats should NOT be null since we loaded reaxt (we are back back from where back from paradise warfare) explicitly earlier.
            Assert.IsNotNull(reaxtBackFromWhere);
            Assert.IsNotNull(reaxtBackFromWhere.Stats);
        }
    }
}