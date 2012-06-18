namespace Boinst.Measure.Volume
{
    /// <summary>
    /// Volume in Cubic Metres.
    /// </summary>
    public class CubicMetres : Volume, IVolume<CubicMetres>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CubicMetres"/> class.
        /// </summary>
        /// <param name="value">
        /// The value to initialise the instance with.
        /// </param>
        public CubicMetres(double value)
            : base(value)
        {
        }

        public CubicMetres From(IVolume flow)
        {
            return new CubicMetres(flow.ToStandardUnits());
        }

        /// <summary>
        /// Convert to a volume in cubic metres.
        /// </summary>
        /// <returns>
        /// The volume in Cubic Metres.
        /// </returns>
        public override double ToStandardUnits()
        {
            return this.Value;
        }
    }
}
