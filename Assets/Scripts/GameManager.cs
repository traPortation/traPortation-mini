using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject person;
    public GameObject building;
    public int PeopleCount = 10;
    // Start is called before the first frame update
    void Start()
    {
        InstantiatePeople();
        InstantiateBuildings();
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
        for (float x = 0.5f; x < 16; x++) {
            for (float y = 0.5f; y < 8; y++) {
                Instantiate(building, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
