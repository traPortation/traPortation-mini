using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BoardElements;
public class Bus : Vehicle
{
    private float stopStationTime = Const.Bus.StopStationTime;
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
    protected override void Arrive(BoardElements.INode node)
    {
        if (this.path.Finished)
        {
            this.path.InitializeEdge();
            Initialize(this.path);
        }
        this.isMoving = false;
        StartCoroutine("stopstation");
    }
    private IEnumerator stopstation()
    {
        this.isMoving = false;
        yield return new WaitForSeconds(stopStationTime);
        this.isMoving = true;
    }
}