using TraPortation.Game;
using UnityEngine;
using Zenject;

namespace TraPortation
{
    public class SetTrainManager : MonoBehaviour
    {
        GameManager gameManager;
		[SerializeField] GameObject trainIcon;

        [Inject]
        public void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        void Update()
        {
			if (this.gameManager.Status != GameStatus.SetTrain)
			{
				return;
			}


        }
    }
}