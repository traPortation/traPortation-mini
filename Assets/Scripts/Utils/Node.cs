using System.Collections;
using System.Collections.Generic;
using TraPortation.Traffic.Node;
using UnityEngine;
// NOTE: UtilsがTrafficに依存してるのあんまよくないかも

namespace Utils
{
    static class Node
    {
        public static float Distance(INode a, INode b)
        {
            return Mathf.Sqrt(Mathf.Pow(a.X - b.X, 2) + Mathf.Pow(a.Y - b.Y, 2));
        }
    }
}