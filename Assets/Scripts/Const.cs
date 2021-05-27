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
    /// Edgeの種類
    /// </summary>
    public static class EdgeType
    {
        public const int Walk = 0;
        public const int Train = 1;
        public const int Multi = 2;
    }
    /// <summary>
    /// Edgeのコスト
    /// </summary>
    public static class EdgeCost
    {
        public const int Walk = 10;
        public const int Train = 3;
        /// <summary>
        /// Const.EdgeType を引数としてコストを返す
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int Get(int type)
        {
            switch (type)
            {
                case Const.EdgeType.Walk: return Walk;
                case Const.EdgeType.Train: return Train;
                default: throw new System.Exception("edge type is wrong");
            }
        }
    }
    /// <summary>
    /// 数に関する定数
    /// </summary>
    public static class Count
    {
        public static readonly int Person = 10;
    }
}