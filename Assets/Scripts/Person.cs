using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MovingObject
{
    // 移動の種類を持つ stringじゃなくてもいい
    private string[] pathType;
    public float velocity = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(0f, 16f);
        float y = Random.Range(0f, 8f);
        transform.position = new Vector3(x, y, 0f);
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 destination;
        do {
            destination = this.SelectDestination();
        } while (transform.position.x == destination.x && transform.position.y == destination.y);
        this.Initialize(this.SearchPath(position, destination));
    }

    // Update is called once per frame
    void Update()
    {
        this.Move(this.velocity);
    }

    // 経路の端に着いたら ArriveDestination() を呼び出したり、pathType[arriveIndex + 1] を見てあれこれしたりする
    protected override void Arrive(int arriveIndex){
        if (arriveIndex == path.Count - 1) {
            this.ArriveDestination();
        }
    }

    private void ArriveDestination() {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 destination;
        do {
            destination = this.SelectDestination();
        } while (transform.position.x == destination.x && transform.position.y == destination.y);
        Debug.Log("(" + position.x.ToString() + " ," + position.y.ToString() + ") に到着しました。次の目的地: (" + destination.x.ToString() + " ," + destination.y.ToString() + ")");
        this.Initialize(this.SearchPath(position, destination));
    }

    // 仮
    private Vector2 SelectDestination() {
        float x = Random.Range(0f, 16f);
        float y = Random.Range(0f, 8f);
        return new Vector2(x, y);
    }

    // 仮
    private List<List<Vector2>> SearchPath(Vector2 position, Vector2 destination) {
        List<List<Vector2>> path = new List<List<Vector2>>();
        path.Add(new List<Vector2>());
        path[0].Add(position);
        path[0].Add(destination);
        return path;
    }
}
