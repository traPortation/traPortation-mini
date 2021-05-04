using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    private bool buildMode;
    private bool buttonClicked;
    private List<Station> stations;
    [SerializeField] private GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonClicked)
        {
            buttonClicked = false;
        }
        else if (Input.GetMouseButtonUp(0) && buildMode)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 8f;
            GameObject newStation = Instantiate(prefab, Camera.main.ScreenToWorldPoint(mousePosition), Quaternion.identity);
            stations.Add(newStation.GetComponent<Station>());
        }
    }

    public void SetBuildMode()
    {
        buttonClicked = true;
        buildMode = !buildMode;
    }
}
