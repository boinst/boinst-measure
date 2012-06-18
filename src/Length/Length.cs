namespace Boinst.Measure.Length
{
    using System;

    using Boinst.Measure.Area;
    using Boinst.Measure.Volume;

    /// <summary>
    /// Units of Length
    /// </summary>
    [Dimension("Length")]
    public abstract class Length : Measure, ILength
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Length"/> class.
        /// </summary>
        /// <param name="value">
        /// The value of the instance.
        /// </param>
        protected Length(double value)
            : base(value)
        {
        }

        
        public Length From<T>(double val) where T : Measure, ILength
        {
            return (Length)Activator.CreateInstance(typeof(T), new object[] { val });
        }

        /// <summary>
        /// Convert this Length instance to another Length type
        /// </summary>
        public T To<T>() where T : ILength, ILength<T>
        {
            T instance = (T)Activator.CreateInstance(typeof(T), new object[] { 0 });
            return instance.From(this);
        }

        public static Length operator +(Length l1, Length l2)
        {
            return new Metres(((Measure)l1).ToStandardUnits() + ((Measure)l2).ToStandardUnits());
        }

        public static Length operator -(Length l1, Length l2)
        {
            return new Metres(((Measure)l1).ToStandardUnits() - ((Measure)l2).ToStandardUnits());
        }

        /// <summary>
        /// Length * Length = Area
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static Area operator *(Length l1, Length l2)
        {
            return (Area)Activator.CreateInstance(typeof(SquareMetres), new object[] { ((Measure)l1).ToStandardUnits() * ((Measure)l2).ToStandardUnits() });
        }

        /// <summary>
        /// Length * Area = Volume
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static Volume operator *(Length l1, Area l2)
        {
            return (Volume)Activator.CreateInstance(typeof(CubicMetres), new object[] { ((Measure)l1).ToStandardUnits() * l2.ToStandardUnits() });
        }

        /// <summary>
        /// Length / Length = value
        /// </summary>
        /// <param name="l1">The numerator</param>
        /// <param name="l2">The denominator</param>
        /// <returns></returns>
        public static double operator /(Length l1, Length l2)
        {
            return ((Measure)l1).ToStandardUnits() / ((Measure)l2).ToStandardUnits();
        }

        /// <summary>
        /// The value of this Measure in Metres
        /// </summary>
        /// <returns>
        /// The equivalent of this Measure in Metres
        /// </returns>
        public override abstract double ToStandardUnits();
    }
}