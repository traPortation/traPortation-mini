using System.Collections.Generic;
using System.Linq;
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
        InputManager inputManager;
        Board board;
        List<IBoardNode> nodes = new List<IBoardNode>();
        ILine line;
        ILine curLine;
        BusRail.Factory factory;
        List<BusRail> rails = new List<BusRail>();

        [Inject]
        public void Construct(GameManager manager, InputManager inputManager, Board board, ILine line, ILine curLine, BusRail.Factory factory)
        {
            this.manager = manager;
            this.inputManager = inputManager;
            this.board = board;
            this.line = line;
            this.line.SetColor(Const.Color.SetBusRail);
            this.curLine = curLine;
            this.curLine.SetColor(Const.Color.SetBusRail);
            this.factory = factory;

            this.line.SetParent(this.transform);
            this.curLine.SetParent(this.transform);
        }

        void Update()
        {
            if (this.manager.Status != GameStatus.SetBusRail)
            {
                if (this.nodes.Count != 0)
                {
                    this.nodes.Clear();
                    this.line.SetLine(this.nodes.Select(n => new Vector3(n.X, n.Y, Const.Z.BusRail)).ToArray());
                    this.curLine.SetLine(new Vector3[] { });
                }

                return;
            }

            if (this.nodes.Count != 0)
            {
                var mousePos = this.inputManager.GetMousePosition();
                this.curLine.SetLine(new Vector3[] { new Vector3(this.nodes.Last().X, this.nodes.Last().Y, Const.Z.BusRail), new Vector3(mousePos.x, mousePos.y, Const.Z.BusRail) });
            }

            if (Input.GetMouseButtonDown(0))
            {
                var obj = this.inputManager.RayCast(LayerMask.GetMask("BusStation"));

                if (obj != null && obj.name == "BusStation")
                {
                    var busStation = obj.GetComponent<BusStationView>().BusStation;

                    if (this.nodes.Count == 0)
                    {
                        this.nodes.Add(busStation.Node);
                        return;
                    }
                    else if (this.nodes.Last() == busStation.Node)
                    {
                        var rail = this.factory.Create(this.rails.Count, this.nodes);
                        this.rails.Add(rail);

                        this.nodes.Clear();
                        this.line.SetLine(this.nodes.Select(n => new Vector3(n.X, n.Y, Const.Z.BusRail)).ToArray());
                        this.curLine.SetLine(new Vector3[] { });
                        this.manager.SetStatus(GameStatus.Normal);
                        return;
                    }
                    else
                    {
                        var nodes = this.board.SearchRoad(this.nodes.Last(), busStation.Node);
                        this.nodes = this.nodes.Concat(nodes).ToList();

                        this.line.SetLine(this.nodes.Select(n => new Vector3(n.X, n.Y, Const.Z.BusRail)).ToArray());
                    }
                }
            }
        }
    }
}