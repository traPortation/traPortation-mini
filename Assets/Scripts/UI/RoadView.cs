using MessagePipe;
using TraPortation.Event;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation.UI
{
    public class RoadView : Line, IPointerClickHandler, IRoadView
    {
        IPublisher<ClickTarget, ClickedEvent> publisher;

        [Inject]
        public void Construct(IPublisher<ClickTarget, ClickedEvent> publisher)
        {
            this.publisher = publisher;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData e)
        {
            this.publisher.Publish(ClickTarget.Road, new ClickedEvent(e.pointerCurrentRaycast.worldPosition));
        }
    }
}