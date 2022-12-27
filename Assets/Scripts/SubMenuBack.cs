using System.Collections;
using System.Collections.Generic;
using TraPortation.Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TraPortation
{
    public class SubMenuBack : MonoBehaviour
    {
        GameManager manager;
        // Start is called before the first frame update
        [Inject]
        public void Construct(GameManager manager)
        {
            this.manager = manager;
        }
        public void back()
        {
            this.manager.SetStatus(GameStatus.Normal);
        }
        public void title()
        {
            SceneManager.LoadScene("Opening");
            this.manager.SetStatus(GameStatus.Normal);
        }

        public void restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Loading");
        }
    }
}