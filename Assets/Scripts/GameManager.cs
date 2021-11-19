﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject person;
    [SerializeField] private GameObject building;
    [SerializeField] private GameObject train;
    public Board Board { get; private set; }
    public StationManager StationManager { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        this.InstantiatePeople();
        this.InstantiateBuildings();
        this.Board = Board.Instance;
        var gameObj = GameObject.FindGameObjectsWithTag("StationManager")[0];
        this.StationManager = gameObj.GetComponent<StationManager>();
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
        var node1 = this.StationManager.AddStation(new Vector3(2, 2, 5f));
        var node2 = this.StationManager.AddStation(new Vector3(2, 6, 5f));
        var node3 = this.StationManager.AddStation(new Vector3(10, 6, 5f));

        // 駅と道をつなげる
        // そのうち勝手にいい感じにやるようにする
        this.Board.AddRoadEdge(nodes[2][2], node1);
        this.Board.AddRoadEdge(node1, nodes[2][2]);
        this.Board.AddRoadEdge(nodes[2][6], node2);
        this.Board.AddRoadEdge(node2, nodes[2][6]);
        this.Board.AddRoadEdge(nodes[10][6], node3);
        this.Board.AddRoadEdge(node3, nodes[10][6]);


        // edgeを追加 (そのうちいい感じにやるようにする)
        var edge1 = this.Board.AddVehicleRoute(node1, node2, EdgeType.Train);
        var edge2 = this.Board.AddVehicleRoute(node2, node1, EdgeType.Train);

        var edge3 = this.Board.AddVehicleRoute(node2, node3, EdgeType.Train);
        var edge4 = this.Board.AddVehicleRoute(node3, node2, EdgeType.Train);


        // 電車を追加
        GameObject trainObject = Instantiate(this.train, Vector3.zero, Quaternion.identity);
        var train = trainObject.GetComponent<Train>();
        var path = new Path(new List<BoardElements.IIndexedEdge>() { edge1, edge3, edge4, edge2 });
        train.Initialize(path);
    }

    public void AlterPauseStatus()
    {
        Time.timeScale = 1 - Time.timeScale;
        Debug.Log(Time.timeScale.ToString());
    }
}
