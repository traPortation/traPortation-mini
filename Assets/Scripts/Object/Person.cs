using System.Linq;
using UnityEngine;
using Zenject;
using Const;
using Traffic;
using Moving;

#nullable enable

public class Person : MovingObject
{
#nullable disable
    Board board;
    PathFactory factory;
    SpriteRenderer spriteRenderer;
#nullable enable
    [Inject]
    public void Construct(Board board, PathFactory factory)
    {
        this.board = board;
        this.factory = factory;

        var path = this.getRandomPath();
        this.Initialize(path);
    }

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

    /// <summary>
    /// ランダムにゴールを設定し、そこまでの経路をセットする
    /// </summary>
    Path getRandomPath()
    {
        var start = this.path?.LastNode ?? this.board.GetRandomPoint();
        var goal = this.board.GetRandomPoint();

        var nodes = this.board.GetPath(start, goal);
        return this.factory.Create(nodes);
    }
}
