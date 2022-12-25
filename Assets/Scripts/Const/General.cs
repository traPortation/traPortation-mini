using UnityEngine;

namespace TraPortation.Const
{
    public static class General
    {
        public const int PersonCount = 10;
        public const int RoadCount = 100;

        public const float TimeLimitSeconds = 300f;

        public static float RoadWidth(float length)
        {
            return Mathf.Sqrt(length) * 0.1f;
        }
    }
}