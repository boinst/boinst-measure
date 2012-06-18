namespace Boinst.Measure.VolumetricFlow
{
    /// <summary>
    /// Cubic Metres per second, a measure of Volumetric Flow
    /// </summary>
    public class CubicMetresPerSecond : VolumetricFlow, IVolumetricFlow<CubicMetresPerSecond>
    {
        public CubicMetresPerSecond(double value)
            : base(value)
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="CubicMetresPerSecond"/> from another <see cref="IVolumetricFlow"/>.
        /// </summary>
        /// <param name="flow">The flow to use as the initial value.</param>
        /// <returns>A new instance.</returns>
        CubicMetresPerSecond IVolumetricFlow<CubicMetresPerSecond>.From(IVolumetricFlow flow)
        {
            return From(flow);
        }

        public static CubicMetresPerSecond From(IVolumetricFlow flow)
        {
            return new CubicMetresPerSecond(flow.ToStandardUnits());
        }

        public override double ToStandardUnits()
        {
            return Value;
        }

        public override string ToString()
        {
            return base.ToString() + " m³ s⁻¹";
        }
    }
}