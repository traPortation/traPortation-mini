﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class GameManager : MonoBehaviour
{
    public GameObject Person;
    public GameObject Building;
    public Board Board { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        InstantiatePeople();
        InstantiateBuildings();
        this.Board = new Board();
        InitBoard();
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
            Instantiate(Person, start, Quaternion.identity);
        }
    }
    /// <summary>
    /// Prefabから建物をインスタンス化する
    /// </summary>
    private void InstantiateBuildings()
    {
        for (float x = 0.5f; x < Const.X.Max; x++)
        {
            for (float y = 0.5f; y < Const.Y.Max; y++)
            {
                Instantiate(Building, new Vector3(x, y, Z.Building), Quaternion.identity);
            }
        }
    }
    /// <summary>
    /// 仮でEdgeをおいてるだけ
    /// </summary>
    private void InitBoard()
    {
        var from = this.Board.AddNode(2, 2);
        var to = this.Board.AddNode(10, 6);
        this.Board.AddEdge(from, to, EdgeType.Train);
        this.Board.AddEdge(to, from, EdgeType.Train);
    }
}
