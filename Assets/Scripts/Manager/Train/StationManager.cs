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

        // TODO: 別のクラスに分ける
        void Update()
        {
            if (this.gameManager.Status != GameStatus.SetStation)
            {
                if (stationIcon.activeSelf)
                {
                    stationIcon.SetActive(false);
                }
                return;
            }

            if (!stationIcon.activeSelf)
            {
                stationIcon.SetActive(true);
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 8f;
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
                    this.AddStation(mousePosition);
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
        public Station AddStation(Vector3 vec)
        {
            var nearestNode = this.board.GetNearestNode(vec.x, vec.y);
            var node = this.board.AddStationNode(vec.x, vec.y, StationKind.Train);
            this.board.AddRoadEdge(nearestNode, node);
            this.board.AddRoadEdge(node, nearestNode);

            GameObject newStation = container.InstantiatePrefab(this.prefab);
            newStation.transform.position = vec;
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