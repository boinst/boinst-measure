namespace Boinst.Measure.Tests
{
    using Boinst.Measure.Volume;

    using NUnit.Framework;

    [TestFixture]
    public class VolumeTests
    {
        [Test]
        public void ConvertFromCubicMetresToLitres()
        {
            // 12 (cubic metres) = 12,000 litres
            Assert.AreEqual(12000, Convert.From<CubicMetres>(12).To<Litres>(), 1e-6);
        }

        [Test]
        public void ConvertFromCubicMetresToMegaLitres()
        {
            // 12 (cubic metres) = 0.012 mega litres
            Assert.AreEqual(0.012, Convert.From<CubicMetres>(12).To<MegaLitres>(), 1e-6);
        }
    }
}