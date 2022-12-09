using TraPortation.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation.UI.Button
{
	// TODO: 他のボタンも置き換える
    public class ChangeStatusButton : MonoBehaviour, IPointerClickHandler
    {
        GameManager manager;
        [SerializeField] GameStatus changeTo;

        [Inject]
        public void Construct(GameManager manager)
        {
            this.manager = manager;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData e)
        {
            if (this.manager.Status == this.changeTo)
            {
                this.manager.SetStatus(GameStatus.Normal);
            }
            else
            {
                this.manager.SetStatus(this.changeTo);
            }
        }
    }
}