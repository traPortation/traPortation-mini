using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using System.Linq;

#nullable enable

public class GameManager : MonoBehaviour
{
    public Board Board { get; } = Board.Instance;

#nullable disable
    [SerializeField] private GameObject person;
    [SerializeField] private GameObject building;
    [SerializeField] private GameObject train;

    public StationManager StationManager { get; private set; }
#nullable enable

    // Start is called before the first frame update
    void Start()
    {
        this.InstantiatePeople();
        this.InstantiateBuildings();
        var gameObj = GameObject.FindGameObjectsWithTag("StationManager")[0];
        this.StationManager = gameObj.GetComponent<StationManager>();

        Utils.NullChecker.Check(this.person, this.building, this.train, this.StationManager);

        this.initBoardForTest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Prefabから人をインスタンス化する
    /// </summary>
    private void InstantiatePeople()
    {
        for (int i = 0; i < Count.Person; i++)
        {
            var start = new Vector3(Random.Range(X.Min, X.Max), Random.Range(Y.Min, Y.Max), Z.Person);
            Instantiate(this.person, start, Quaternion.identity);
        }
    }
    /// <summary>
    /// Prefabから建物をインスタンス化する
    /// </summary>
    private void InstantiateBuildings()
    {
        for (float x = 0.5f; x < X.Max; x++)
        {
            for (float y = 0.5f; y < Y.Max; y++)
            {
                Instantiate(this.building, new Vector3(x, y, Z.Building), Quaternion.identity);
            }
        }
    }
    /// <summary>
    /// 動作確認用に色々置いてるだけ
    /// </summary>
    private void initBoardForTest()
    {
        // 交差点を追加
        var nodes = Enumerable.Range(0, (int)X.Max + 1).Select(x =>
        {
            return Enumerable.Range(0, (int)Y.Max + 1).Select(y =>
            {
                return this.Board.AddIntersectionNode(x, y);
            }).ToList();
        }).ToList();

        // 道を追加

        foreach (var x in Enumerable.Range(0, (int)X.Max + 1))
        {
            foreach (var y in Enumerable.Range(0, (int)Y.Max + 1))
            {
                if (x != 0) this.Board.AddRoadEdge(nodes[x][y], nodes[x - 1][y]);
                if (y != 0) this.Board.AddRoadEdge(nodes[x][y], nodes[x][y - 1]);
                if (x != X.Max) this.Board.AddRoadEdge(nodes[x][y], nodes[x + 1][y]);
                if (y != Y.Max) this.Board.AddRoadEdge(nodes[x][y], nodes[x][y + 1]);
            }
        }

        // 駅を追加
        var snode1 = this.StationManager.AddStation(new Vector3(2, 2, 5f));
        var snode2 = this.StationManager.AddStation(new Vector3(2, 6, 5f));
        var snode3 = this.StationManager.AddStation(new Vector3(10, 6, 5f));

        // 駅と道をつなげる
        // そのうち勝手にいい感じにやるようにする
        this.Board.AddRoadEdge(nodes[2][2], snode1);
        this.Board.AddRoadEdge(snode1, nodes[2][2]);
        this.Board.AddRoadEdge(nodes[2][6], snode2);
        this.Board.AddRoadEdge(snode2, nodes[2][6]);
        this.Board.AddRoadEdge(nodes[10][6], snode3);
        this.Board.AddRoadEdge(snode3, nodes[10][6]);

        // edgeを追加 (そのうちいい感じにやるようにする)
        var node1 = new PathNode(snode1, this.Board.AddStationEdge(snode1, snode2, EdgeType.Train));
        var node2 = new PathNode(snode2, this.Board.AddStationEdge(snode2, snode3, EdgeType.Train));
        var node3 = new PathNode(snode3, this.Board.AddStationEdge(snode3, snode2, EdgeType.Train));
        var node4 = new PathNode(snode2, this.Board.AddStationEdge(snode2, snode1, EdgeType.Train));
        var node5 = new PathNode(snode1, null);

        // 電車を追加
        GameObject trainObject = Instantiate(this.train, Vector3.zero, Quaternion.identity);
        var train = trainObject.GetComponent<Train>();
        var path = new Path(new List<PathNode>() { node1, node2, node3, node4, node5 }, train.transform);
        train.Initialize(path);
    }

    public void AlterPauseStatus()
    {
        Time.timeScale = 1 - Time.timeScale;
        Debug.Log(Time.timeScale.ToString());
    }
}
