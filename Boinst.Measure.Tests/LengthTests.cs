namespace Boinst.Measure.Tests
{
    using Boinst.MeasureKai;
    using Boinst.MeasureKai.Area;
    using Boinst.MeasureKai.Length;
    using Boinst.MeasureKai.Volume;

    using NUnit.Framework;

    /// <summary>
    /// Test length conversions
    /// </summary>
    [TestFixture]
    public class LengthTests
    {
        [Test]
        public void MetresToFeet()
        {
            Metres m = new Metres(1);
            Feet ft = (new Feet(0)).From(m);

            // 1 metre = 3.2808399 feet (3 feet 3⅜ inches)
            Assert.AreEqual(3.2808399, ft.Value, 4);
        }

        [Test]
        public void ConvertFromMetresToFeet()
        {
            double feet = Convert.From<Metres>(1).To<Feet>();
            Assert.AreEqual(3.2808399, feet, 4);
        }

        [Test]
        public void ConvertToFeetFromMetres()
        {
            double feet = Convert.To<Feet>().From<Metres>(1);
            Assert.AreEqual(3.2808399, feet, 4);
        }

        [Test]
        public void LengthTimesLengthEqualsArea()
        {
            var m1 = new Metres(1);
            var m2 = new Metres(1);
            Area a = m1 * m2;
            Assert.AreEqual(1, a, 1e-6);
        }

        [Test]
        public void LengthDividedByLengthEqualsValue()
        {
            var m1 = new Metres(4);
            var m2 = new Metres(20);
            double v = m1 / m2;
            Assert.AreEqual(0.2, v, 1e-6);
        }

        [Test]
        public void LengthTimesAreaEqualsVolume()
        {
            Area area = new SquareMetres(1);
            Length length = new Metres(1);
            Volume volume = length * area;
            Assert.AreEqual(1, volume, 1e-6);
        }
    }
}
