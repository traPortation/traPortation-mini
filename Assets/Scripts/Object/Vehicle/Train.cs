using System.Collections;
using UnityEngine;
using Zenject;

namespace TraPortation
{
    public class Train : MovingObject
    {
        public int ID { get; private set; }
        public int Capacity { get; private set; }
        GameManager manager;
        void Start()
        {
            this.Capacity = Const.Train.Capacity;
            this.velocity = Const.Velocity.Train;
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
