using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using Zenject;
using MessagePipe;

#nullable enable

public class Train : Vehicle
{
    private float stopStationTime = Const.Train.StopStationTime;
    private bool isMoving = true;
#nullable disable
    StationManager stationManager;
    IPublisher<VehicleArrivedEvent> publisher;
#nullable enable
    void Start()
    {

    }

    [Inject]
    void Construct(StationManager stationManager, IPublisher<VehicleArrivedEvent> publisher)
    {
        this.stationManager = stationManager;
        this.Capacity = Const.Train.Capacity;
        this.Wage = Const.Train.Wage;
        this.velocity = Const.Velocity.Train;
        this.publisher = publisher;
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
            // 乗っている人を移動させる
            foreach (var person in this.people)
            {
                // 人を今着いた駅まで進める
                var next = person.MoveNext();

                if (next == null)
                {
                    throw new System.Exception("next is null");
                }
            }

            if (this.path.NextNode is StationNode nextNode)
            {
                this.RemovePerson(nextNode);
            }
            else
            {
                throw new System.Exception();
            }

            // 駅に到着したイベントを送信
            this.publisher.Publish(new VehicleArrivedEvent(sNode.Index, this));
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
