namespace Boinst.Measure.Volume
{
    /// <summary>
    /// Convert from any other Volume type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IVolume<out T> : IVolume where T : IVolume
    {
        /// <summary>
        /// Convert from another volume type to this volume type.
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        T From(IVolume flow);
    }

    /// <summary>
    /// A measure of Volume
    /// </summary>
    public interface IVolume
    {
        /// <summary>
        /// Convert to a volume in cubic metres.
        /// </summary>
        /// <returns></returns>
        double ToStandardUnits();

        /// <summary>
        /// Convert this Volume instance to an instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <returns>An instance of the desired type.</returns>
        T To<T>() where T : IVolume, IVolume<T>;
    }


}