namespace Boinst.Measure.Time
{
    /// <summary>
    /// </summary>
    public class Seconds : Time, ITime<Seconds>
    {
        public Seconds(double value)
            : base(value)
        {
        }

        Seconds ITime<Seconds>.From(ITime time)
        {
            return From(time);
        }

        public static Seconds From(ITime time)
        {
            return new Seconds(time.ToSeconds());
        }

        public override double ToSeconds()
        {
            return this.Value;
        }
    }
}