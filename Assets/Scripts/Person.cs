using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using System.Linq;
using BoardElements;
public class Person : MovingObject, IPerson
{
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        this.manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (this.manager == null) throw new System.Exception("GameManager not found");
        var path = this.getRandomPath();
        this.Initialize(path);
        if (this.path == null) throw new System.Exception("path not found");
        this.velocity = 0.01f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.Move(this.velocity);
    }

    protected override void Arrive(BoardElements.Node node)
    {
        if (this.path.Finished)
        {
            var path = this.getRandomPath();
            this.Initialize(path);
        }

        if (node is BoardNode)
        {
            this.velocity = 0;

            var station = this.manager.StationManager.GetStation((node as BoardNode).Index);

            station.AddPerson(this);
        }
    }
    /// <summary>
    /// ランダムにゴールを設定し、そこまでの経路をセットする
    /// </summary>
    private Path getRandomPath()
    {
        var start = new Node(transform.position.x, transform.position.y);
        var goal = new Node(Random.Range(X.Min, X.Max), Random.Range(Y.Min, Y.Max));
        return this.manager.Board.GetPath(start, goal);
    }

    public bool DecideToRide(Vehicle vehicle)
    {
        if (this.path.NextNode == vehicle.NextNode) return true;
        else return false;
    }

    public void Ride(Vehicle vehicle)
    {
        // 人を見えなくする 動きを止める
        gameObject.SetActive(false);

        vehicle.AddPerson(this);
    }

    public bool DecideToGetOff(BoardNode node)
    {
        // 後で直す
        // 最後の駅で降りる
        if (this.path.NextNode == node) return false;
        else return true;
    }

    public void GetOff(BoardNode node)
    {
        // TODO: 次が電車の場合と徒歩の場合で分ける
        gameObject.SetActive(true);
        this.velocity = 0.1f;
    }

    public Node Next()
    {
        return this.path.Next();
    }
}
