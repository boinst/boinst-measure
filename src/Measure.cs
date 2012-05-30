namespace Boinst.Measure
{
    using System;
    using System.Globalization;

    /// <summary>
    /// A measure.
    /// </summary>
    /// <remarks>
    /// Base class for all unit types in Boinst.Measure.
    /// </remarks>
    public abstract class Measure
    {
        /// <summary>
        /// The value (immutable)
        /// </summary>
        private readonly double value;

        protected Measure(double value)
        {
            this.value = value;
        }

        public double Value
        {
            get { return value; }
        }

        /// <summary>
        /// A measure may be implicitly converted to a double
        /// </summary>
        /// <param name="measure">
        /// The measure to convert from
        /// </param>
        /// <returns>
        /// </returns>
        public static implicit operator double(Measure measure)
        {
            return measure.Value;
        }

        public static Measure operator *(Measure l1, double l2)
        {
            return (Measure)Activator.CreateInstance(l1.GetType(), new object[] { l1.Value * l2 });
        }

        public static Measure operator /(Measure l1, double l2)
        {
            return (Measure)Activator.CreateInstance(l1.GetType(), new object[] { l1.Value / l2 });
        }

        public override string ToString()
        {
            return Value.ToString("0.0##E0", CultureInfo.CurrentCulture);
        }
    }
}
