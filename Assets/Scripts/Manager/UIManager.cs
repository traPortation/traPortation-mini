using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TraPortation.Game;
using UnityEngine;
using UnityEngine.UI;

namespace TraPortation.UI
{
    public class UIManager : MonoBehaviour
    {
        private GameManager manager;
        private float timeLimit = Const.General.TimeLimitSeconds;
        private int score = 0;
        private int money;
        public TextMeshProUGUI timeLimitText;
        public TextMeshProUGUI moneyText;
        public TextMeshProUGUI populationText;
        public Text scoreText;
        public Text statusText;
        // Start is called before the first frame update
        void Start()
        {
            this.manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            Utils.NullChecker.Check(this.manager);
        }

        // Update is called once per frame
        void Update()
        {
            money = manager.ManageMoney.money;
            timeLimit -= Time.deltaTime;

            timeLimitText.text = string.Format("{0:#.#} 秒", timeLimit);
            moneyText.text = string.Format("{0} 円", money);
            scoreText.text = string.Format("{0} 点", score);
            populationText.text = Const.General.PersonCount.ToString();
            statusText.text = manager.Status switch
            {
                GameStatus.Normal => "Normal",
                GameStatus.Pause => "Pause",
                GameStatus.SubMenu => "SubMenu",
                GameStatus.SetTrain => "SetTrain",
                GameStatus.SetRail => "SetRail",
                GameStatus.SetStation => "SetStation",
                GameStatus.SetBusStation => "SetBusStation",
                GameStatus.SetBusRail => "SetBusRail",
                GameStatus.SetBus => "SetBus",
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
