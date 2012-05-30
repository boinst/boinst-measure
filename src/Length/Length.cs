namespace Boinst.Measure.Length
{
    using System;

    /// <summary>
    /// Units of Length
    /// </summary>
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

        /// <summary>
        /// The value of this Length in Metres
        /// </summary>
        /// <returns>
        /// The equivalent of this length in metres.
        /// </returns>
        public abstract double ToMetres();
        
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
            return new Metres(l1.ToMetres() + l2.ToMetres());
        }

        public static Length operator -(Length l1, Length l2)
        {
            return new Metres(l1.ToMetres() - l2.ToMetres());
        }
    }
}