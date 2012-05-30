namespace Boinst.Measure.Volume
{
    /// <summary>
    /// MegaLitres, a measure of Volume
    /// </summary>
    public class MegaLitres : Volume, IVolume<MegaLitres>
    {
        public MegaLitres(double value)
            : base(value)
        {
        }

        public MegaLitres From(IVolume flow)
        {
            return new MegaLitres(Litres.From(flow) / Magnitude.Mega);
        }

        public override double ToCubicMetres()
        {
            return new Litres(Magnitude.Mega * this).ToCubicMetres();
        }

        public override string ToString()
        {
            return base.ToString() + " ML";
        }
    }
}