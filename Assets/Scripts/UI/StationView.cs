using MessagePipe;
using TraPortation.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation.UI
{
    public class StationView : MonoBehaviour, IPointerClickHandler
    {
        IPublisher<ClickTarget, ClickedEvent> publisher;
        Station station;

        [Inject]
        public void Construct(IPublisher<ClickTarget, ClickedEvent> publisher)
        {
            this.publisher = publisher;
        }

        public void SetStation(Station station)
        {
            this.station = station;
        }

        // NOTE: クリックしないでも発火してよさそう (スマホゲーなので)
        public void OnPointerClick(PointerEventData e)
        {
            this.publisher.Publish(ClickTarget.Station, new ClickedEvent(new Vector2(this.transform.position.x, this.transform.position.y)));
        }
    }
}