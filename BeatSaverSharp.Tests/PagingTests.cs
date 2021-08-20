using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using static BeatSaverSharp.Tests.TestHelper;

namespace BeatSaverSharp.Tests
{
    [TestClass]
    public class PagingTests
    {
        [TestMethod]
        public async Task TestPaging()
        {
            var ratingPage = await Client.SearchBeatmaps(SearchTextFilterOption.Rating);
            Assert.IsNotNull(ratingPage);

            // There better be beatmaps on the rating page.
            Assert.IsFalse(ratingPage.Empty);

            var nextPage = await ratingPage.Next();
            Assert.IsNotNull(nextPage);

            // And there BETTER be beatmaps on the next page.
            Assert.IsFalse(ratingPage.Empty);

            // Now we check to see if they're in the right order
            Assert.IsTrue(ratingPage.Beatmaps[ratingPage.Beatmaps.Count - 1].Stats.Score >= nextPage.Beatmaps[0].Stats.Score);

            // And now we test is the previous page is the same as the first.
            var previousPage = await nextPage.Previous();
            CollectionAssert.AreEqual(ratingPage.Beatmaps.ToList(), previousPage.Beatmaps.ToList());
        }

        [TestMethod]
        public async Task TestLatest()
        {
            var latestPage = await Client.LatestBeatmaps();
            Assert.IsNotNull(latestPage);

            // There better be beatmaps on the latest page.
            Assert.IsFalse(latestPage.Empty);

            var nextPage = await latestPage.Next();
            Assert.IsNotNull(nextPage);

            // And there BETTER be beatmaps on the next page.
            Assert.IsFalse(latestPage.Empty);

            // Now we check to see if they're in the right order
            Assert.IsTrue(latestPage.Beatmaps[latestPage.Beatmaps.Count - 1].Uploaded > nextPage.Beatmaps[0].Uploaded);

            // And now we test is the previous page is the same as the first.
            var previousPage = await nextPage.Previous();
            CollectionAssert.AreEqual(latestPage.Beatmaps.ToList(), previousPage.Beatmaps.ToList());
        }

        [TestMethod]
        public async Task TestUsers()
        {
            var joetasticBeatmaps = await Client.UploaderBeatmaps(58338);

            Assert.IsNotNull(joetasticBeatmaps);
            var nextPage = await joetasticBeatmaps.Next();
            Assert.IsNotNull(nextPage);
            CollectionAssert.AreNotEqual(joetasticBeatmaps.Beatmaps.ToList(), nextPage.Beatmaps.ToList());
        }
    }
}