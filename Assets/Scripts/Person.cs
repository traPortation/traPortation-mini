using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using System.Linq;
using BoardElements;
public class Person : MovingObject
<<<<<<< HEAD
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
    
=======
{
    private GameManager manager;
>>>>>>> master
    // Start is called before the first frame update
    void Start()
    {
        this.manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (this.manager == null) throw new System.Exception("GameManager not found");
        var path = this.getRandomPath();
        this.Initialize(path);
        if (this.path == null) throw new System.Exception("path not found");
        this.velocity = 0.01f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.Move(this.velocity);
    }

    protected override void Arrive(BoardElements.Node vertex)
    {
        if (this.path.Finished)
        {
            var path = this.getRandomPath();
            this.Initialize(path);
        }
    }
    /// <summary>
    /// ランダムにゴールを設定し、そこまでの経路をセットする
    /// </summary>
    private Path getRandomPath()
    {
        var start = new Node(transform.position.x, transform.position.y);
        var goal = new Node(Random.Range(X.Min, X.Max), Random.Range(Y.Min, Y.Max));
        return this.manager.Board.GetPath(start, goal);
    }
}
