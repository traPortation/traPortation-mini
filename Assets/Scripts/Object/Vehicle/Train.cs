using System.Collections;
using UnityEngine;
using Zenject;
using MessagePipe;
using Traffic.Node;
using Event;

#nullable enable

public class Train : Vehicle
{
    private float stopStationTime = Const.Train.StopStationTime;
    private bool isMoving = true;
#nullable disable
    StationManager stationManager;
    IPublisher<int, StationArrivedEvent> stationPublisher;
    IPublisher<int, VehicleArrivedEvent> vehiclePublisher;
#nullable enable

    [Inject]
    void Construct(StationManager stationManager, IPublisher<int, StationArrivedEvent> stationPublisher, IPublisher<int, VehicleArrivedEvent> vehiclePublisher)
    {
        this.stationManager = stationManager;
        this.Capacity = Const.Train.Capacity;
        this.Wage = Const.Train.Wage;
        this.velocity = Const.Velocity.Train;
        this.stationPublisher = stationPublisher;
        this.vehiclePublisher = vehiclePublisher;
    }

    void FixedUpdate()
    {
        if (this.isMoving)
        {
            this.Move(this.velocity);
        }
    }
    protected override void Arrive(INode node)
    {
        if (this.path.Finished)
        {
            this.path.InitializeEdge();
            Initialize(this.path);
        }
        if (node is StationNode sNode)
        {
            if (this.path.NextNode is StationNode nextNode)
            {
                var station = this.stationManager.GetStation(sNode);
                var nextStation = this.stationManager.GetStation(nextNode);
                // 乗客に到着したイベントを送信
                this.vehiclePublisher.Publish(this.ID, new VehicleArrivedEvent(station, nextStation));

                // 駅に到着したイベントを送信
                this.stationPublisher.Publish(sNode.Index, new StationArrivedEvent(this.ID, nextStation));
            }
            else
            {
                throw new System.Exception();
            }

            
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
