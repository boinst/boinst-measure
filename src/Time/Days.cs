namespace Boinst.Measure.Time
{
    /// <summary>
    /// </summary>
    public class Days : Time, ITime<Days>
    {
        public Days(double value)
            : base(value)
        {
        }

        Days ITime<Days>.From(ITime time)
        {
            return From(time);
        }

        public static Days From(ITime time)
        {
            return new Days(time.ToSeconds() / SecondsPerDay);
        }

        public override double ToSeconds()
        {
            return this.Value * SecondsPerDay;
        }
    }
}