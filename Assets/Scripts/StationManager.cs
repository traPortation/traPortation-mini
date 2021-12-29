using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;

public class StationManager : MonoBehaviour
{
    private bool buildMode;
    private bool buttonClicked;
    private List<Station> stations = new List<Station>();
    [SerializeField] private GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //buildModeを切り替えるためのボタンクリックを無視する
        if (this.buttonClicked)
        {
            this.buttonClicked = false;
        }
        //それ以外の場合はbuildModeを判定し、駅を追加する
        else if (Input.GetMouseButtonUp(0) && this.buildMode)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Mathf.Abs(mousePosition.x - Mathf.Round(mousePosition.x)) < 0.1 || Mathf.Abs(mousePosition.y - Mathf.Round(mousePosition.y)) < 0.1)
            {
                mousePosition.z = 8f;
                GameObject newStation = Instantiate(this.prefab, mousePosition, Quaternion.identity);
                stations.Add(newStation.GetComponent<Station>());
            }
        }
    }

    public void SetBuildMode()
    {
        this.buttonClicked = true;
        this.buildMode = !this.buildMode;
        Board.Instance.Test();
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
        var node = Board.Instance.AddStationNode(vec.x, vec.y);

        GameObject newStation = Instantiate(this.prefab, vec, Quaternion.identity);
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
