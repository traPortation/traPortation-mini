using UnityEngine;

namespace TraPortation.Const
{
    /// <summary>
    /// X座標に関する定数
    /// </summary>
    public static class X
    {
        public static readonly float Min = 0f;
        public static readonly float Max = 16f;
    }
    /// <summary>
    /// Y座標に関する定数
    /// </summary>
    public static class Y
    {
        public static readonly float Min = 0f;
        public static readonly float Max = 9f;
    }

    public static class Map
    {
        public static readonly float Margin = 0f;

        public static readonly float XMin = X.Min - Margin;
        public static readonly float XMax = X.Max + Margin;
        public static readonly float YMin = Y.Min - Margin;
        public static readonly float YMax = Y.Max + Margin;

        public static readonly float Width = XMax - XMin;
        public static readonly float Height = YMax - YMin;

        public static readonly Vector2 Center = new Vector2((XMax + XMin) / 2, (YMax + YMin) / 2);
    }

    /// <summary>
    /// Z座標に関する定数
    /// </summary>
    public static class Z
    {
        public const float Camera = -100f;

        public const float MouseIcon = -10f;
        public const float Gradation = 0.5f;

        public const float Person = 5f;

        // 乗り物
        public const float Train = 10f;
        public const float Bus = 11f;

        // 路線
        public const float Rail = 20f;
        public const float BusRail = 21f;


        // 駅
        public const float Station = 30f;
        public const float BusStation = 31f;


        public const float Road = 50f;

        public const float Background = 100f;
    }
}