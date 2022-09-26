using System.Collections;
using UnityEngine;

#nullable enable

namespace TraPortation
{
    public class Train : Vehicle
    {
        void Start()
        {
            this.Capacity = Const.Train.Capacity;
            this.Wage = Const.Train.Wage;
            this.velocity = Const.Velocity.Train;
        }

        void FixedUpdate()
        {
            this.Move(this.velocity);
        }

        public void GoTo(Vector3 vec)
        {
            this.path.GoTo(vec);
            this.transform.position = new Vector3(this.path.Position.X, this.path.Position.Y, this.transform.position.z);
        }
    }
}
