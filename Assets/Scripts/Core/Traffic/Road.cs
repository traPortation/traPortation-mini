using TraPortation.Const;
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

            this.view.SetLine(new Vector3[] { new Vector3(from.X, from.Y, Z.Road), new Vector3(to.X, to.Y, Z.Road) });
            this.view.SetColor(new Color(196 / 255f, 196 / 255f, 196 / 255f));
            this.view.SetWidth(Mathf.Sqrt(roadLength) * 0.1f);
        }

        public class Factory : PlaceholderFactory<IntersectionNode, IntersectionNode, float, Road> { }
    }
}