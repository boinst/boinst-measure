namespace Boinst.Measure.Tests
{
    using Boinst.Measure.Length;

    using Xunit;

    /// <summary>
    /// Test length conversions
    /// </summary>
    public class LengthTests
    {
        [Fact]
        public void MetresToFeet()
        {
            Metres m = new Metres(1);
            Feet ft = (new Feet(0)).From(m);
            
            // 1 metre = 3.2808399 feet (3 feet 3⅜ inches)
            Assert.Equal(3.2808399, ft.Value, 4);
        }

        [Fact]
        public void ConvertFromMetresToFeet()
        {
            double feet = Convert.From<Metres>(1).To<Feet>();
            Assert.Equal(3.2808399, feet, 4);
        }

        [Fact]
        public void ConvertToFeetFromMetres()
        {
            double feet = Convert.To<Feet>().From<Metres>(1);
            Assert.Equal(3.2808399, feet, 4);
        }
    }
}
