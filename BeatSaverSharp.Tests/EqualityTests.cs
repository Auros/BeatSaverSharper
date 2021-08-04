using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BeatSaverSharp.Tests.TestHelper;
using System.Threading.Tasks;

namespace BeatSaverSharp.Tests
{
    [TestClass]
    public class EqualityTests
    {
        [TestMethod]
        public async Task BeatmapEqualityTest()
        {
            // overcooked
            //  lolPants
            //  May 11th, 2019
            var overcookedByKey = await Client.Beatmap("4c19");
            Assert.IsNotNull(overcookedByKey);

            var overcookedByHash = await Client.BeatmapByHash("64ec3d5b0f9239a56d1e709daf29dfa00a42cbef");
            Assert.IsNotNull(overcookedByHash);

            Assert.AreEqual(overcookedByKey, overcookedByHash);
        }

        [TestMethod]
        public async Task Denyah()
        {
            var denyahByName = await Client.User("denyah_");
            Assert.IsNotNull(denyahByName);

            var denyahByID = await Client.User(1834);
            Assert.IsNotNull(denyahByID);

            Assert.AreEqual(denyahByID, denyahByName);
        }
    }
}