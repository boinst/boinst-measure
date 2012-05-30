namespace Boinst.Measure.Length
{
    /// <summary>
    /// Metres - a unit of length
    /// </summary>
    public sealed class Metres : Length, ILength<Metres>
    {
        public Metres(double value)
            : base(value)
        {
        }

        public override double ToMetres()
        {
            return this.Value;
        }

        public Metres From(ILength length)
        {
            return new Metres(length.ToMetres());
        }

        public override string ToString()
        {
            return base.ToString() + " metres";
        }
    }
}