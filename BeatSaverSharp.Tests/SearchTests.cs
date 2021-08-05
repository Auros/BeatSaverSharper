using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BeatSaverSharp.Tests.TestHelper;
using System.Threading.Tasks;
using System.Linq;

namespace BeatSaverSharp.Tests
{
    [TestClass]
    public class SearchTests
    {
        /*[TestMethod]
        public async Task AutomappersAreCringe()
        {
            var automappersBad = await Client.SearchBeatmaps(new SearchTextFilterOption
            {
                IncludeAutomappers = null
            });

            Assert.IsNotNull(automappersBad);

            foreach (var map in automappersBad.Beatmaps)
            {
                Assert.IsFalse(map.Automapper);
            }
        }

        [TestMethod]
        public async Task ILoveAutomappers()
        {
            var iLoveAutomappers = await Client.SearchBeatmaps(new SearchTextFilterOption
            {
                IncludeAutomappers = false,
            });

            Assert.IsNotNull(iLoveAutomappers);

            foreach (var map in iLoveAutomappers.Beatmaps)
            {
                Assert.IsTrue(map.Automapper);
            }
        }*/

        [TestMethod]
        public async Task NoodleExtensionsAndChromaOnly()
        {
            var noodleExtensions = await Client.SearchBeatmaps(new SearchTextFilterOption
            {
                NoodleExtensions = true,
                Chroma = true
            });
            
            Assert.IsNotNull(noodleExtensions);

            foreach (var map in noodleExtensions.Beatmaps)
            {
                Assert.IsTrue(map.LatestVersion.Difficulties.Any(d => d.NoodleExtensions && d.Chroma));
            }
        }

        [TestMethod]
        public async Task BoomerSaber()
        {
            var release = new System.DateTime(2018, 5, 1);
            var endOf2018 = new System.DateTime(2018, 12, 31);
            var boomerMaps = await Client.SearchBeatmaps(new SearchTextFilterOption
            {
                From = release,
                To = endOf2018,
            });

            Assert.IsNotNull(boomerMaps);

            foreach (var map in boomerMaps.Beatmaps)
            {
                Assert.IsTrue(map.Uploaded >= release && endOf2018 >= map.Uploaded);
            }
        }

        [TestMethod]
        public async Task RankAndy()
        {
            var rankedMaps = await Client.SearchBeatmaps(new SearchTextFilterOption
            {
                Ranked = true
            });

            Assert.IsNotNull(rankedMaps);

            foreach (var map in rankedMaps.Beatmaps)
            {
                Assert.IsTrue(map.Ranked);
            }
        }
    }
}