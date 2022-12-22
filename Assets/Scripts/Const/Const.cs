using System.Collections;
using System.Collections.Generic;
using TraPortation.Traffic;
using UnityEngine;

namespace TraPortation.Const
{
    /// <summary>
    /// 辺の長さあたりのコスト (経路探索で使用する)
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

    public static class Velocity
    {
        public const float Person = 0.05f;
        public const float Train = 0.3f;
        public const float Bus = 0.03f;
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