using System;
using System.Linq;
using UnityEngine;
using Zenject;
using MessagePipe;
using Const;
using Traffic;
using Traffic.Node;
using Event;
using Moving;

#nullable enable

public class Person: MonoBehaviour
{
    float velocity;
#nullable disable
    PersonPath path;
    StationManager stationManager;
    Board board;
    ISubscriber<int, StationArrivedEvent> stationSubscriber;
    ISubscriber<int, VehicleArrivedEvent> vehicleSubscriber;
    PathFactory factory;
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

        if (this.path.Status == SectionStatus.Finished)
        {
            var path = this.getRandomPath();
            this.Initialize(path);
        }
    }

    void OnDestory()
    {
        this.disposable?.Dispose();
    }

    [Inject]
    void construct(Board board, StationManager stationManager, ISubscriber<int, StationArrivedEvent> stationSubscriber, ISubscriber<int, VehicleArrivedEvent> vehicleSubscriber, PathFactory factory)
    {
        this.board = board;
        this.stationManager = stationManager;
        this.stationSubscriber = stationSubscriber;
        this.vehicleSubscriber = vehicleSubscriber;
        this.factory = factory;

        var path = this.getRandomPath();
        this.Initialize(path);
    }

    void Initialize(PersonPath path)
    {
        this.path = path;
        this.transform.position = new Vector3(path.Position.X, path.Position.Y, this.transform.position.z);
    }

    void Move(float delta)
    {
        this.path.Move(delta);
        this.transform.position = new Vector3(path.Position.X, path.Position.Y, this.transform.position.z);
    }    

    /// <summary>
    /// ランダムにゴールを設定し、そこまでの経路をセットする
    /// </summary>
    private PersonPath getRandomPath()
    {
        var start = this.path != null && this.path.LastNode is IIndexedNode iNode ? iNode : this.board.Nodes[UnityEngine.Random.Range(0, this.board.Nodes.Count)];

        // 始点と終点が被らないようにするための処理
        IBoardNode goal;
        do
        {
            goal = this.board.Nodes[UnityEngine.Random.Range(0, this.board.Nodes.Count)];
        } while (start.Index == goal.Index);

        var edges = this.board.GetPath(start, goal);

        var nodes = edges.Select(edge => edge.Node).ToList();

        return this.factory.Create(nodes);
    }
}
