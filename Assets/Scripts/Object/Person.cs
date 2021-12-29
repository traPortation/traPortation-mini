using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using System.Linq;
using BoardElements;

#nullable enable

public class Person : MovingObject
{
#nullable disable
    private GameManager manager;
#nullable enable
    // Start is called before the first frame update
    void Start()
    {
        this.manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Utils.NullChecker.Check(this.manager);

        var start = new Vector3(Random.Range(X.Min, X.Max), Random.Range(Y.Min, Y.Max), Z.Person);
        var goal = new Vector3(Random.Range(X.Min, X.Max), Random.Range(Y.Min, Y.Max), Z.Person);
        this.Initialize(manager.Board.GetPath(start, goal, this.transform));

        this.velocity = Velocity.Person;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.Move(this.velocity);
    }

    protected override void Arrive(INode node)
    {
        // 目的地に到達した場合は次の目的地を設定する
        if (this.path.Finished)
        {
            var start = new Vector3(Random.Range(X.Min, X.Max), Random.Range(Y.Min, Y.Max), Z.Person);
            var goal = new Vector3(Random.Range(X.Min, X.Max), Random.Range(Y.Min, Y.Max), Z.Person);
            this.Initialize(manager.Board.GetPath(start, goal, this.transform));
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
        var start = this.path != null ? this.path.LastNode : Board.Instance.Nodes[Random.Range(0, Board.Instance.Nodes.Count)];

        // 始点と終点が被らないようにするための処理
        IBoardNode goal;
        do
        {
            goal = Board.Instance.Nodes[Random.Range(0, Board.Instance.Nodes.Count)];
        } while (start.X == goal.X && start.Y == goal.Y);

        var path = this.manager.Board.GetPath(new Vector3(start.X, start.Y, Z.Person), new Vector3(goal.X, goal.Y, Z.Person), this.transform);
        return path;

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
