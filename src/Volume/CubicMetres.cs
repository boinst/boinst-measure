using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boinst.Measure.VolumetricFlow
{
    using Boinst.Measure.Volume;

    /// <summary>
    /// </summary>
    public class CubicMetres : Volume, IVolume<CubicMetres>
    {
        public CubicMetres(double value)
            : base(value)
        {
        }

        public CubicMetres From(IVolume flow)
        {
            return new CubicMetres(flow.ToCubicMetres());
        }

        public override double ToCubicMetres()
        {
            return Value;
        }
    }
}
