using System;

namespace QuantitativeWorld.Angular
{
    partial struct DegreeAngle
    {
        public static readonly DegreeAngle Right = new DegreeAngle(90d);
        public static readonly DegreeAngle Straight = new DegreeAngle(180d);
        public static readonly DegreeAngle Full = new DegreeAngle(360d);
    }
}
