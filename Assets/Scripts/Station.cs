using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Traffic.Node;
using MessagePipe;
using Zenject;

public class Station : MonoBehaviour
{
    public int ID { get; private set; }
    public StationNode Node { get; private set; }

    /// <summary>
    /// StationNodeを割り当てる
    /// Instantiate時に一度だけ呼ぶ
    /// </summary>
    /// <param name="node"></param>
    public void SetNode(StationNode node)
    {
        if (this.Node == null)
        {
            this.Node = node;
            this.ID = this.Node.Index;
        }
        else
        {
            // 例外投げるのはあんまよくないかも
            throw new System.Exception("Stationのノードへの再代入");
        }
    }
}
