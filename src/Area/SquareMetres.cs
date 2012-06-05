namespace Boinst.Measure.Area
{
    public class SquareMetres : Area, IArea<SquareMetres>
    {
        public SquareMetres(double value)
            : base(value)
        {
        }

        /// <summary>
        /// Convert from another area type to this area type.
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public SquareMetres From(IArea area)
        {
            return new SquareMetres(area.ToSquareMetres());
        }

        /// <summary>
        /// Convert to Square Metres.
        /// </summary>
        /// <returns></returns>
        public override double ToSquareMetres()
        {
            return this.Value;
        }
    }
}