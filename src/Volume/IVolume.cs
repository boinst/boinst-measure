namespace Boinst.Measure.Volume
{
    /// <summary>
    /// Convert from any other Volume type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IVolume<out T> : IVolume where T : IVolume
    {
        T From(IVolume flow);
    }

    public interface IVolume
    {
        double ToCubicMetres();
    }
}