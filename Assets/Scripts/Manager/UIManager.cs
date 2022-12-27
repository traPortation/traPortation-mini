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
        GameStatus currentStatus;
        private float timeLimit = Const.General.TimeLimitSeconds;
        private int score = 0;
        private int money;
        public TextMeshProUGUI timeLimitText;
        public TextMeshProUGUI moneyText;
        public TextMeshProUGUI populationText;
        public Text scoreText;
        public Text statusText;

        public Image trainImage;
        public Image busImage;
        public Image stationImage;
        public Image vehicleImage;
        public Image routeImage;
        // Start is called before the first frame update
        void Start()
        {
            this.manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            Utils.NullChecker.Check(this.manager);
            this.currentStatus = this.manager.Status;

            this.setVehicleImage(Vehicle.None);
            this.setVerbImage(Verb.None);
        }

        // Update is called once per frame
        void Update()
        {
            money = manager.ManageMoney.money;
            timeLimit -= Time.deltaTime;

            timeLimitText.text = string.Format("{0} 秒", ((int)timeLimit));
            moneyText.text = string.Format("¥{0}", money);
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

            if (this.currentStatus != this.manager.Status)
            {
                this.currentStatus = this.manager.Status;
                switch (this.manager.Status)
                {
                    case GameStatus.SetTrain:
                    case GameStatus.SetRail:
                    case GameStatus.SetStation:
                        this.setVehicleImage(Vehicle.Train);
                        break;
                    case GameStatus.SetBusStation:
                    case GameStatus.SetBusRail:
                    case GameStatus.SetBus:
                        this.setVehicleImage(Vehicle.Bus);
                        break;
                    default:
                        this.setVehicleImage(Vehicle.None);
                        break;
                }

                switch (this.manager.Status)
                {
                    case GameStatus.SetRail:
                    case GameStatus.SetBusRail:
                        this.setVerbImage(Verb.Route);
                        break;
                    case GameStatus.SetStation:
                    case GameStatus.SetBusStation:
                        this.setVerbImage(Verb.Station);
                        break;
                    case GameStatus.SetTrain:
                    case GameStatus.SetBus:
                        this.setVerbImage(Verb.Vehicle);
                        break;
                    default:
                        this.setVerbImage(Verb.None);
                        break;
                }
            }
        }

        enum Vehicle
        {
            Train,
            Bus,
            None
        }
        void setVehicleImage(Vehicle v)
        {
            switch (v)
            {
                case Vehicle.Train:
                    if (!this.trainImage.enabled)
                        this.trainImage.enabled = true;
                    if (this.busImage.enabled)
                        this.busImage.enabled = false;
                    break;
                case Vehicle.Bus:
                    if (this.trainImage.enabled)
                        this.trainImage.enabled = false;
                    if (!this.busImage.enabled)
                        this.busImage.enabled = true;
                    break;
                case Vehicle.None:
                    if (this.trainImage.enabled)
                        this.trainImage.enabled = false;
                    if (this.busImage.enabled)
                        this.busImage.enabled = false;
                    break;
            }
        }

        enum Verb
        {
            Vehicle,
            Station,
            Route,
            None
        }

        void setVerbImage(Verb v)
        {
            switch (v)
            {
                case Verb.Vehicle:
                    if (!this.vehicleImage.enabled)
                        this.vehicleImage.enabled = true;
                    if (this.stationImage.enabled)
                        this.stationImage.enabled = false;
                    if (this.routeImage.enabled)
                        this.routeImage.enabled = false;
                    break;
                case Verb.Station:
                    if (this.vehicleImage.enabled)
                        this.vehicleImage.enabled = false;
                    if (!this.stationImage.enabled)
                        this.stationImage.enabled = true;
                    if (this.routeImage.enabled)
                        this.routeImage.enabled = false;
                    break;
                case Verb.Route:
                    if (this.vehicleImage.enabled)
                        this.vehicleImage.enabled = false;
                    if (this.stationImage.enabled)
                        this.stationImage.enabled = false;
                    if (!this.routeImage.enabled)
                        this.routeImage.enabled = true;
                    break;
                case Verb.None:
                    if (this.vehicleImage.enabled)
                        this.vehicleImage.enabled = false;
                    if (this.stationImage.enabled)
                        this.stationImage.enabled = false;
                    if (this.routeImage.enabled)
                        this.routeImage.enabled = false;
                    break;
            }
        }
    }
}
