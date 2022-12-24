using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TraPortation.Game;
using TraPortation.Traffic;
using TraPortation.Traffic.Node;
using TraPortation.UI;
using UnityEngine;
using Zenject;

namespace TraPortation
{
    public class StationManager : MonoBehaviour
    {
        List<Station> stations = new List<Station>();
        [SerializeField] GameObject prefab;
        [SerializeField] GameObject stationIcon;
        GameManager gameManager;
        Board board;
        DiContainer container;

        void Start()
        {
            stationIcon.transform.position = new Vector3(Const.Map.XMin - 1, Const.Map.YMin - 1, Const.Z.MouseIcon);
            stationIcon.SetActive(false);
        }

        // TODO: 別のクラスに分ける
        void Update()
        {
            if (this.gameManager.Status != GameStatus.SetStation)
            {
                if (stationIcon.activeSelf)
                {
                    stationIcon.transform.position = new Vector3(Const.Map.XMin - 1, Const.Map.YMin - 1, Const.Z.MouseIcon);
                    stationIcon.SetActive(false);
                }
                return;
            }

            if (!stationIcon.activeSelf)
            {
                stationIcon.SetActive(true);
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Const.Z.MouseIcon;
            stationIcon.transform.position = mousePosition;
            Color stationColor = stationIcon.GetComponent<SpriteRenderer>().color;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, Mathf.Infinity);

            if (hitInfo.collider != null && hitInfo.collider.gameObject.name == "RoadView" && this.gameManager.ManageMoney.ExpenseCheck(Const.Train.StationCost))
            {
                stationColor.a = 1f;
                stationIcon.GetComponent<SpriteRenderer>().material.color = stationColor;

                if (Input.GetMouseButtonDown(0) && this.gameManager.ManageMoney.ExpenseMoney(Const.Train.StationCost, false))
                {
                    this.AddStation(mousePosition.x, mousePosition.y);
                }
            }
            else
            {
                stationColor.a = 0.5f;
                stationIcon.GetComponent<SpriteRenderer>().material.color = stationColor;
            }
        }

        [Inject]
        public void Construct(GameManager gameManager, Board board, DiContainer container)
        {
            this.gameManager = gameManager;
            this.board = board;
            this.container = container;
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
            newStation.name = "Station";
            newStation.transform.position = new Vector3(x, y, Const.Z.Station);
            var station = new Station(node);
            var view = newStation.GetComponent<StationView>();
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