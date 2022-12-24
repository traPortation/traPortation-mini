using System;
using System.Collections.Generic;
using System.Linq;
using MessagePipe;
using TraPortation.Event;
using TraPortation.Game;
using UnityEngine;
using Zenject;

namespace TraPortation.UI
{
    // TODO: RailManagerと合わせる
    public class LineManager : MonoBehaviour
    {
        GameManager manager;
        RailManager railManager;
        ILine mainLine;
        ILine currentLine;
        List<Vector3> positions = new List<Vector3>();
        List<Station> stations = new List<Station>();

        [Inject]
        public void Construct(GameManager manager, ILine mainLine, ILine currentLine, RailManager railManager)
        {
            this.manager = manager;
            this.railManager = railManager;
            this.mainLine = mainLine;
            this.currentLine = currentLine;

            this.mainLine.SetParent(this.transform);
            this.currentLine.SetParent(this.transform);

            this.mainLine.SetColor(Const.Color.SetRail);
            this.currentLine.SetColor(Const.Color.SetRail);
        }

        void Update()
        {
            if (this.manager.Status != GameStatus.SetRail)
            {
                if (this.positions.Count == 0)
                {
                    return;
                }

                this.railManager.AddRail(this.stations);

                this.positions = new List<Vector3>();
                this.stations = new List<Station>();
                this.mainLine.SetLine(Array.Empty<Vector3>());
                this.currentLine.SetLine(Array.Empty<Vector3>());
            }
            else
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Input.GetMouseButtonDown(0))
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    var mask = LayerMask.GetMask("Station");
                    var hitInfo = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, mask);

                    if (hitInfo.collider != null && hitInfo.collider.gameObject.name == "Station")
                    {
                        var view = hitInfo.collider.gameObject.GetComponent<StationView>();
                        var station = view.Station;

                        if (station is null)
                        {
                            return;
                        }
                        if (this.stations.Count != 0 && this.stations.Last() == station)
                        {
                            return;
                        }
                        this.positions.Add(new Vector3(station.Node.X, station.Node.Y, Const.Z.Rail));
                        this.stations.Add(station);
                        this.mainLine.SetLine(this.positions.ToArray());
                    }
                }

                if (this.positions.Count == 0)
                {
                    return;
                }

                this.currentLine.SetLine(new Vector3[2] { this.positions.Last(), new Vector3(mousePos.x, mousePos.y, Const.Z.Rail) });
            }
        }

    }
}