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
                return;
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 8f;
            stationIcon.transform.position = mousePosition;
            Color stationColor = stationIcon.GetComponent<SpriteRenderer>().color;

            //それ以外の場合はbuildModeを判定し、駅を追加する
            // TODO: 道の形を考慮
            if ((Mathf.Abs(mousePosition.x - Mathf.Round(mousePosition.x)) < 0.1 || Mathf.Abs(mousePosition.y - Mathf.Round(mousePosition.y)) < 0.1))
            {
                stationColor.a = 1f;
                stationIcon.GetComponent<SpriteRenderer>().material.color = stationColor;
            }
            else
            {
                stationColor.a = 0.5f;
                stationIcon.GetComponent<SpriteRenderer>().material.color = stationColor;
            }
        }

        [Inject]
        public void Construct(GameManager gameManager, Board board, DiContainer container, ISubscriber<RoadClickedEvent> subscriber)
        {
            this.gameManager = gameManager;
            this.board = board;
            this.container = container;

            // TODO: dispose
            subscriber.Subscribe(e =>
            {
                if (this.gameManager.Status != GameStatus.SetStation)
                {
                    return;
                }

                var pos = new Vector3(e.Position.x, e.Position.y, 8f);
                this.AddStation(pos);
            });
        }

        public void SetBuildMode()
        {
            this.gameManager.SetStatus(this.gameManager.Status switch
            {
                GameStatus.SetStation => GameStatus.Normal,
                _ => GameStatus.SetStation
            });

            stationIcon.SetActive(this.gameManager.Status == GameStatus.SetStation);
        }

        /// <summary>
        /// 指定した場所にstationを追加する
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public Station AddStation(Vector3 vec)
        {
            int index = this.stations.Count;
            var node = this.board.AddStationNode(vec.x, vec.y);

            var nearestNode = this.board.GetNearestNode(vec.x, vec.y);
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
        /// 指定したIDのstationを取得
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public Station GetStation(int stationId)
        {
            //TODO: 応急処置なのでそのうちどうにかする
            return this.stations.Where(station => station.ID == stationId).First();
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