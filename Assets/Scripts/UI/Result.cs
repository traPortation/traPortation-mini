using System;
using TMPro;
using TraPortation.Game;
using UnityEngine;
using Zenject;

namespace TraPortation.UI
{
    public class Result : MonoBehaviour
    {
        [SerializeField] GameObject obj;
        [SerializeField] TextMeshProUGUI peopleCount;
        [SerializeField] TextMeshProUGUI peopleRatio;
        [SerializeField] TextMeshProUGUI peopleTotal;
        [SerializeField] TextMeshProUGUI moneyCount;
        [SerializeField] TextMeshProUGUI moneyRatio;
        [SerializeField] TextMeshProUGUI moneyTotal;
        [SerializeField] TextMeshProUGUI scoreTotal;
        GameManager manager;

        [Inject]
        public void Construct(GameManager manager)
        {
            this.manager = manager;
        }

        void Start()
        {

        }

        void Update()
        {
            if (this.manager.Status != GameStatus.Result) return;

            this.obj.SetActive(true);

            int peopleScore = ((int)(Const.General.PersonCount * Const.General.PeopleScoreRatio));
            int moneyScore = (int)(this.manager.ManageMoney.money * Const.General.MoneyScoreRatio);

            this.peopleCount.text = Const.General.PersonCount.ToString("#,0");
            this.peopleRatio.text = "×" + Const.General.PeopleScoreRatio.ToString();
            this.peopleTotal.text = peopleScore.ToString("#,0");

            this.moneyCount.text = this.manager.ManageMoney.money.ToString("#,0");
            this.moneyRatio.text = "×" + Const.General.MoneyScoreRatio.ToString();
            this.moneyTotal.text = moneyScore.ToString("#,0");
            this.scoreTotal.text = (peopleScore + moneyScore).ToString("#,0");
        }
    }
}