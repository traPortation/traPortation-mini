using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;
using System.Linq;
using Zenject;

#nullable enable

public class GameManager : MonoBehaviour
{

#nullable disable
    Board Board;
    [SerializeField] private GameObject person;
    [SerializeField] private GameObject building;
    [SerializeField] private GameObject train;
    [SerializeField] private Image pauseButton;
    [SerializeField] private Sprite[] pauseSprite = new Sprite[2];

    StationManager StationManager;
    RailManager railManager;
    // TODO: 消す
    DiContainer container;
#nullable enable

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Inject]
    void construct(Board board, DiContainer container, StationManager stationManager, RailManager railManager)
    {
        this.Board = board;
        this.container = container;
        this.StationManager = stationManager;
        this.railManager = railManager;

        // 実行順序の関係でここでboardを渡している
        this.StationManager.Construct(board, container);

        this.InstantiateBuildings();
        this.initBoardForTest();
        this.InstantiatePeople();

        Utils.NullChecker.Check(this.person, this.building, this.train, this.StationManager);
    }

    /// <summary>
    /// Prefabから人をインスタンス化する
    /// </summary>
    private void InstantiatePeople()
    {
        for (int i = 0; i < Count.Person; i++)
        {
            var start = new Vector3(Random.Range(X.Min, X.Max), Random.Range(Y.Min, Y.Max), Z.Person);
            var obj = this.container.InstantiatePrefab(this.person);
            obj.transform.position = start;
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
        var snode1 = this.StationManager.AddStation(new Vector3(2, 2, 5f)).Node;
        var snode2 = this.StationManager.AddStation(new Vector3(2, 6, 5f)).Node;
        var snode3 = this.StationManager.AddStation(new Vector3(10, 6, 5f)).Node;

        // 駅と道をつなげる
        // TODO: 勝手にいい感じにやるようにする
        this.Board.AddRoadEdge(nodes[2][2], snode1);
        this.Board.AddRoadEdge(snode1, nodes[2][2]);
        this.Board.AddRoadEdge(nodes[2][6], snode2);
        this.Board.AddRoadEdge(snode2, nodes[2][6]);
        this.Board.AddRoadEdge(nodes[10][6], snode3);
        this.Board.AddRoadEdge(snode3, nodes[10][6]);

        // edgeを追加
        // TODO: いい感じにやるようにする
        var node1 = new PathNode(snode1, this.Board.AddVehicleRoute(snode1, snode2, EdgeType.Train));
        var node2 = new PathNode(snode2, this.Board.AddVehicleRoute(snode2, snode3, EdgeType.Train));
        var node3 = new PathNode(snode3, this.Board.AddVehicleRoute(snode3, snode2, EdgeType.Train));
        var node4 = new PathNode(snode2, this.Board.AddVehicleRoute(snode2, snode1, EdgeType.Train));
        var node5 = new PathNode(snode1, null);

        // 電車を追加
        GameObject trainObject = container.InstantiatePrefab(this.train);
        var train = trainObject.GetComponent<Train>();

        var trainNodes = new List<PathNode>() { node1, node2, node3, node4, node5 };

        var rail = this.railManager.AddRail(trainNodes);
        var path = new Path(trainNodes, train.transform);

        rail.AddTrain(train);

        train.Initialize(path);
    }

    public void AlterPauseStatus()
    {
        Time.timeScale = 1 - Time.timeScale;
        pauseButton.sprite = pauseSprite[(int)Time.timeScale];
        Debug.Log(Time.timeScale.ToString());
    }
}
