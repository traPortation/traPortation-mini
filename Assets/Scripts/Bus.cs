using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BoardElements;
using Const;
public class Bus : Vehicle
{
    private float stopStationTime = Buses.StopStationTime;
    private bool isMoving = true;
    void Start()
    {
        this.Capacity = Buses.Capacity;
        this.Wage = Buses.Wage;
        this.velocity = Buses.BusVelocity;
        this.Initialize(this.path);
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            this.Move(this.velocity);
        }
    }
    protected override void Arrive(BoardElements.Node node)
    {
        if (this.path.Finished)
        {
            this.path.InitializeEdge();
            Initialize(this.path);
        }
        isMoving = false;
        StartCoroutine("stopstation");
    }
    private IEnumerator stopstation()
    {
        isMoving = false;
        yield return new WaitForSeconds(stopStationTime);
        isMoving = true;
    }
}