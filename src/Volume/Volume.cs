namespace Boinst.Measure.Volume
{
    using System;

    using Boinst.Measure.Area;
    using Boinst.Measure.Length;
    using Boinst.Measure.VolumetricFlow;

    public abstract class Volume : Measure, IVolume
    {
        protected Volume(double value)
            : base(value)
        {
        }

        public abstract double ToCubicMetres();

        /// <summary>
        /// Volume / Time = VolumetricFlow
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static VolumetricFlow operator /(Volume l1, Time.Time l2)
        {
            return new CubicMetresPerSecond(l1.ToCubicMetres() / l2.ToSeconds());
        }

        /// <summary>
        /// Volume / Length = Area
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static Area operator /(Volume l1, Length l2)
        {
            return new SquareMetres(l1.ToCubicMetres() / l2.ToMetres());
        }

        /// <summary>
        /// Volume / Area = Length
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static Length operator /(Volume l1, Area l2)
        {
            return new Metres(l1.ToCubicMetres() / l2.ToSquareMetres());
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