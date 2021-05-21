using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using System.Linq;
using BoardElements;
public class Person : MovingObject
{  
    private GameManager manager; 
    // Start is called before the first frame update
    void Start()
    {
        this.manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (this.manager == null) throw new System.Exception("GameManager not found");
        this.setRandomPath();
        this.Initialize(path);
        if (this.path == null) throw new System.Exception("path not found");
        this.velocity = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        this.Move(this.velocity);
    }

    protected override void Arrive(BoardElements.Node vertex){
        if (this.path.Finished) {
            this.setRandomPath();
            this.Initialize(path);
        }
    }
    private void setRandomPath() {
        var start = new Node(transform.position.x, transform.position.y);
        var goal = new Node(Random.Range(X.Min, X.Max), Random.Range(Y.Min, Y.Max));
        this.path = manager.Board.GetPath(start, goal);
    }
}
