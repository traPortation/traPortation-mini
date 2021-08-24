using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Const
{
    /// <summary>
    /// X座標に関する定数
    /// </summary>
    public static class X
    {
        public static readonly float Min = 0f;
        public static readonly float Max = 12f;
    }
    /// <summary>
    /// Y座標に関する定数
    /// </summary>
    public static class Y
    {
        public static readonly float Min = 0f;
        public static readonly float Max = 8f;
    }
    /// <summary>
    /// Z座標に関する定数
    /// </summary>
    public static class Z
    {
        public static readonly float Building = 0f;
        public static readonly float Person = 8f;
        public static readonly float Camera = 10f;
    }

    /// <summary>
    /// Edgeのコスト
    /// </summary>
    public static class EdgeCost
    {
        public enum Type
        {
            Walk = 10,
            Train = 3,
            Finished = -1 // 使い終わったEdge
            // Multi = ?,
        }
        public static int Get(Type type)
        {
            return (int) type;
        }
    }

    public static class Trains{
        public const int Capacity = 4;
        public const int Wage = 100;
        public const float TrainVelocity = 0.03f;
        public const float StopStationTime = 1f;
    }

    /// <summary>
    /// 数に関する定数
    /// </summary>
    public static class Count
    {
        public static readonly int Person = 10;
    }

    /// <summary>
    /// 所持金に関する定数
    /// </summary>
    public static class MoneyLimit
    {
        private static const int minMoney = 0;
        private static const int maxMoney = 999999999;
    }
}