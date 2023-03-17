using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TraPortation.Game;

namespace TraPortation
{

    public class SettingsMenu : MonoBehaviour
    {
        GameManager manager;
        // Start is called before the first frame update
        [Inject]
        public void Construct(GameManager manager)
        {
            this.manager = manager;
        }
        // Update is called once per frame
        public void GoSettings()
        {
            this.manager.SetStatus(GameStatus.Settings);
        }
        public void GoSubMenu()
        {
            this.manager.SetStatus(GameStatus.SubMenu);
        }
    }


}