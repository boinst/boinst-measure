namespace Boinst.Measure.Tests
{
    using Boinst.MeasureKai;
    using Boinst.MeasureKai.VolumetricFlow;

    using NUnit.Framework;

    [TestFixture]
    public class VolumetricFlowTests
    {
        [Test]
        public void ConvertFromCubicMetresPerSecondToMegaLitresPerDay()
        {
            // 7 ((cubic metres) per second) = 604.8 megalitres per day
            Assert.AreEqual(604.8, Convert.From<CubicMetresPerSecond>(7).To<MegaLitresPerDay>(), 1e-6);
        }
    }
}