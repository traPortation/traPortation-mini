﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Traffic;
using System.Linq;
using Zenject;

public class StationManager : MonoBehaviour
{
    private bool buildMode;
    private bool buttonClicked;
    private List<Station> stations = new List<Station>();
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject stationIcon;
    Board board;
    // TODO: 消す
    DiContainer container;

    // Start is called before the first frame update
    void Start()
    {

    }

    // TODO: 別のクラスに分ける
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //buildModeを切り替えるためのボタンクリックを無視する
        if (this.buttonClicked)
        {
            this.buttonClicked = false;
        }
        //それ以外の場合はbuildModeを判定し、駅を追加する
        else if ((Mathf.Abs(mousePosition.x - Mathf.Round(mousePosition.x)) < 0.1 || Mathf.Abs(mousePosition.y - Mathf.Round(mousePosition.y)) < 0.1) && this.buildMode)
        {
            mousePosition.z = 8f;
            stationIcon.SetActive(true);
            stationIcon.transform.position = mousePosition;
            if (Input.GetMouseButtonUp(0))
            {
                GameObject newStation = Instantiate(this.prefab, mousePosition, Quaternion.identity);
                stations.Add(newStation.GetComponent<Station>());
            }
        }
        else
        {
            stationIcon.SetActive(false);
        }
    }

    // TODO: 実行順序の問題が解決したらinjectするようにする
    public void Construct(Board board, DiContainer container)
    {
        this.board = board;
        this.container = container;
    }

    public void SetBuildMode()
    {
        this.buttonClicked = true;
        this.buildMode = !this.buildMode;
        this.board.Test();
    }

    /// <summary>
    /// 指定した場所にstationを追加する
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public Station AddStation(Vector3 vec)
    {
        // TODO: 駅と(一番近い)道をつなげる

        int index = this.stations.Count;
        var node = this.board.AddStationNode(vec.x, vec.y);

        GameObject newStation = this.container.InstantiatePrefab(this.prefab);
        newStation.transform.position = vec;
        var station = newStation.GetComponent<Station>();
        station.SetNode(node);

        this.stations.Add(station);

        return station;
    }

    /// <summary>
    /// 指定したindexのstationを取得
    /// </summary>
    /// <param name="stationId"></param>
    /// <returns></returns>
    public Station GetStation(int stationId)
    {
        //TODO: 応急処置なのでそのうちどうにかする
        return this.stations.Where(station => station.Node.Index == stationId).First();
    }
}
