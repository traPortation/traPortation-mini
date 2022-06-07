using System.Collections.Generic;
using System.Linq;
using MessagePipe;
using TraPortation.Event;
using TraPortation.Game;
using UnityEngine;
using Zenject;

namespace TraPortation.UI
{
    public class LineManager : MonoBehaviour
    {
        GameManager manager;
        ILine mainLine;
        ILine currentLine;
        List<Vector3> positions = new List<Vector3>();
        ISubscriber<ClickTarget, ClickedEvent> stationSubscriber;

        [Inject]
        public void Construct(GameManager manager, ILine mainLine, ILine currentLine, ISubscriber<ClickTarget, ClickedEvent> stationSubscriber)
        {
            this.manager = manager;
            this.mainLine = mainLine;
            this.currentLine = currentLine;
            this.stationSubscriber = stationSubscriber;

            this.mainLine.SetColor(Color.blue);
            this.currentLine.SetColor(Color.blue);

            this.stationSubscriber.Subscribe(ClickTarget.Station, e =>
            {
                if (this.manager.Status != GameStatus.SetRail)
                {
                    return;
                }

                Debug.Log("subscribed");
                Debug.Log(e.Position.x);
                this.positions.Add(new Vector3(e.Position.x, e.Position.y, 9));
                this.mainLine.SetLine(this.positions.ToArray());
            });
        }

        void Update()
        {
            if (this.manager.Status != GameStatus.SetRail)
            {
                return;
            }
            if (this.positions.Count != 0)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                this.currentLine.SetLine(new Vector3[2] { this.positions.Last(), new Vector3(mousePos.x, mousePos.y, 9) });
            }
        }

    }
}