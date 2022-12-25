using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TraPortation.Game;

namespace TraPortation
{

    public class SubMenu : MonoBehaviour
    {
        GameManager manager;
        // Start is called before the first frame update
        [Inject]
        public void Construct(GameManager manager)
        {
            this.manager = manager;
        }
        // Update is called once per frame
        public void Pause()
        {
            this.manager.SetStatus(GameStatus.SubMenu);
        }
    }


}