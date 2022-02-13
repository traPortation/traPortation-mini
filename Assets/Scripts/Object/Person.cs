using System.Linq;
using UnityEngine;
using Zenject;
using Const;
using Traffic;
using Traffic.Node;
using Moving;

#nullable enable

public class Person : MovingObject
{
#nullable disable
    Board board;
    PathFactory factory;
    SpriteRenderer spriteRenderer;
#nullable enable
    // Start is called before the first frame update
    void Start()
    {
        this.velocity = Velocity.Person;
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.Move(this.velocity);

        if (this.path.Status == SectionStatus.Finished)
        {
            var path = this.getRandomPath();
            this.Initialize(path);
        }

        if (this.path.Status == SectionStatus.OnTrain)
        {
            this.spriteRenderer.enabled = false;
        }
        else
        {
            this.spriteRenderer.enabled = true;
        }
    }

    [Inject]
    void construct(Board board, PathFactory factory)
    {
        this.board = board;
        this.factory = factory;

        var path = this.getRandomPath();
        this.Initialize(path);
    }

    /// <summary>
    /// ランダムにゴールを設定し、そこまでの経路をセットする
    /// </summary>
    private Path getRandomPath()
    {
        var start = this.path != null && this.path.LastNode is IIndexedNode iNode ? iNode : this.board.Nodes[UnityEngine.Random.Range(0, this.board.Nodes.Count)];

        // 始点と終点が被らないようにするための処理
        IBoardNode goal;
        do
        {
            goal = this.board.Nodes[UnityEngine.Random.Range(0, this.board.Nodes.Count)];
        } while (start.Index == goal.Index);

        var nodes = this.board.GetPath(start, goal);

        return this.factory.Create(nodes);
    }
}
