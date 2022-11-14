using System.Collections.Generic;
using TraPortation.Traffic;
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
        DiContainer container;

        [Inject]
        public void Construct(Board board, DiContainer container)
        {
            this.board = board;
            this.container = container;
        }

        public BusStation AddBusStation(Vector3 vec)
        {
            var node = this.board.AddStationNode(vec.x, vec.y);

            var nearestNode = this.board.GetNearestNode(vec.x, vec.y);
            this.board.AddRoadEdge(nearestNode, node);
            this.board.AddRoadEdge(node, nearestNode);

            var newStation = container.InstantiatePrefab(this.prefab);
            newStation.transform.position = vec;
            var station = new BusStation();
            var view = newStation.GetComponent<BusStationView>();

            this.busStations.Add(station);

            return station;
        }
    }
}