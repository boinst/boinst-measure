namespace Boinst.Measure.Volume
{
    using Boinst.Measure.VolumetricFlow;

    public abstract class Volume : Measure, IVolume
    {
        protected Volume(double value)
            : base(value)
        {
            
        }

        public abstract double ToCubicMetres();

        public static VolumetricFlow operator /(Volume l1, Time.Time l2)
        {
            return new CubicMetresPerSecond(l1.ToCubicMetres() / l2.ToSeconds());
        }
    }
}