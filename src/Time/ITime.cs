namespace Boinst.Measure.Time
{
    /// <summary>
    /// Convert from any other Time type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITime<out T> : ITime where T : ITime
    {
        T From(ITime flow);
    }

    public interface ITime
    {
        double ToSeconds();
    }
}
