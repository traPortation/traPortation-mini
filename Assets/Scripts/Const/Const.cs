using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraPortation.Traffic;

namespace TraPortation.Const
{
    /// <summary>
    /// Edgeのコスト
    /// </summary>
    public static class EdgeCost
    {
        public static int Get(EdgeType type)
        {
            return type switch
            {
                EdgeType.Walk => 10,
                EdgeType.Train => 1,
                _ => 0,
            };
        }
    }

    /// <summary>
    /// 数に関する定数
    /// </summary>
    public static class Count
    {
        public const int Person = 10;
        public const int Road = 30;
    }
    /// <summary>
    /// 速度に関する定数
    /// </summary>
    public static class Velocity
    {
        public const float Person = 0.05f;
        public const float Train = 0.3f;
    }

    /// <summary>
    /// 所持金に関する定数
    /// </summary>
    public static class Money
    {
        public const int Start = 100000;

        public const int minMoney = 0;
        public const int maxMoney = 999999999;
    }
}