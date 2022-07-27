using MessagePipe;
using TraPortation.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation.UI
{
    public class StationView : MonoBehaviour, IPointerClickHandler
    {
        IPublisher<StationClickedEvent> publisher;
        Station station;

        [Inject]
        public void Construct(IPublisher<StationClickedEvent> publisher)
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
            this.publisher.Publish(new StationClickedEvent(new Vector2(this.transform.position.x, this.transform.position.y), this.station));
        }
    }
}