using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using System.Linq;

public class Person : MovingObject
{   
    // 移動の種類を持つ stringじゃなくてもいい
    public struct PathType 
    {
        private string howToMove;
        private int getOnId;
        private int getOffId;
        
        // 移動方法
        public string HowToMove
        {
            get {return this.howToMove;} 
            set {this.howToMove = value;}
        }
        // 乗り物に乗った所のid
        public int GetOnId
        {
            get 
            {
                if (this.howToMove == Const.Move.Walking) return -1; // 移動方法が徒歩ならidを-1にする
                else return this.getOnId;
            } 
            set {this.getOnId = value;}
        }
        // 乗り物から降りた時のid
        public int GetOffId
        {
            get 
            {
                if (this.howToMove == Const.Move.Walking) return -1; // 移動方法が徒歩ならidを-1にする
                else return this.getOffId;
            } 
            set {this.getOffId = value;}
        }
    }

    public PathType moving; 
    
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

    // 経路の端に着いたら ArriveDestination() を呼び出す
    // pathType[arriveIndex + 1] を見てあれこれしたりもする
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
        Vector2 getRoad(Vector2 position) { // ある点がそこから一番近い道にでるための関数、ある点が道上ならそのまま返す
            if ((int)position.x != position.x && (int)position.y != position.y) { // position が道以外に置かれている時
                float leftSideRoad = position.x % 1;
                float downSideRoad = position.y % 1;
                float rightSideRoad = 1f - leftSideRoad;
                float upSideRoad = 1f - downSideRoad;
                float minRoad = new float[] {leftSideRoad, downSideRoad, rightSideRoad, upSideRoad}.Min();
                if (minRoad == rightSideRoad) {
                    position.x = Mathf.Ceil(position.x);
                    return position;
                }
                else if (minRoad == upSideRoad) {
                    position.y = Mathf.Ceil(position.y);
                    return position;     
                }
                else if (minRoad == leftSideRoad) {
                    position.x = Mathf.Floor(position.x);
                    return position;     
                }
                else {
                    position.y = Mathf.Floor(position.y);
                    return position;     
                }
            }
            return position;
        }

        Vector2 GetIntersection(Vector2 position) { // ある点がそこから一番近い交差点に出るための関数
            if ((int)position.x != position.x) {
                position.x = Mathf.Round(position.x);
            }
            if ((int)position.y != position.y) {
                position.y = Mathf.Round(position.y);
            }
            return position;
        }
        
        Vector2 originToRoad = getRoad(origin), destinationToRoad = getRoad(destination); // origin, destination から道に出た時のそれぞれの座標
        
        // 碁盤の目状でランダムに進むための準備
        Vector2 gobanFirst = GetIntersection(originToRoad), gobanLast = GetIntersection(destinationToRoad);
        int xMoving = (int)(gobanLast.x - gobanFirst.x), yMoving = (int)(gobanLast.y - gobanFirst.y); // gobanFirst から gobanLast まで x 方向、y 方向にいくら進むか。
        int n = Mathf.Abs(xMoving) + Mathf.Abs(yMoving) + 1; // 移動量
        Vector2[] randomPath = new Vector2[n];
        randomPath[0] = gobanFirst;
        int ord = 1; // 現在の移動量
        int plusOrMinusX = xMoving / Mathf.Abs(xMoving), plusOrMinusY = yMoving / Mathf.Abs(yMoving); // gobanFirst から gobanLast までの各軸の移動の方向を表す
        
        // randomPath の生成
        while (xMoving != 0 || yMoving != 0) {
            if (xMoving != 0 && yMoving != 0) { // x 軸方向とy 軸方向共に移動可能か
                int rnd = Random.Range(0, 2); // 0 なら x 軸方向へ、1 なら y 軸方向へ
                if (rnd == 0) {
                    randomPath[ord].x = randomPath[ord - 1].x + plusOrMinusX;
                    randomPath[ord].y = randomPath[ord - 1].y;
                    xMoving = xMoving - plusOrMinusX;
                }
                else {
                    randomPath[ord].y = randomPath[ord - 1].y + plusOrMinusY;
                    randomPath[ord].x = randomPath[ord - 1].x;
                    yMoving = yMoving - plusOrMinusY;
                }
            }
            else {
                if (xMoving == 0) { // ずっと y 軸方向へ
                    randomPath[ord].y = randomPath[ord - 1].y + plusOrMinusY;
                    randomPath[ord].x = randomPath[ord - 1].x;
                    yMoving = yMoving - plusOrMinusY;
                }
                else if (yMoving == 0) { // ずっと x 軸方向へ
                    randomPath[ord].x = randomPath[ord - 1].x + plusOrMinusX;
                    randomPath[ord].y = randomPath[ord - 1].y;
                    xMoving = xMoving - plusOrMinusX;
                }
            }
            ord++;           
        }

        // 返り値 path の生成
        List<Vector2> path = new List<Vector2>(); // 返り値
        path.Add(origin);
        if (origin != originToRoad) path.Add(originToRoad); // origin が道の上でないなら
        if (gobanFirst != origin) path.Add(gobanFirst); // origin が交差点上でないなら
        for (int i = 1; i < n; i++) path.Add(randomPath[i]);
        if (gobanLast != destination) path.Add(destinationToRoad); // destination が交差点上でないなら
        if (destinationToRoad != destination) path.Add(destination); // destination が道の上でないなら
        return path;
    }
}
