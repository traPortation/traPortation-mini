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
    private GameManager manager;
    void Start()
    {
        this.manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        this.Capacity = Trains.Capacity;
        this.Wage = Trains.Wage;
        this.velocity = Velocity.Train;
        this.Initialize(this.path);
    }

    void FixedUpdate()
    {
        if (isMoving)
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
        if (node is StationNode)
        {
            // 乗っている人を移動させる
            foreach (var person in this.people)
            {
                var next = person.Next();

                // テスト用
                if (node != next)
                {
                    throw new System.Exception("なにかがおかしい");
                }
            }

            var station = this.manager.StationManager.GetStation((node as StationNode).Index);

            // 降ろす処理
            this.RemovePerson(node as StationNode);

            // 乗せる処理
            // 発車直前に乗せたほうが自然かも
            station.AddPersonToTrain(this);
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
