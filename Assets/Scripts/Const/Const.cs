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
                EdgeType.Bus => 5,
                _ => throw new System.NotImplementedException(),
            };
        }
    }

    /// <summary>
    /// 速度に関する定数
    /// </summary>
    public static class Velocity
    {
        public const float Person = 0.01f;
        public const float Train = 0.3f;
        public const float Bus = 0.1f;
    }


    public static class Person
    {
        public const int StopMilliseconds = 1000;
    }
}