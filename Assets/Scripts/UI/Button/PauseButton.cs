using TraPortation.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace TraPortation.UI.Button
{
    public class PauseButton : MonoBehaviour, IPointerClickHandler
    {
        GameManager manager;
        [SerializeField] Sprite[] pauseSprite = new Sprite[2];
        Image image;

        [Inject]
        public void Construct(GameManager manager)
        {
            this.manager = manager;
            this.image = this.gameObject.GetComponent<Image>();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData e)
        {

            if (this.manager.Status == GameStatus.Pause)
            {
                this.manager.SetStatus(GameStatus.Normal);
                this.image.sprite = this.pauseSprite[0];
            }
            else
            {
                this.manager.SetStatus(GameStatus.Pause);
                this.image.sprite = this.pauseSprite[1];
            }
        }
    }
}