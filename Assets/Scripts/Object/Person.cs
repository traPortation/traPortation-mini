using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using System.Linq;
using BoardElements;
public class Person : MovingObject
{
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        this.manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (this.manager == null) throw new System.Exception("GameManager not found");

        var path = this.getRandomPath();
        this.Initialize(path);

        this.velocity = Velocity.Person;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.Move(this.velocity);
    }

    protected override void Arrive(BoardElements.INode node)
    {
        // 目的地に到達した場合は次の目的地を設定する
        if (this.path.Finished)
        {
            var path = this.getRandomPath();
            this.Initialize(path);
        }

        // 着いた先が駅の場合は駅に自分自身を追加する
        if (node is StationNode)
        {
            this.velocity = 0;
            var station = this.manager.StationManager.GetStation((node as StationNode).Index);
            station.AddPerson(this);
        }
    }
    /// <summary>
    /// ランダムにゴールを設定し、そこまでの経路をセットする
    /// </summary>
    private Path getRandomPath()
    {
        var start = this.path != null ? this.path.LastNode : Board.Instance.Nodes[Random.Range(0, Board.Instance.Nodes.Count)];
        var goal = Board.Instance.Nodes[Random.Range(0, Board.Instance.Nodes.Count)];
        return this.manager.Board.GetPath(start, goal);
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
        gameObject.SetActive(false);

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
        gameObject.SetActive(true);
        this.velocity = Velocity.Person;
    }

    /// <summary>
    /// pathを次のnodeまで進める
    /// </summary>
    /// <returns></returns>
    public INode Next()
    {
        return this.path.Next();
    }
}
