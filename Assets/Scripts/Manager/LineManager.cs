using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using MessagePipe;
using Event;

namespace UI
{
    public class LineManager : MonoBehaviour
    {
        ILine mainLine;
        ILine currentLine;
        List<Vector3> positions = new List<Vector3>();
        ISubscriber<StationClickedEvent> stationSubscriber;

        [Inject]
        public void Construct(ILine mainLine, ILine currentLine, ISubscriber<StationClickedEvent> stationSubscriber)
        {
            this.mainLine = mainLine;
            this.currentLine = currentLine;
            this.stationSubscriber = stationSubscriber;

            this.mainLine.SetColor(Color.blue);
            this.currentLine.SetColor(Color.blue);

            this.stationSubscriber.Subscribe(e =>
            {
                Debug.Log("subscribed");
                Debug.Log(e.Position.x);
                this.positions.Add(new Vector3(e.Position.x, e.Position.y, 9));
                this.mainLine.SetLine(this.positions.ToArray());
            });
        }

        void Update()
        {
            if (this.positions.Count != 0)
            {
				var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                this.currentLine.SetLine(new Vector3[2] { this.positions.Last(), new Vector3(mousePos.x, mousePos.y, 9) });
            }
        }

    }
}