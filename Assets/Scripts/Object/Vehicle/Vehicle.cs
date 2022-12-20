using System.Collections;
using System.Collections.Generic;
using TraPortation.Traffic.Node;
using UnityEngine;

public abstract class Vehicle : MovingObject
{
    
    public int ID { get; private set; }
    public int Wage { get; protected set; }
    public int Capacity { get; protected set; }
    public int PassengerCount { get; protected set; }
    void Start()
    {
        this.ID = Random.Range(0, 100000);
    }
}
