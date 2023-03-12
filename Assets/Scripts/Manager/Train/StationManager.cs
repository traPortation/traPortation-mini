using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MessagePipe;
using TraPortation.Event;
using TraPortation.Game;
using TraPortation.Traffic;
using TraPortation.Traffic.Node;
using TraPortation.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation
{
    public class StationManager : MonoBehaviour
    {
        List<Station> stations = new List<Station>();
        [SerializeField] GameObject prefab;
        [SerializeField] GameObject stationIcon;
        MouseIcon icon;
        GameManager gameManager;
        InputManager inputManager;
        IPublisher<CreatedEvent> publisher;
        Board board;
        DiContainer container;

        void Start()
        {
            this.icon = new MouseIcon(stationIcon, this.inputManager);
            this.icon.SetActive(false);
        }

        // TODO: 別のクラスに分ける
        void Update()
        {
            if (this.gameManager.Status != GameStatus.SetStation)
            {
                this.icon.SetActive(false);
                return;
            }

            this.icon.SetActive(true);
            this.icon.Update();

            var obj = this.inputManager.RayCast(LayerMask.GetMask("Road"));
            if (obj != null && obj.name == "RoadView"
                && this.gameManager.ManageMoney.ExpenseCheck(Const.Money.StationCost))
            {
                this.icon.SetAlpha(1.0f);

                if (Input.GetMouseButtonDown(0)
                    && this.gameManager.ManageMoney.Expense(Const.Money.StationCost))
                {
                    var mousePosition = this.inputManager.GetMousePosition();
                    this.AddStation(mousePosition.x, mousePosition.y);
                }
            }
            else
            {
                this.icon.SetAlpha(0.5f);
            }
        }

        [Inject]
        public void Construct(GameManager gameManager, Board board, DiContainer container, InputManager inputManager, IPublisher<CreatedEvent> publisher)
        {
            this.gameManager = gameManager;
            this.inputManager = inputManager;
            this.board = board;
            this.container = container;
            this.publisher = publisher;
        }

        /// <summary>
        /// 指定した場所にstationを追加する
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public Station AddStation(float x, float y)
        {
            var (road, _) = this.board.GetNearestRoad(new Vector2(x, y));
            var node = this.board.AddStationNode(x, y, StationKind.Train);

            this.board.AddRoadEdge(road.From, node);
            this.board.AddRoadEdge(node, road.From);
            this.board.AddRoadEdge(road.To, node);
            this.board.AddRoadEdge(node, road.To);

            GameObject newStation = container.InstantiatePrefab(this.prefab);
            this.publisher.Publish(new CreatedEvent(CreateType.Station));
            newStation.name = "Station";
            newStation.transform.position = new Vector3(x, y, Const.Z.Station);
            var view = newStation.GetComponent<StationView>();
            var station = new Station(node, view);
            view.SetStation(station);

            this.stations.Add(station);

            return station;
        }

        /// <summary>
        /// 指定したStationNodeを持つstationを取得 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Station GetStation(StationNode node)
        {
            return this.stations.Where(station => station.Node.Index == node.Index).First();
        }
    }
}