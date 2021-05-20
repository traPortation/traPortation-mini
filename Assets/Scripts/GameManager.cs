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
    private int PeopleCount = 10;

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
        var vList = new List<BoardElements.Vertex>();
        for (float x = 0; x < 13; x++) {
            for (float y = 0; y < 9; y++) {
                vList.Add(this.board.AddVertex(x, y));
            }
        }
        for (int x = 0; x < 13; x++) {
            for (int y = 0; y < 9; y++) {
                if (x != 12) this.board.AddEdge(vList[x * 9 + y], vList[(x + 1) * 9 + y], 1);
                if (x != 0) this.board.AddEdge(vList[x * 9 + y], vList[(x - 1) * 9 + y], 1);
                if (y != 8) this.board.AddEdge(vList[x * 9 + y], vList[x * 9 + y + 1], 1);
                if (y != 0) this.board.AddEdge(vList[x * 9 + y], vList[x * 9 + y - 1], 1);
            }
        }
    }
}
