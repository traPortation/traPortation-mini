using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Const
{

    public enum EdgeType
    {
        Walk = 10,
        Train = 3,
        Finished = -1 // 使い終わったEdge
        // Multi = ?,
    }

    /// <summary>
    /// Edgeのコスト
    /// </summary>
    public static class EdgeCost
    {
        public static int Get(EdgeType type)
        {
            return (int)type;
        }
    }



    /// <summary>
    /// 数に関する定数
    /// </summary>
    public static class Count
    {
        public static readonly int Person = 10;
    }
    /// <summary>
    /// 速度に関する定数
    /// </summary>
    public static class Velocity
    {
        public const float Person = 0.01f;
        public const float Train = 0.03f;
    }

    /// <summary>
    /// 所持金に関する定数
    /// </summary>
    public static class MoneyLimit
    {
        public const int minMoney = 0;
        public const int maxMoney = 999999999;
    }
}