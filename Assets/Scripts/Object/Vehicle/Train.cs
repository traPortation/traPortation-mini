using System.Collections;
using UnityEngine;

#nullable enable

public class Train : Vehicle
{
    void Start()
    {
        this.Capacity = TraPortation.Const.Train.Capacity;
        this.Wage = TraPortation.Const.Train.Wage;
        this.velocity = TraPortation.Const.Velocity.Train;
    }

    void FixedUpdate()
    {
        this.Move(this.velocity);
    }
}
