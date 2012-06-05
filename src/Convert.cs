namespace Boinst.Measure
{
    using System;

    /// <summary>
    /// Provides syntactically pleasant conversion methods
    /// </summary>
    public static class Convert
    {
        public static T From<T>(double value) where T : Measure
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { value });
        }

        public static T To<T>() where T : Measure
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { 0 });
        }
    }
}