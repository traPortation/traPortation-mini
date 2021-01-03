using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject person;
    public int PeopleCount = 10;
    // Start is called before the first frame update
    void Start()
    {
        CreatePeople();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePeople(){
        for (int i = 0; i < PeopleCount; i++) {
            Instantiate(person, Vector3.zero, Quaternion.identity);
        }
    }
}
