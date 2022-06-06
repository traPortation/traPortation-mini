using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TraPortation.Traffic;
using TraPortation.Traffic.Node;
using UnityEngine;
using Zenject;

public class StationManager : MonoBehaviour
{
    private bool buildMode;
    private bool buttonClicked;
    private List<Station> stations = new List<Station>();
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject stationIcon;
    [SerializeField] private GameObject gameManager;
    Board board;
    DiContainer container;

    // Start is called before the first frame update
    void Start()
    {

    }

    // TODO: 別のクラスに分ける
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 8f;
        stationIcon.transform.position = mousePosition;
        Color stationColor = stationIcon.GetComponent<SpriteRenderer>().color;
        //buildModeを切り替えるためのボタンクリックを無視する
        if (this.buttonClicked)
        {
            this.buttonClicked = false;
            stationIcon.SetActive(false);
        }
        //それ以外の場合はbuildModeを判定し、駅を追加する
        else if ((Mathf.Abs(mousePosition.x - Mathf.Round(mousePosition.x)) < 0.1 || Mathf.Abs(mousePosition.y - Mathf.Round(mousePosition.y)) < 0.1) && this.buildMode)
        {
            stationIcon.SetActive(true);
            stationColor.a = 1f;
            stationIcon.GetComponent<SpriteRenderer>().material.color = stationColor;
            if (Input.GetMouseButtonUp(0))
            {
                GameObject newStation = Instantiate(this.prefab, mousePosition, Quaternion.identity);
                stations.Add(newStation.GetComponent<Station>());
            }
        }
        else
        {
            stationColor.a = 0.5f;
            stationIcon.GetComponent<SpriteRenderer>().material.color = stationColor;
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
        if (!gameManager.GetComponent<GameManager>().paused)
        {
            gameManager.GetComponent<GameManager>().ChangeTimeScale();
        }
        this.board.Test();
    }

    /// <summary>
    /// 指定した場所にstationを追加する
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public Station AddStation(Vector3 vec)
    {
        int index = this.stations.Count;
        var node = this.board.AddStationNode(vec.x, vec.y);

        var nearestNode = this.board.GetNearestNode(vec.x, vec.y);
        this.board.AddRoadEdge(nearestNode, node);
        this.board.AddRoadEdge(node, nearestNode);

        GameObject newStation = container.InstantiatePrefab(this.prefab);
        newStation.transform.position = vec;
        var station = new Station(node);

        this.stations.Add(station);

        return station;
    }

    /// <summary>
    /// 指定したIDのstationを取得
    /// </summary>
    /// <param name="stationId"></param>
    /// <returns></returns>
    public Station GetStation(int stationId)
    {
        //TODO: 応急処置なのでそのうちどうにかする
        return this.stations.Where(station => station.ID == stationId).First();
    }

    /// <summary>
    /// 指定したStationNodeを持つstationを取得 
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public Station GetStation(StationNode node)
    {
        return this.stations.Where(station => station.Node.Index == node.Index).First();
    }
}
