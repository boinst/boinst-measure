namespace Boinst.Measure.Area
{
    using System;

    using Boinst.Measure.Volume;

    public abstract class Area : Measure, IArea
    {
        public Area(double value)
            : base(value)
        {
        }

        /// <summary>
        /// Area * Length = Volume
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static Volume operator *(Area l1, Length.Length l2)
        {
            return (Volume)Activator.CreateInstance(typeof(CubicMetres), new object[] { l1.ToStandardUnits() * ((Measure)l2).ToStandardUnits() });
        }

        /// <summary>
        /// Convert this Volume instance to another Volume type
        /// </summary>
        public T To<T>() where T : IArea, IArea<T>
        {
            T instance = (T)Activator.CreateInstance(typeof(T), new object[] { 0 });
            return instance.From(this);
        }
    }

}
