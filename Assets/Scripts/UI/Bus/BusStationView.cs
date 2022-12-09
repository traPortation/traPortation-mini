using MessagePipe;
using TraPortation.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation.UI
{
    public class BusStationView : MonoBehaviour, IPointerClickHandler
    {
        IPublisher<BusStationClickedEvent> publisher;
        BusStation busStation;

        [Inject]
        public void Construct(IPublisher<BusStationClickedEvent> publisher)
        {
            this.publisher = publisher;
        }

        public void SetBusStation(BusStation busStation)
        {
            this.busStation = busStation;
        }

        public void OnPointerClick(PointerEventData e)
        {
            this.publisher.Publish(new BusStationClickedEvent(new Vector2(this.transform.position.x, this.transform.position.y), this.busStation));
        }
    }
}