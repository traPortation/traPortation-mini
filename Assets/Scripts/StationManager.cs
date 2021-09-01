using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

public class StationManager : MonoBehaviour
{
    private bool buildMode;
    private bool buttonClicked;
    private List<BoardNode> stations = new List<BoardNode>();
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
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 8f;
            AddStation(Camera.main.ScreenToWorldPoint(mousePosition));
        }
    }

    public void SetBuildMode()
    {
        buttonClicked = true;
        buildMode = !buildMode;
        Board.Instance.Test();
    }

    public BoardNode AddStation(Vector3 vec)
    {
        int index = stations.Count;
        var node = Board.Instance.AddNode(vec.x, vec.y);

        GameObject newStation = Instantiate(prefab, vec, Quaternion.identity);
        var station = newStation.GetComponent<Station>();
        station.SetNode(node);

        return node;
    }
}
