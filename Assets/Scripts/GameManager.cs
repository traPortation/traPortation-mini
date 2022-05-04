using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;
using System.Linq;
using Zenject;
using Traffic;
using Moving;

#nullable enable

public class GameManager : MonoBehaviour
{

#nullable disable
    Board Board;
    [SerializeField] private GameObject person;
    [SerializeField] private GameObject building;
    [SerializeField] private GameObject train;

    public ManageMoney ManageMoney { get; private set; }
    [SerializeField] private Image pauseButton;
    [SerializeField] private Sprite[] pauseSprite = new Sprite[2];

    StationManager StationManager;
    RailManager railManager;
    Road.Factory roadFactory;
    // TODO: 消す
    DiContainer container;
    public bool paused { get; set; } = false;
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
    public void Construct(Board board, DiContainer container, StationManager stationManager, RailManager railManager, Road.Factory roadFactory)
    {
        this.Board = board;
        this.container = container;
        this.StationManager = stationManager;
        this.railManager = railManager;
        this.roadFactory = roadFactory;

        this.ManageMoney = new ManageMoney();

        // 実行順序の関係でここでboardを渡している
        this.StationManager.Construct(board);

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
                if (x != X.Max) roadFactory.Create(nodes[x][y], nodes[x + 1][y]);
                if (y != Y.Max) roadFactory.Create(nodes[x][y], nodes[x][y + 1]);
            }
        }

        // 駅を追加
        var station1 = this.StationManager.AddStation(new Vector3(2, 2, 5f));
        var station2 = this.StationManager.AddStation(new Vector3(2, 6, 5f));
        var station3 = this.StationManager.AddStation(new Vector3(10, 6, 5f));

        // 駅同士を繋げる
        this.Board.AddVehicleRoute(station1.Node, station2.Node, EdgeType.Train);
        this.Board.AddVehicleRoute(station2.Node, station1.Node, EdgeType.Train);
        this.Board.AddVehicleRoute(station2.Node, station3.Node, EdgeType.Train);
        this.Board.AddVehicleRoute(station3.Node, station2.Node, EdgeType.Train);

        // 電車を追加
        GameObject trainObject = container.InstantiatePrefab(this.train);
        var train = trainObject.GetComponent<Train>();

        var stations = new List<Station>() { station1, station2, station3 };

        var rail = this.railManager.AddRail(stations);

        rail.AddTrain(train);
    }

    public void ChangeTimeScale()
    {
        Time.timeScale = 1 - Time.timeScale;
    }

    public void ChangePauseStatus()
    {
        pauseButton.sprite = pauseSprite[(int)Time.timeScale];
        paused = !paused;
    }
}
