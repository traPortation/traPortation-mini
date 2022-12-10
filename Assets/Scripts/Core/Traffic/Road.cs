using TraPortation.Traffic.Node;
using TraPortation.UI;
using UnityEngine;
using Zenject;

namespace TraPortation.Traffic
{
    public class Road
    {
        IRoadView view;
        Board board;

        [Inject]
        public Road(IntersectionNode from, IntersectionNode to, float roadLength, IRoadView view, Board board)
        {
            this.view = view;
            this.board = board;

            this.board.AddRoadEdge(from, to);
            this.board.AddRoadEdge(to, from);

            this.view.SetLine(new Vector3[] { new Vector3(from.X, from.Y, -1), new Vector3(to.X, to.Y, -1) });
            this.view.SetColor(Color.white);
            this.view.SetWidth(Mathf.Sqrt(roadLength) * 0.1f);
        }

        public class Factory : PlaceholderFactory<IntersectionNode, IntersectionNode, float, Road> { }
    }
}