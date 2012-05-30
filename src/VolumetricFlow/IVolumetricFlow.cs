namespace Boinst.Measure.VolumetricFlow
{
    /// <summary>
    /// Convert from any other VolumetricFlow type
    /// </summary>
    /// <typeparam name="T">
    /// The unit type
    /// </typeparam>
    public interface IVolumetricFlow<out T> : IVolumetricFlow where T : IVolumetricFlow
    {
        /// <summary>
        /// Create a new instance of <see cref="T"/> from another <see cref="IVolumetricFlow"/>.
        /// </summary>
        /// <param name="flow">The flow to use as the initial value.</param>
        /// <returns>A new instance.</returns>
        T From(IVolumetricFlow flow);
    }

    /// <summary>
    /// A measure of Volumetric Flow
    /// </summary>
    public interface IVolumetricFlow
    {
        double ToCubicMetresPerSecond();
    }
}