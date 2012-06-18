namespace Boinst.Measure.VolumetricFlow
{
    using System;

    public abstract class VolumetricFlow : Measure, IVolumetricFlow
    {
        protected VolumetricFlow(double value)
            : base(value)
        {
        }

        /// <summary>
        /// Convert this VolumeFlow instance to an instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <returns>An instance of the desired type.</returns>
        public T To<T>() where T : IVolumetricFlow, IVolumetricFlow<T>
        {
            T instance = (T)Activator.CreateInstance(typeof(T), new object[] { 0 });
            return instance.From(this);
        }
    }
}