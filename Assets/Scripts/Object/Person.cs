using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using System.Linq;
using BoardElements;
using Zenject;

#nullable enable

public class Person : MovingObject
{
#nullable disable
    GameManager manager;
    Board board;

#nullable enable
    // Start is called before the first frame update
    void Start()
    {
        this.manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Utils.NullChecker.Check(this.manager);

        this.velocity = Velocity.Person;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.Move(this.velocity);
    }

    [Inject]
    public void Construct(Board board) {
        this.board = board;

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
            var station = this.manager.StationManager.GetStation(sNode.Index);
            station.AddPerson(this);
        }
    }
    /// <summary>
    /// ランダムにゴールを設定し、そこまでの経路をセットする
    /// </summary>
    private Path getRandomPath()
    {
        var start = this.path != null ? this.path.LastNode : this.board.Nodes[Random.Range(0, this.board.Nodes.Count)];

        // 始点と終点が被らないようにするための処理
        IBoardNode goal;
        do
        {
            goal = this.board.Nodes[Random.Range(0, this.board.Nodes.Count)];
        } while (start.Index == goal.Index);

        var edges = this.board.GetPath(start, goal);
        return new Path(edges, this.transform);

    }

    /// <summary>
    /// 駅で待っているときに呼ばれる
    /// 来た乗り物に乗るかどうかを判断する
    /// </summary>
    /// <param name="vehicle"></param>
    /// <returns></returns>
    public bool DecideToRide(Vehicle vehicle)
    {
        if (this.path.NextNode == vehicle.NextNode) return true;
        else return false;
    }

    /// <summary>
    /// 乗り物に乗る処理
    /// </summary>
    /// <param name="vehicle"></param>
    public void Ride(Vehicle vehicle)
    {
        // 人を見えなくする 動きを止める
        this.gameObject.SetActive(false);

        // これ駅とかでやるべきかも？
        vehicle.AddPerson(this);
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
