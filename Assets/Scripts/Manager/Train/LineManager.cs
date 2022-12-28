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
        InputManager inputManager;
        IPublisher<CreatedEvent> publisher;
        ILine mainLine;
        ILine currentLine;
        List<Vector3> positions = new List<Vector3>();
        List<Station> stations = new List<Station>();

        [Inject]
        public void Construct(GameManager manager, InputManager inputManager,
            ILine mainLine, ILine currentLine, RailManager railManager, IPublisher<CreatedEvent> publisher)
        {
            this.manager = manager;
            this.inputManager = inputManager;
            this.railManager = railManager;
            this.publisher = publisher;
            this.mainLine = mainLine;
            this.currentLine = currentLine;


            this.mainLine.SetParent(this.transform);
            this.currentLine.SetParent(this.transform);

            var color = this.railManager.NextColor;
            color.a = 0.5f;

            this.mainLine.SetColor(color);
            this.currentLine.SetColor(color);
        }

        void Update()
        {
            if (this.manager.Status != GameStatus.SetRail)
            {
                if (this.positions.Count == 0)
                {
                    return;
                }

                this.positions = new List<Vector3>();
                this.stations = new List<Station>();
                this.mainLine.SetLine(Array.Empty<Vector3>());
                this.currentLine.SetLine(Array.Empty<Vector3>());
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    var obj = this.inputManager.RayCast(LayerMask.GetMask("Station"));

                    if (obj != null && obj.name == "Station")
                    {
                        var view = obj.GetComponent<StationView>();
                        var station = view.Station;

                        if (station is null)
                        {
                            return;
                        }
                        if (this.stations.Count != 0 && this.stations.Last() == station)
                        {
                            this.railManager.AddRail(this.stations);
                            this.publisher.Publish(new CreatedEvent(CreateType.Rail));

                            this.positions = new List<Vector3>();
                            this.stations = new List<Station>();
                            this.mainLine.SetLine(Array.Empty<Vector3>());
                            this.currentLine.SetLine(Array.Empty<Vector3>());
                            var color = this.railManager.NextColor;
                            color.a = 0.5f;
                            this.mainLine.SetColor(color);
                            this.currentLine.SetColor(color);

                            this.manager.SetStatus(GameStatus.Normal);
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

                var mousePos = this.inputManager.GetMousePosition();
                this.currentLine.SetLine(new Vector3[2] { this.positions.Last(), new Vector3(mousePos.x, mousePos.y, Const.Z.Rail) });
            }
        }

    }
}