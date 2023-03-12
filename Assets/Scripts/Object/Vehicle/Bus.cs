using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TraPortation
{
    public class Bus : MovingObject
    {
        public int ID { get; private set; }
        public int Capacity { get; private set; }
        GameManager manager;

        void Start()
        {
            this.Capacity = Const.Bus.Capacity;
            this.velocity = Const.Velocity.Bus;
        }

        [Inject]
        public void Construct(GameManager manager)
        {
            this.manager = manager;
        }

        void FixedUpdate()
        {
            for (int i = 0; i < this.manager.GameSpeed; i++)
                this.Move(this.velocity);
        }

        public void SetId(int id)
        {
            this.ID = id;
        }
    }
}