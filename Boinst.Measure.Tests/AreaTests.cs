namespace Boinst.Measure.Tests
{
    using Boinst.MeasureKai;
    using Boinst.MeasureKai.Area;
    using Boinst.MeasureKai.Length;
    using Boinst.MeasureKai.Volume;

    using NUnit.Framework;

    [TestFixture]
    public class AreaTests
    {
        [Test]
        public void ConvertFromSquareMetresToHectares()
        {
            // 1 (square metre) = 0.0001 hectares
            Assert.AreEqual(0.0001, Convert.From<SquareMetres>(1).To<Hectares>(), 1e-6);
        }

        [Test]
        public void ConvertFromHectaresToSquareMetres()
        {
            // 1 (square metre) = 0.0001 hectares
            Assert.AreEqual(10000, Convert.From<Hectares>(1).To<SquareMetres>(), 1e-6);
        }

        [Test]
        public void AreaTimesLengthEqualsVolume()
        {
            Area area = new SquareMetres(1);
            Length length = new Metres(1);
            Volume volume = area * length;
            Assert.AreEqual(1, volume, 1e-6);
        }
    }
}