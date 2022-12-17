using System.Collections.Generic;
using System.Linq;
using MessagePipe;
using TraPortation.Const;
using TraPortation.Event;
using TraPortation.Game;
using TraPortation.Traffic;
using TraPortation.Traffic.Edge;
using TraPortation.Traffic.Node;
using TraPortation.UI;
using UnityEngine;
using Zenject;

namespace TraPortation
{
    public class SetBusRailManager : MonoBehaviour
    {
        GameManager manager;
        List<IBoardNode> nodes = new List<IBoardNode>();
        ISubscriber<BusStationClickedEvent> subscriber;
        ILine line;
        ILine curLine;

        [Inject]
        public void Construct(GameManager manager, ISubscriber<BusStationClickedEvent> subscriber, ILine line, ILine curLine, BusRail.Factory factory)
        {
            this.manager = manager;
            this.subscriber = subscriber;
            this.line = line;
            this.line.SetColor(Color.red);
            this.curLine = curLine;
            this.curLine.SetColor(Color.red);

            this.subscriber.Subscribe(e =>
            {
                if (this.manager.Status != GameStatus.SetBusRail)
                {
                    return;
                }

                if (this.nodes.Count == 0)
                {
                    this.nodes.Add(e.BusStation.Node);
                    return;
                }
                else
                {
                    if (this.nodes.Last() != e.BusStation.Node)
                    {
                        return;
                    }
                    else
                    {
                        factory.Create(this.nodes);
                        this.manager.SetStatus(GameStatus.Normal);
                    }
                }
            });
        }

        void Update()
        {
            if (this.manager.Status != GameStatus.SetBusRail)
            {
                if (this.nodes.Count != 0)
                {
                    this.nodes.Clear();
                    this.line.SetLine(this.nodes.Select(n => new Vector3(n.X, n.Y, Z.BusRail)).ToArray());
                    this.curLine.SetLine(new Vector3[] { });
                }

                return;
            }

            if (this.nodes.Count == 0) return;

            var mousePos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var mousePos = new Vector2(mousePos3D.x, mousePos3D.y);

            this.curLine.SetLine(new Vector3[] { new Vector3(this.nodes.Last().X, this.nodes.Last().Y, Z.BusRail), new Vector3(mousePos.x, mousePos.y, Z.BusRail) });

            // 解除
            if (nodes.Count > 1)
            {
                var secondToLast = new Vector2(nodes[nodes.Count - 2].X, nodes[nodes.Count - 2].Y);

                if (Vector2.Distance(mousePos, secondToLast) < 0.1f)
                {
                    nodes.RemoveAt(nodes.Count - 1);
                    this.line.SetLine(this.nodes.Select(n => new Vector3(n.X, n.Y, Z.BusRail)).ToArray());
                }
            }

            // 繋げられる点のうち最も近い点に繋げる
            foreach (var e in nodes.Last().Edges)
            {
                if (e is RoadEdge r)
                {
                    if (nodes.Count >= 2 && e.To == nodes[nodes.Count - 2]) continue;

                    var from = new Vector2(r.From.X, r.From.Y);
                    var to = new Vector2(r.To.X, r.To.Y);

                    var cos = Vector2.Dot(mousePos - to, from - to) / (Vector2.Distance(mousePos, to) * Vector2.Distance(from, to));
                    if (cos < -0.9f)
                    {
                        nodes.Add(r.To);
                        this.line.SetLine(this.nodes.Select(n => new Vector3(n.X, n.Y, Z.BusRail)).ToArray());
                        break;
                    }
                }
            }
        }
    }
}