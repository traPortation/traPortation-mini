﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TraPortation
{
    public class Bus : Vehicle
    {
        void Start()
        {
            this.Capacity = Const.Bus.Capacity;
            this.Wage = Const.Bus.Wage;
            this.velocity = Const.Bus.BusVelocity;
        }

        void FixedUpdate()
        {
            this.Move(this.velocity);
        }
    }
}