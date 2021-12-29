using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

#nullable enable

// NOTE: 構造については要検討
public class PathNode
{
    public INode Node { get; }
    public IEdge? Edge { get; }
    public PathNode(INode node, IEdge? edge)
    {
        this.Node = node;
        this.Edge = edge;
    }
}