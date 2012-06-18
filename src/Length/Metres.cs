namespace Boinst.Measure.Length
{
    /// <summary>
    /// Metres - a unit of length
    /// </summary>
    public sealed class Metres : Length, ILength<Metres>
    {
        private const double MetresPerMeter = 1.0;

        public Metres(double value)
            : base(value)
        {
        }

        public override double ToStandardUnits()
        {
            return this.Value / MetresPerMeter;
        }

        public Metres From(Length length)
        {
            return new Metres(length.ToStandardUnits() * MetresPerMeter);
        }

        public override string ToString()
        {
            return base.ToString() + " metres";
        }
    }
}