using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

#nullable enable

// NOTE: そのうちIndexedじゃなくてよくなる
public class PathNode
{
    // できればprivateにしたい
    public IIndexedNode Node { get; }
    public float X => this.Node.X;
    public float Y => this.Node.Y;
    public int Index => this.Node.Index;
    public IEdge? Edge { get; }
    public PathNode(IIndexedNode node, IEdge? edge)
    {
        this.Node = node;
        this.Edge = edge;
    }
}