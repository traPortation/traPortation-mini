using System.Collections;
using System.Collections.Generic;
using TraPortation.Game;
using UnityEngine;
using Zenject;

namespace TraPortation
{
    public class SubMenuBar : MonoBehaviour
    {
        [SerializeField] GameObject SubMenuPanel;
        GameManager manager;
        // Start is called before the first frame update
        [Inject]
        public void Construct(GameManager manager)
        {
            this.manager = manager;
        }

        // Update is called once per frame
        void Update()
        {
            if (manager.Status == GameStatus.SubMenu) {
                SubMenuPanel.SetActive(true);
            }
            else {
                SubMenuPanel.SetActive(false);
            }
        }
    }
}