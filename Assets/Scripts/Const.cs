using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Const
{
    public class Z {
        public static readonly float Building = 0f;
        public static readonly float Person = 8f;
        public static readonly float Camera = 10f;
    }

    // Edgeの種類 (要追加)
    // デフォルト引数で使いたかったので const
    public class Edge {
        public class Type {
            public const int Walk = 0;
            public const int Train = 1;
        }
        public static float Cost(int type) {
            switch (type) {
                case Type.Walk:
                    return 10;
                case Type.Train:
                    return 2;
                default:
                    throw new System.Exception();
            }
        }
    }

    // 多分そのうちいらなくなる
    public class Move {
        public static readonly string Walking = "Walking";
        public static readonly string Train = "Train";
        public static readonly string Bus = "Bus";
    }
}