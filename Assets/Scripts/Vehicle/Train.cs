using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BoardElements;
using Const;
public class Train : Vehicle
{
    private float stopStationTime = Trains.StopStationTime;
    private bool isMoving = true;
    void Start(){
        this.Capacity = Trains.Capacity;
        this.Wage = Trains.Wage;
        this.velocity = Trains.TrainVelocity;
        this.Initialize(this.path);        
    }
    
    void FixedUpdate(){
        if(isMoving){
            this.Move(this.velocity);
        }
    }
    protected override void Arrive(BoardElements.Node node){
        if(this.path.Finished){
            this.path.InitializeEdge();
            Initialize(this.path);
        }
        isMoving = false;
        StartCoroutine("stopstation");
    }
    private IEnumerator stopstation()
    {   isMoving = false;
        yield return new WaitForSeconds(stopStationTime);
        isMoving = true;
    }   
}
