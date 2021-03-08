using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class Person : MovingObject
{
    // 移動の種類を持つ stringじゃなくてもいい
    private string[] pathType;
    

    // Start is called before the first frame update
    void Start()
    {
        velocity = 0.005f;
        float x = Random.Range(0f, 16f);
        float y = Random.Range(0f, 8f);
        transform.position = new Vector3(x, y, Z.Person);
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
        // Debug.Log($"({position.x.ToString()} ,{position.y.ToString()}) に到着しました。次の目的地: ({destination.x.ToString()} ,{destination.y.ToString()})");
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

    // 2点間を歩く経路を返す関数
    private List<Vector2> PathBetweenTwoPoints(Vector2 origin, Vector2 destination) {
        List<Vector2> path = new List<Vector2>();
        Vector2[] position_list = new Vector2[] {origin, origin, destination, destination};
        for (int i = 1; i < 3; i++) {
            if ((int)position_list[i].x != position_list[i].x && (int)position_list[i].y != position_list[i].y) {
                float right_side_road = Mathf.Ceil(position_list[i].x) - position_list[i].x;
                float up_side_road = Mathf.Ceil(position_list[i].y) - position_list[i].y;
                float left_side_road = 1f - right_side_road;
                float down_side_road = 1f - up_side_road;
                float min_road = Mathf.Min(down_side_road, Mathf.Min(left_side_road, Mathf.Min(up_side_road, right_side_road)));
                if (min_road == right_side_road) {
                    position_list[i].x = Mathf.Ceil(position_list[i].x);
                    continue;
                }
                if (min_road == up_side_road) {
                    position_list[i].y = Mathf.Ceil(position_list[i].y);
                    continue;     
                }
                if (min_road == left_side_road) {
                    position_list[i].x = Mathf.Floor(position_list[i].x);
                    continue;     
                }
                if (min_road == down_side_road) {
                    position_list[i].y = Mathf.Floor(position_list[i].y);
                    continue;     
                }
            }
        }
        Vector2 goban_first = position_list[1], goban_last = position_list[2];
        if (position_list[2].x - position_list[1].x >= 0) {
            if ((int)position_list[1].x != position_list[1].x) {
                goban_first.x = Mathf.Ceil(position_list[1].x);
            }
            if ((int)position_list[2].x != position_list[2].x) {
                goban_last.x = Mathf.Floor(position_list[2].x);
            }
        }
        else {
            if ((int)position_list[1].x != position_list[1].x) {
                goban_first.x = Mathf.Floor(position_list[1].x);
            }
            if ((int)position_list[2].x != position_list[2].x) {
                goban_last.x = Mathf.Ceil(position_list[2].x);
            }
        }
        if (position_list[2].y - position_list[1].y >= 0) {
            if ((int)position_list[1].y != position_list[1].y) {
                goban_first.y = Mathf.Ceil(position_list[1].y);
                Debug.Log(position_list[1].y);
            }
            if ((int)position_list[2].y != position_list[2].y) {
                goban_last.y = Mathf.Floor(position_list[2].y);
            }
        }
        else {
            if ((int)position_list[1].y != position_list[1].y) {
                goban_first.y = Mathf.Floor(position_list[1].y);
            }
            if ((int)position_list[2].y != position_list[2].y) {
                goban_last.y = Mathf.Ceil(position_list[2].y);
            }
        }
        int x_moving = (int)(goban_last.x - goban_first.x), y_moving = (int)(goban_last.y - goban_first.y);
        int n = Mathf.Abs(x_moving) + Mathf.Abs(y_moving) + 1;
        Vector2[] random_path = new Vector2[n];
        random_path[0] = goban_first;
        int ord = 1;
        int plus_minus_x = x_moving / Mathf.Abs(x_moving), plus_minus_y = y_moving / Mathf.Abs(y_moving);
        while (x_moving != 0 || y_moving != 0) {
            if (x_moving == 0) {
                while (y_moving != 0) {
                    random_path[ord].y = random_path[ord - 1].y + plus_minus_y;
                    random_path[ord].x = random_path[ord - 1].x;
                    ord++;
                    y_moving = y_moving - plus_minus_y;    
                }
                break;
            }
            if (y_moving == 0) { 
                while (x_moving != 0) {
                    random_path[ord].x = random_path[ord - 1].x + plus_minus_x;
                    random_path[ord].y = random_path[ord - 1].y;
                    ord++;
                    x_moving = x_moving - plus_minus_x;
                }
                break;
            }
            if (Random.Range(0, 2) == 0) {
                random_path[ord].x = random_path[ord - 1].x + plus_minus_x;
                random_path[ord].y = random_path[ord - 1].y;
                ord++;
                x_moving = x_moving - plus_minus_x;
                continue;
            }
            else {
                random_path[ord].y = random_path[ord - 1].y + plus_minus_y;
                random_path[ord].x = random_path[ord - 1].x;
                ord++;
                y_moving = y_moving - plus_minus_y;
                continue;
            }           
        }
        path.Add(origin);
        if (position_list[0] != position_list[1]) path.Add(position_list[1]);
        if (goban_first != origin) path.Add(goban_first);
        for (int i = 1; i < n; i++) path.Add(random_path[i]);
        if (goban_last != destination) path.Add(position_list[2]);
        if (position_list[2] != position_list[3]) path.Add(destination);
        return path;
    }
}
