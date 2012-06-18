namespace Boinst.Measure.Area
{
    /// <summary>
    /// </summary>
    public class Hectares : Area, IArea<Hectares>
    {
        // 1 (square metre) = 0.0001 hectares
        private const double HectaresPerSquareMetre = 0.0001;

        public Hectares(double value)
            : base(value)
        {
        }

        /// <summary>
        /// Convert from another area type to this area type.
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public Hectares From(IArea area)
        {
            return new Hectares(area.ToStandardUnits() * HectaresPerSquareMetre);
        }

        /// <summary>
        /// Convert to Square Metres.
        /// </summary>
        /// <returns></returns>
        public override double ToStandardUnits()
        {
            return Value / HectaresPerSquareMetre;
        }
    }
}
