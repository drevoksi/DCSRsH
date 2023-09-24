using System;
namespace DCSRsH
{
    public static class CalculuF
    {
        public static float Clamp(float f, float lower, float upper)
        {
            if (f < lower) return lower;
            if (f > upper) return upper;
            return f;
        }
        public static float UnitClamp(float f)
            => Clamp(f, 0, 1);
        public static int RoundToInt(float f)
            => Convert.ToInt32(MathF.Round(f));
    }
}

