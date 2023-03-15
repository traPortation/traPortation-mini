using System.Collections;
using System.Collections.Generic;
using TraPortation.Game;
using UnityEngine;
using Zenject;

namespace TraPortation
{
    public class SettingsMenuBar : MonoBehaviour
    {
        [SerializeField] GameObject SettingsPanel;
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
            if (manager.Status == GameStatus.Settings) {
                SettingsPanel.SetActive(true);
            }
            else {
                SettingsPanel.SetActive(false);
            }
        }
    }
}