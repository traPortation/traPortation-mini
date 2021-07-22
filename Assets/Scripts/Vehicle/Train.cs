using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BoardElements;
using Const;
public class Train : Vehicle
{
    private float stopStationTime = Trains.StopStationTime;
    private bool moveTrain = true;
    void Start(){
        this.Capacity = Trains.Capacity;
        this.Wage = Trains.Wage;
        this.velocity = Trains.TrainVelocity;
        this.Initialize(this.path);        
    }
    
    void FixedUpdate(){
        if(moveTrain){
            this.Move(this.velocity);
        }
    }
    protected override void Arrive(BoardElements.Node node){
        if(this.path.Finished){
            this.path.ReverseEdge();
            Initialize(this.path);
        }
        moveTrain = false;
        StartCoroutine("stopstation");
    }
    private IEnumerator stopstation()
    {   moveTrain = false;
        yield return new WaitForSeconds(stopStationTime);
        moveTrain = true;
    }   
}
