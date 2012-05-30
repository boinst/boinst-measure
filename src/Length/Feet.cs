namespace Boinst.Measure.Length
{
    /// <summary>
    /// Units of Feet
    /// </summary>
    public class Feet : Length, ILength<Feet>
    {
        /// <summary>
        /// Conversion rate for Feet to Metres
        /// 1 metre = 3.2808399 feet (3 feet 3⅜ inches)
        /// </summary>
        private const double feetPerMetre = 3.2808399;

        /// <summary>
        /// Initializes a new instance of the <see cref="Feet"/> class. 
        /// </summary>
        /// <param name="feet">
        /// </param>
        public Feet(double feet)
            : base(feet)
        {
        }
        
        public override double ToMetres()
        {
            return this.Value / feetPerMetre;
        }

        public Feet From(ILength length)
        {
            return new Feet(length.ToMetres() * feetPerMetre);
        }

        public override string ToString()
        {
            return base.ToString() + " feet";
        }
    }
}