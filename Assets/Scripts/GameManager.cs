using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class GameManager : MonoBehaviour
{
    public GameObject person;
    public GameObject building;
    // 盤面を管理するクラス
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

    private void InstantiatePeople()
    {
        for (int i = 0; i < Count.Person; i++)
        {
            Instantiate(person, Vector3.zero, Quaternion.identity);
        }
    }

    private void InstantiateBuildings()
    {
        for (float x = 0.5f; x < 12; x++)
        {
            for (float y = 0.5f; y < 8; y++)
            {
                Instantiate(building, new Vector3(x, y, Z.Building), Quaternion.identity);
            }
        }
    }
    // 仮 
    private void InitBoard()
    {
        var from = this.Board.AddNode(2, 2);
        var to = this.Board.AddNode(10, 6);
        this.Board.AddEdge(from, to, EdgeType.Train);
        this.Board.AddEdge(to, from, EdgeType.Train);
    }
}
