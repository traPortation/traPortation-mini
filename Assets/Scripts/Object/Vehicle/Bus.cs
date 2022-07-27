using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TraPortation.Traffic.Node;
using UnityEngine;

public class Bus : Vehicle
{
    void Start()
    {
        this.Capacity = TraPortation.Const.Bus.Capacity;
        this.Wage = TraPortation.Const.Bus.Wage;
        this.velocity = TraPortation.Const.Bus.BusVelocity;
        this.Initialize(this.path);
    }

    void FixedUpdate()
    {
        this.Move(this.velocity);
    }
}