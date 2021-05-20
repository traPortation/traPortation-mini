using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using System.Linq;

public class Person : MovingObject
{  
    private GameManager manager; 
    // Start is called before the first frame update
    void Start()
    {
        this.manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (this.manager == null) throw new System.Exception("GameManager not found");

        var path = this.manager.board.GetRandomPath(this.manager.board.GetRandomVertex());
        if (path == null) throw new System.Exception("path is null");
        this.Initialize(path);
        this.velocity = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.path == null) throw new System.Exception("path not found");
        this.Move(this.velocity);
    }

    protected override void Arrive(BoardElements.Vertex vertex){
        if (this.path.Finished) {
            var path = this.manager.board.GetRandomPath(vertex);
            this.Initialize(path);
        }
    }
}
