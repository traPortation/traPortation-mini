using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Traffic.Node;
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