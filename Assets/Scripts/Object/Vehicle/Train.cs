using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
public class Train : Vehicle
{
    private float stopStationTime = Const.Train.StopStationTime;
    private bool isMoving = true;
    private GameManager manager;
    void Start()
    {
        this.manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        this.Capacity = Const.Train.Capacity;
        this.Wage = Const.Train.Wage;
        this.velocity = Const.Velocity.Train;
        this.Initialize(this.path);
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
        if (node is StationNode)
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

                // テスト用
                if (node != next)
                {
                    throw new System.Exception("なにかがおかしい");
                }
            }

            var station = this.manager.StationManager.GetStation((node as StationNode).Index);


            if (this.path.NextNode is StationNode nextNode)
            {
                this.RemovePerson(nextNode);
            }
            else
            {
                throw new System.Exception();
            }

            // 乗せる処理
            // 発車直前に乗せたほうが自然かも
            station.AddPersonToTrain(this);
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
