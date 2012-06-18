namespace Boinst.Measure.Volume
{
    public class Litres : Volume, IVolume<Litres>
    {
        /// <summary>
        /// There are 1000 litres in a cubic meter
        /// </summary>
        private const double litresPerCubicMetre = 1e3;

        /// <summary>
        /// Initializes a new instance of the <see cref="Litres"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public Litres(double value)
            : base(value)
        {
        }

        Litres IVolume<Litres>.From(IVolume flow)
        {
            return From(flow);
        }

        public static Litres From(IVolume flow)
        {
            return new Litres(flow.ToStandardUnits() * litresPerCubicMetre);
        }

        public override double ToStandardUnits()
        {
            return base.Value / litresPerCubicMetre;
        }

        public override string ToString()
        {
            return base.ToString() + " L";
        }
    }
}