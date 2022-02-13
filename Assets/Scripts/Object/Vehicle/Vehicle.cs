using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Traffic.Node;

public abstract class Vehicle : MovingObject
{
    public int ID { get; private set; }
    public int Wage { get; protected set; }
    public int Capacity { get; protected set; }
    void Start()
    {
        this.ID = Random.Range(0, 100000);
    }
}
