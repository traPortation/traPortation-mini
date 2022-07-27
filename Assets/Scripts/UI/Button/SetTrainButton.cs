using TraPortation.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation.UI.Button
{
    public class SetTrainButton : MonoBehaviour, IPointerClickHandler
    {
        GameManager manager;

        [Inject]
        public void Construct(GameManager manager)
        {
            this.manager = manager;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData e)
        {
            this.manager.SetStatus(this.manager.Status switch {
                GameStatus.SetTrain => GameStatus.Normal,
                _ => GameStatus.SetTrain,
            });
        }
    }
}