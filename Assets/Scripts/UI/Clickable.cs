using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using MessagePipe;
using Event;

namespace UI
{
	// TODO: 名前を変える
    public class Clickable : MonoBehaviour, IPointerClickHandler
    {
        IPublisher<StationClickedEvent> publisher;

        [Inject]
        public void Construct(IPublisher<StationClickedEvent> publisher) { 
			this.publisher = publisher;
		}

		// NOTE: クリックしないでも発火してよさそう (スマホゲーなので)
        public void OnPointerClick(PointerEventData e) {
			this.publisher.Publish(new StationClickedEvent(e.pointerCurrentRaycast.worldPosition));
		}
    }
}