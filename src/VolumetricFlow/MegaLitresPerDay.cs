namespace Boinst.Measure.VolumetricFlow
{
    /// <summary>
    /// Mega Litres per day
    /// </summary>
    public class MegaLitresPerDay : VolumetricFlow, IVolumetricFlow<MegaLitresPerDay>
    {
        // 1 ((cubic metre) per second) = 86.4 megalitres per day
        private const double megalitresPerDayPerCubicMetrePerSecond = 86.4;

        public MegaLitresPerDay(double value)
            : base(value)
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="T"/> from another <see cref="IVolumetricFlow"/>.
        /// </summary>
        /// <param name="flow">The flow to use as the initial value.</param>
        /// <returns>A new instance.</returns>
        MegaLitresPerDay IVolumetricFlow<MegaLitresPerDay>.From(IVolumetricFlow flow)
        {
            return From(flow);
        }

        public static MegaLitresPerDay From(IVolumetricFlow flow)
        {
            return new MegaLitresPerDay(flow.ToStandardUnits() * megalitresPerDayPerCubicMetrePerSecond);
        }

        public override double ToStandardUnits()
        {
            return Value / megalitresPerDayPerCubicMetrePerSecond;
        }

        public override string ToString()
        {
            return base.ToString() + " ML per day";
        }
    }
}