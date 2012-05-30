namespace Boinst.Measure.Length
{
    /// <summary>
    /// Convert from any other Length type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILength<out T> : ILength where T : ILength
    {
        T From(ILength length);
    }

    public interface ILength
    {
        double ToMetres();
    }
}