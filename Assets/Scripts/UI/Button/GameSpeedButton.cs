using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace TraPortation.UI
{
    public class GameSpeedButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Sprite[] buttons = new Sprite[3];
        [SerializeField] int[] speeds = new int[] { 1, 2, 4 };
        GameManager manager;
		Image image;
        int index = 0;

        [Inject]
        public void Construct(GameManager manager)
        {
            this.manager = manager;
        }

        void Start()
        {
            this.image = this.gameObject.GetComponent<Image>();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData e)
        {
            index++;
            if (index >= this.speeds.Length)
            {
                index = 0;
            }
            this.manager.GameSpeed = this.speeds[index];
            this.image.sprite = this.buttons[index];
        }
    }
}