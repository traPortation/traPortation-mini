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
        ISubscriber<StationClickedEvent> stationSubscriber;

        [Inject]
        public void Construct(GameManager manager, ILine mainLine, ILine currentLine, RailManager railManager, ISubscriber<StationClickedEvent> stationSubscriber)
        {
            this.manager = manager;
            this.railManager = railManager;
            this.mainLine = mainLine;
            this.currentLine = currentLine;
            this.stationSubscriber = stationSubscriber;

            this.mainLine.SetColor(Color.blue);
            this.currentLine.SetColor(Color.blue);

            this.stationSubscriber.Subscribe(e =>
            {
                if (this.manager.Status == GameStatus.SetRail)
                {
                    if (e.Station is null) {
                        return;
                    }
                    this.positions.Add(new Vector3(e.Position.x, e.Position.y, 9));
                    this.stations.Add(e.Station);
                    this.mainLine.SetLine(this.positions.ToArray());
                }
            });
        }

        void Update()
        {
            if (this.positions.Count == 0)
            {
                return;
            }

            if (this.manager.Status != GameStatus.SetRail)
            {
                this.railManager.AddRail(this.stations);

                this.positions = new List<Vector3>();
                this.mainLine.SetLine(Array.Empty<Vector3>());
                this.currentLine.SetLine(Array.Empty<Vector3>());
            }
            else
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                this.currentLine.SetLine(new Vector3[2] { this.positions.Last(), new Vector3(mousePos.x, mousePos.y, 9) });
            }
        }

    }
}