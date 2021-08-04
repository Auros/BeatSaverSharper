using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BeatSaverSharp.Tests.TestHelper;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace BeatSaverSharp.Tests
{
    [TestClass]
    public class DownloadTests
    {
        [TestMethod]
        public async Task TestDownloadingMapFiles()
        {
            // [Noodle Remaster - OST 1] Jaroslav Beck - Legend (ft. Backchat)
            //  Ramen Noodle
            //  May 1st, 2020
            var legend = await Client.Beatmap("a125");
            Assert.IsNotNull(legend);

            var assembly = Assembly.GetExecutingAssembly();

            using var legendCoverStream = assembly.GetManifestResourceStream("BeatSaverSharp.Tests.Resources.Legend.jpg");
            using var legendZIPStream = assembly.GetManifestResourceStream("BeatSaverSharp.Tests.Resources.Legend.zip");

            using var legendCoverMemoryStream = new MemoryStream();
            using var legendZIPMemoryStream = new MemoryStream();
            await legendCoverStream.CopyToAsync(legendCoverMemoryStream);
            await legendZIPStream.CopyToAsync(legendZIPMemoryStream);

            var coverImageBytes = await legend.LatestVersion.DownloadCoverImage();
            Assert.IsNotNull(coverImageBytes);
            var zipBytes = await legend.LatestVersion.DownloadZIP();
            Assert.IsNotNull(zipBytes);

            CollectionAssert.AreEqual(coverImageBytes, legendCoverMemoryStream.ToArray());
            CollectionAssert.AreEqual(zipBytes, legendZIPMemoryStream.ToArray());
        }
    }
}