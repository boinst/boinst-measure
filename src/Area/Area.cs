namespace Boinst.Measure.Area
{
    using System;

    public abstract class Area : Measure, IArea
    {
        public Area(double value)
            : base(value)
        {
        }

        /// <summary>
        /// Convert to Square Metres.
        /// </summary>
        /// <returns></returns>
        public abstract double ToSquareMetres();


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
