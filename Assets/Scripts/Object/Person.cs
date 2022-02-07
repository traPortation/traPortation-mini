using System;
using UnityEngine;
using Const;
using Traffic;
using Traffic.Node;
using Zenject;
using MessagePipe;

#nullable enable

public class Person : MovingObject
{
#nullable disable
    StationManager stationManager;
    Board board;
    ISubscriber<int, VehicleArrivedEvent> subscriber;
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
    public void Construct(Board board, StationManager stationManager, ISubscriber<int, VehicleArrivedEvent> subscriber)
    {
        this.board = board;
        this.stationManager = stationManager;
        this.subscriber = subscriber;

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

            this.subscriber.Subscribe(station.ID, e =>
            {
                if (this.path.NextNode == e.Vehicle.NextNode)
                {
                    // これ駅とかでやるべきかも？
                    bool res = e.Vehicle.AddPerson(this);

                    if (res)
                    {
                        // 人を見えなくする 動きを止める
                        this.gameObject.SetActive(false);
                        this.disposable.Dispose();
                    }
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
        return new Path(edges, this.transform);

    }

    /// <summary>
    /// 乗り物に乗っているときに呼ばれる
    /// 到着した駅で降りるかどうかを判断する
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool DecideToGetOff(StationNode node)
    {
        if (this.path.NextNode == node) return false;
        else return true;
    }

    public void GetOff(StationNode node)
    {
        this.gameObject.SetActive(true);
        this.velocity = Velocity.Person;
    }

    /// <summary>
    /// pathを次のnodeまで進める
    /// </summary>
    /// <returns></returns>
    public INode? MoveNext()
    {
        return this.path.MoveNext();
    }
}
