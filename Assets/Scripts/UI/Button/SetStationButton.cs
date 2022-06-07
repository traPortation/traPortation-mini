using TraPortation.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation.UI.Button
{
    public class SetStationButton : MonoBehaviour, IPointerClickHandler
    {
        GameManager manager;
        StationManager sManager;

        [Inject]
        public void Construct(GameManager manager, StationManager sManager)
        {
            this.manager = manager;
			this.sManager = sManager;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData e)
        {
            this.sManager.SetBuildMode();
        }
    }
}