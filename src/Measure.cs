namespace Boinst.Measure
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml.Serialization;

    /// <summary>
    /// A measure.
    /// </summary>
    /// <remarks>
    /// Base class for all unit types in Boinst.Measure.
    /// </remarks>
    public abstract class Measure : IMeasure
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
        /// The value of this Measure in Standard Units
        /// </summary>
        /// <returns>
        /// The equivalent of this Measure in Standard Units
        /// </returns>
        public abstract double ToStandardUnits();

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

    public interface IMeasure
    {
        double ToStandardUnits();
    }

    public class MeasureClass
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Dimension")]
        public string Dimension { get; set; }

        [XmlAttribute("Factor")]
        public double Factor { get; set; }
    }

    [Serializable]
    [XmlRoot("Measures")]
    public class Measures : List<MeasureClass>
    {
    }

    public class DimensionAttribute : Attribute
    {
        public DimensionAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
