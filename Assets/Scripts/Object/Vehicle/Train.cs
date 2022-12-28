using System.Collections;
using UnityEngine;

#nullable enable

namespace TraPortation
{
    public class Train : MovingObject
    {
        public int ID { get; private set; }
        public int Capacity { get; private set; }
        void Start()
        {
            this.Capacity = Const.Train.Capacity;
            this.velocity = Const.Velocity.Train;
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
