using System.Linq;
using TraPortation.Moving;
using TraPortation.Traffic;
using TraPortation.Traffic.Node;
using UnityEngine;
using Zenject;

namespace TraPortation
{

#nullable enable

    public class Person : MovingObject
    {
#nullable disable
        Board board;
        PersonPathFactory factory;
        SpriteRenderer spriteRenderer;
#nullable enable
        INode? goalNode;

        [Inject]
        public void Construct(Board board, PersonPathFactory factory)
        {
            this.board = board;
            this.factory = factory;

            var (path, goal) = this.getRandomPath();
            this.goalNode = goal;
            this.Initialize(path);
        }

        // Start is called before the first frame update
        void Start()
        {
            this.velocity = Const.Velocity.Person;
            this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            this.Move(this.velocity);

            if (this.path.Status == SectionStatus.Finished)
            {
                var (path, goal) = this.getRandomPath();
                this.goalNode = goal;
                this.Initialize(path);
            }

            if (this.path.Status == SectionStatus.OnTrain || this.path.Status == SectionStatus.OnBus)
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
        (PersonPath, INode) getRandomPath()
        {
            var start = this.goalNode ?? this.board.GetRandomPoint();
            var goal = this.board.GetRandomPoint();

            var nodes = this.board.GetPath(start, goal);
            return (this.factory.Create(nodes), goal);
        }
    }
}