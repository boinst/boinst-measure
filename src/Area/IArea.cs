namespace Boinst.Measure.Area
{
    /// <summary>
    /// A measure of Area
    /// </summary>
    public interface IArea
    {
        /// <summary>
        /// Convert to Square Metres.
        /// </summary>
        /// <returns></returns>
        double ToSquareMetres();
    }

    /// <summary>
    /// Convert from any other Volume type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IArea<out T> : IArea where T : IArea
    {
        /// <summary>
        /// Convert from another area type to this area type.
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        T From(IArea area);
    }
}