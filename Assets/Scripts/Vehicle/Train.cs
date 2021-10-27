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
