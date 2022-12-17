using System.Collections;
using System.Collections.Generic;
using TraPortation.Const;
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

            this.transform.position = new Vector3(0, 0, Z.Bus);
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