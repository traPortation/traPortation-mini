using System;
using UnityEngine;
using Zenject;
using MessagePipe;
using Const;
using Traffic;
using Traffic.Node;
using Event;

#nullable enable

public class Person : MovingObject
{
#nullable disable
    StationManager stationManager;
    Board board;
    ISubscriber<int, StationArrivedEvent> stationSubscriber;
    ISubscriber<int, VehicleArrivedEvent> vehicleSubscriber;
    IDisposable disposable;
#nullable enable
    // Start is called before the first frame update
    void Start()
    {
        this.velocity = Velocity.Person;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.Move(this.velocity);
    }

    void OnDestory()
    {
        this.disposable?.Dispose();
    }

    [Inject]
    void construct(Board board, StationManager stationManager, ISubscriber<int, StationArrivedEvent> stationSubscriber, ISubscriber<int, VehicleArrivedEvent> vehicleSubscriber)
    {
        this.board = board;
        this.stationManager = stationManager;
        this.stationSubscriber = stationSubscriber;
        this.vehicleSubscriber = vehicleSubscriber;

        var path = this.getRandomPath();
        this.Initialize(path);
    }

    protected override void Arrive(INode node)
    {
        // 目的地に到達した場合は次の目的地を設定する
        if (this.path.Finished)
        {
            var path = this.getRandomPath();
            this.Initialize(path);
        }

        // 着いた先が駅の場合は駅に自分自身を追加する
        if (node is StationNode sNode)
        {
            // TODO: velocity直接いじるのよくない
            this.velocity = 0;
            var station = this.stationManager.GetStation(sNode.Index);

            var d = DisposableBag.CreateBuilder();

            this.stationSubscriber.Subscribe(station.ID, e =>
            {
                if (this.path.NextNode is StationNode sNode && this.stationManager.GetStation(sNode.Index) == e.NextStation)
                {
                    // TODO: 乗れるかのチェック

                    // 人を見えなくする 動きを止める
                    this.gameObject.SetActive(false);

                    this.disposable.Dispose();

                    this.vehicleSubscriber.Subscribe(e.Vehicle.ID, e =>
                    {
                        this.path.MoveNext();

                        // 次の目的の駅が同じ場合は降りない
                        if (this.path.NextNode is StationNode sNode && this.stationManager.GetStation(sNode.Index) == e.NextStation)
                        {
                        }
                        else
                        {
                            this.disposable.Dispose();

                            this.gameObject.SetActive(true);
                            this.velocity = Velocity.Person;
                        }
                    }).AddTo(d);
                    this.disposable = d.Build();

                }
            }).AddTo(d);

            this.disposable = d.Build();
        }
    }
    /// <summary>
    /// ランダムにゴールを設定し、そこまでの経路をセットする
    /// </summary>
    private Path getRandomPath()
    {
        var start = this.path != null ? this.path.LastNode : this.board.Nodes[UnityEngine.Random.Range(0, this.board.Nodes.Count)];

        // 始点と終点が被らないようにするための処理
        IBoardNode goal;
        do
        {
            goal = this.board.Nodes[UnityEngine.Random.Range(0, this.board.Nodes.Count)];
        } while (start.Index == goal.Index);

        var edges = this.board.GetPath(start, goal);
        return new Path(edges);

    }
}
