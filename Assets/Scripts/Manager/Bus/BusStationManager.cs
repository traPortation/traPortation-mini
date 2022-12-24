using System.Collections.Generic;
using TraPortation.Game;
using TraPortation.Traffic;
using TraPortation.Traffic.Node;
using TraPortation.UI;
using UnityEngine;
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
        DiContainer container;
        GameManager gameManager;

        [Inject]
        public void Construct(Board board, DiContainer container, GameManager gameManager)
        {
            this.board = board;
            this.container = container;
            this.gameManager = gameManager;
        }

        void Start()
        {
            stationIcon.transform.position = new Vector3(Const.Map.XMin - 1, Const.Map.YMin - 1, Const.Z.MouseIcon);
            stationIcon.SetActive(false);
        }

        void Update()
        {
            if (this.gameManager.Status != GameStatus.SetBusStation)
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

            if (hitInfo.collider != null && hitInfo.collider.gameObject.name == "RoadView")
            {
                stationColor.a = 1f;
                stationIcon.GetComponent<SpriteRenderer>().material.color = stationColor;

                if (Input.GetMouseButtonDown(0))
                {
                    this.AddBusStation(mousePosition.x, mousePosition.y);
                }
            }
            else
            {
                stationColor.a = 0.5f;
                stationIcon.GetComponent<SpriteRenderer>().material.color = stationColor;
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