using MessagePipe;
using TraPortation.Event;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation.UI
{
    public class RoadView : Line, IPointerClickHandler, IRoadView
    {
        IPublisher<RoadClickedEvent> publisher;

        [Inject]
        public void Construct(IPublisher<RoadClickedEvent> publisher)
        {
            this.publisher = publisher;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData e)
        {
            this.publisher.Publish(new RoadClickedEvent(e.pointerCurrentRaycast.worldPosition));
        }
    }
}