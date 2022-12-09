using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TraPortation
{
    public class Bus : MovingObject
    {
        public int ID { get; private set; }
        public int Wage { get; private set; }
        public int Capacity { get; private set; }
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

        public void SetId(int id)
        {
            this.ID = id;
        }
    }
}