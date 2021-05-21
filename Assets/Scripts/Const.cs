using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Const
{
    public static class X {
        public static readonly float Min = 0f;
        public static readonly float Max = 12f;
    }
    public static class Y {
        public static readonly float Min = 0f;
        public static readonly float Max = 8f;
    }
    public static class Z {
        public static readonly float Building = 0f;
        public static readonly float Person = 8f;
        public static readonly float Camera = 10f;
    }

    // Edgeの種類 (要追加)
    // デフォルト引数で使いたかったので const
    public static class EdgeType {
        public const int Walk = 0;
        public const int Train = 1;
        public const int Multi = 2;
    }
    public static class EdgeCost {
        public const int Walk = 10;
        public const int Train = 3;
        public static int Get(int type) {
            switch (type) {
                case EdgeType.Walk: return Walk;
                case EdgeType.Train: return Train;
                default: throw new System.Exception("edge type is wrong");
            }
        }
    }
    public static class Count {
        public static readonly int Person = 10;
    }
}