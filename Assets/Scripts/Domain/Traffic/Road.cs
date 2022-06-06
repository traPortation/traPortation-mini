using TraPortation.Traffic.Node;
using UI;
using UnityEngine;
using Zenject;

namespace TraPortation.Traffic
{
    public class Road
    {
        ILine line;
        Board board;

        [Inject]
        public Road(IntersectionNode from, IntersectionNode to, ILine line, Board board)
        {
            this.line = line;
            this.board = board;

            this.board.AddRoadEdge(from, to);
            this.board.AddRoadEdge(to, from);

            this.line.SetLine(new Vector3[] { new Vector3(from.X, from.Y, -1), new Vector3(to.X, to.Y, -1) });
            this.line.SetColor(Color.black);
        }

        public class Factory : PlaceholderFactory<IntersectionNode, IntersectionNode, Road> { }
    }
}