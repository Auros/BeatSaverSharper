using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BeatSaverSharp.Tests.TestHelper;

namespace BeatSaverSharp.Tests
{
    [TestClass]
    public class PlaylistTests
    {
        [TestMethod]
        public async Task TestPlaylistByID()
        {
            // Re:Zero Pack
            // Bytrius, Joshabi, and Alice
            // November 23rd, 2021
            var iLoveRem = await Client.Playlist(23);
            Assert.IsNotNull(iLoveRem);
            Assert.IsFalse(iLoveRem.Empty);
            Assert.IsNotNull(iLoveRem.Playlist.Owner);
            // Please do not uncurate this Helen
            Assert.IsNotNull(iLoveRem.Playlist.Curator);
            
            // Testing going forward
            var whoIsRem = await iLoveRem.Next();
            Assert.IsNotNull(whoIsRem);
            Assert.IsTrue(iLoveRem.Playlist.Equals(whoIsRem.Playlist));
            Assert.IsTrue(whoIsRem.Empty);
            
            // And back
            var iRember = await whoIsRem.Previous();
            Assert.IsNotNull(iRember);
            Assert.IsTrue(iLoveRem.Playlist.Equals(iRember.Playlist));
            Assert.IsFalse(iRember.Empty);
        }
        
        [TestMethod]
        public async Task TestPaging()
        {
            var latestPage = await Client.SearchPlaylists(SearchTextPlaylistFilterOptions.Latest);
            Assert.IsNotNull(latestPage);

            // There better be playlists on the latest curator page.
            Assert.IsFalse(latestPage.Empty);

            var nextPage = await latestPage.Next();
            Assert.IsNotNull(nextPage);

            // And there BETTER be playlists on the next page.
            Assert.IsFalse(latestPage.Empty);

            // Now we check to see if they're in the right order
            Assert.IsTrue(latestPage.Playlists[latestPage.Playlists.Count - 1].CreatedAt >= nextPage.Playlists[0].CreatedAt);

            // And now we test is the previous page is the same as the first.
            var previousPage = await nextPage.Previous();
            Assert.IsNotNull(previousPage);
            CollectionAssert.AreEqual(latestPage.Playlists.ToList(), previousPage.Playlists.ToList());
        }
        
        [TestMethod]
        public async Task TestLatest()
        {
            var latestPage = await Client.LatestPlaylists();
            Assert.IsNotNull(latestPage);

            // There better be playlists on the latest page.
            Assert.IsFalse(latestPage.Empty);

            var nextPage = await latestPage.Next();
            Assert.IsNotNull(nextPage);

            // And there BETTER be playlists on the next page.
            Assert.IsFalse(latestPage.Empty);

            // Now we check to see if they're in the right order
            Assert.IsTrue(latestPage.Playlists[latestPage.Playlists.Count - 1].CreatedAt > nextPage.Playlists[0].CreatedAt);

            // And now we test is the previous page is the same as the first.
            var previousPage = await nextPage.Previous();
            Assert.IsNotNull(previousPage);
            CollectionAssert.AreEqual(latestPage.Playlists.ToList(), previousPage.Playlists.ToList());
        }
    }
}