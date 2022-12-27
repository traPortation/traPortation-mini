using System.Collections.Generic;
using TraPortation.Game;
using TraPortation.Traffic;
using TraPortation.Traffic.Node;
using TraPortation.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation
{
    // NOTE: StationManagerと共通部分が多い
    public class BusStationManager : MonoBehaviour
    {
        List<BusStation> busStations = new List<BusStation>();
        Board board;
        [SerializeField] GameObject prefab;
        [SerializeField] GameObject stationIcon;
        MouseIcon icon;
        DiContainer container;
        GameManager gameManager;
        InputManager inputManager;

        [Inject]
        public void Construct(Board board, DiContainer container, GameManager gameManager, InputManager inputManager)
        {
            this.board = board;
            this.container = container;
            this.gameManager = gameManager;
            this.inputManager = inputManager;
        }

        void Start()
        {
            this.icon = new MouseIcon(stationIcon, this.inputManager);
            this.icon.SetActive(false);
        }

        void Update()
        {
            if (this.gameManager.Status != GameStatus.SetBusStation)
            {
                this.icon.SetActive(false);
                return;
            }

            this.icon.SetActive(true);
            this.icon.Update();

            var mask = LayerMask.GetMask("Road");
            var obj = this.inputManager.RayCast(mask);

            if (obj != null && obj.name == "RoadView"
                && this.gameManager.ManageMoney.ExpenseCheck(Const.Money.BusStationCost))
            {
                this.icon.SetAlpha(1.0f);

                if (Input.GetMouseButtonDown(0) && this.gameManager.ManageMoney.Expense(Const.Money.BusStationCost))
                {
                    var pos = this.inputManager.GetMousePosition();
                    this.AddBusStation(pos.x, pos.y);
                }
            }
            else
            {
                this.icon.SetAlpha(0.5f);
            }
        }

        public BusStation AddBusStation(float x, float y)
        {
            var (road, _) = this.board.GetNearestRoad(new Vector2(x, y));
            var node = this.board.AddStationNode(x, y, StationKind.Bus);
            this.board.AddRoadEdge(road.From, node);
            this.board.AddRoadEdge(node, road.From);
            this.board.AddRoadEdge(road.To, node);
            this.board.AddRoadEdge(node, road.To);

            var newStation = container.InstantiatePrefab(this.prefab);
            newStation.name = "BusStation";
            newStation.transform.position = new Vector3(x, y, Const.Z.BusStation);
            var station = new BusStation(node);
            var view = newStation.GetComponent<BusStationView>();
            view.SetBusStation(station);

            this.busStations.Add(station);

            return station;
        }

        public BusStation GetBusStation(StationNode node)
        {
            return this.busStations.Find(s => s.Node.Index == node.Index);
        }
    }
}