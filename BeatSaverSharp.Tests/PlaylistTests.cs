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
    }
}