using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class GameManager : MonoBehaviour
{
    public GameObject person;
    public GameObject building;
    // 盤面を管理するクラス
    public Board board;
    private int PeopleCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        InstantiatePeople();
        InstantiateBuildings();
        this.board = new Board();
        InitBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiatePeople() {
        for (int i = 0; i < PeopleCount; i++) {
            Instantiate(person, Vector3.zero, Quaternion.identity);
        }
    }

    void InstantiateBuildings() {
        for (float x = 0.5f; x < 12; x++) {
            for (float y = 0.5f; y < 8; y++) {
                Instantiate(building, new Vector3(x, y, Z.Building), Quaternion.identity);
            }
        }
    }
    
    void InitBoard() {
        var from = this.board.AddNode(2, 2);
        var to = this.board.AddNode(10, 6);
        this.board.AddEdge(from, to, EdgeType.Train);
        this.board.AddEdge(to, from, EdgeType.Train);
    }
}
