using UnityEngine;

namespace TraPortation.Const
{
    public static class General
    {
        public const int PersonCount = 100;
        public const int RoadCount = 100;

        public const float TimeLimitSeconds = 600f;

        public static float RoadWidth(float length)
        {
            return Mathf.Sqrt(length) * 0.1f;
        }

        public const float LineWidth = 0.1f;
        public const float GradationWidth = 0.5f;
    }
}