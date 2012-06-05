namespace Boinst.Measure.Volume
{
    using System;

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

        /// <summary>
        /// Convert this Volume instance to another Volume type
        /// </summary>
        public T To<T>() where T : IVolume, IVolume<T>
        {
            T instance = (T)Activator.CreateInstance(typeof(T), new object[] { 0 });
            return instance.From(this);
        }
    }
}