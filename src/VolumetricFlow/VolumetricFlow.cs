namespace Boinst.Measure.VolumetricFlow
{
    public abstract class VolumetricFlow : Measure, IVolumetricFlow
    {
        protected VolumetricFlow(double value)
            : base(value)
        {
        }

        public abstract double ToCubicMetresPerSecond();
    }
}