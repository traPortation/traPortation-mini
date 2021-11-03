using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

public class StationManager : MonoBehaviour
{
    private bool buildMode;
    private bool buttonClicked;
    [SerializeField] private List<Station> stations;
    [SerializeField] private GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //buildModeを切り替えるためのボタンクリックを無視する
        if (buttonClicked)
        {
            buttonClicked = false;
        }
        //それ以外の場合はbuildModeを判定し、駅を追加する
        else if (Input.GetMouseButtonUp(0) && buildMode)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Mathf.Abs(mousePosition.x - Mathf.Round(mousePosition.x)) < 0.1 || Mathf.Abs(mousePosition.y - Mathf.Round(mousePosition.y)) < 0.1)
            {
                mousePosition.z = 8f;
                GameObject newStation = Instantiate(prefab, mousePosition, Quaternion.identity);
                stations.Add(newStation.GetComponent<Station>());
            }
        }
    }

    public void SetBuildMode()
    {
        buttonClicked = true;
        buildMode = !buildMode;
        Board.Instance.Test();
    }

    /// <summary>
    /// 指定した場所にstationを追加する
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public StationNode AddStation(Vector3 vec)
    {
        int index = this.stations.Count;
        var node = Board.Instance.AddNode(vec.x, vec.y);

        GameObject newStation = Instantiate(prefab, vec, Quaternion.identity);
        var station = newStation.GetComponent<Station>();
        station.SetNode(node);

        stations.Add(station);

        return node;
    }

    /// <summary>
    /// 指定したindexのstationを取得
    /// </summary>
    /// <param name="stationId"></param>
    /// <returns></returns>
    public Station GetStation(int stationId)
    {
        return this.stations[stationId];
    }
}
