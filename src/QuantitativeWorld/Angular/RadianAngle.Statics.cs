using System;

namespace QuantitativeWorld.Angular
{
    partial struct RadianAngle
    {
        public static readonly RadianAngle Right = new RadianAngle(Math.PI / 2d);
        public static readonly RadianAngle Straight = new RadianAngle(Math.PI);
        public static readonly RadianAngle Full = new RadianAngle(Math.PI * 2d);
    }
}
