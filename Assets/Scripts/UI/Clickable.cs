using MessagePipe;
using TraPortation.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UI
{
    // TODO: 名前を変える
    public class Clickable : MonoBehaviour, IPointerClickHandler
    {
        IPublisher<ClickTarget, ClickedEvent> publisher;

        [Inject]
        public void Construct(IPublisher<ClickTarget, ClickedEvent> publisher)
        {
            this.publisher = publisher;
        }

        // NOTE: クリックしないでも発火してよさそう (スマホゲーなので)
        public void OnPointerClick(PointerEventData e)
        {
            this.publisher.Publish(ClickTarget.Station, new ClickedEvent(e.pointerCurrentRaycast.worldPosition));
        }
    }
}