﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Traffic.Node;
public class Bus : Vehicle
{
    private bool isMoving = true;
    void Start()
    {
        this.Capacity = Const.Bus.Capacity;
        this.Wage = Const.Bus.Wage;
        this.velocity = Const.Bus.BusVelocity;
        this.Initialize(this.path);
    }

    void FixedUpdate()
    {
        if (this.isMoving)
        {
            this.Move(this.velocity);
        }
    }
}