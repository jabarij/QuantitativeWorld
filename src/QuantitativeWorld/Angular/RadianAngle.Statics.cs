using System;

namespace QuantitativeWorld.Angular
{
    partial struct RadianAngle
    {
        public static readonly RadianAngle PI = new RadianAngle(Math.PI);
        public static readonly RadianAngle Right = PI / 2d;
        public static readonly RadianAngle Straight = PI;
        public static readonly RadianAngle Full = PI * 2d;
    }
}
