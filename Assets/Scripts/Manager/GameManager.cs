using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TraPortation.Const;
using TraPortation.Core.RoadGen;
using TraPortation.Game;
using TraPortation.Moving;
using TraPortation.Traffic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

#nullable enable

namespace TraPortation
{
    public class GameManager : MonoBehaviour
    {

#nullable disable
        Board Board;
        [SerializeField] GameObject person;
        [SerializeField] GameObject building;
        [SerializeField] GameObject train;
        StationManager StationManager;
        RailManager railManager;
        Traffic.Road.Factory roadFactory;
        // TODO: 消す
        DiContainer container;
        public ManageMoney ManageMoney { get; private set; }
        public GameStatus Status { get; private set; }
#nullable enable

        void Start()
        {
            this.SetStatus(GameStatus.Normal);

            this.initBoardForTest();
            this.InstantiatePeople();
        }

        [Inject]
        public void Construct(Board board, DiContainer container, StationManager stationManager, RailManager railManager, Traffic.Road.Factory roadFactory)
        {
            this.Board = board;
            this.container = container;
            this.StationManager = stationManager;
            this.railManager = railManager;
            this.roadFactory = roadFactory;

            this.ManageMoney = new ManageMoney();

            Utils.NullChecker.Check(this.person, this.building, this.train, this.StationManager);
        }

        /// <summary>
        /// Prefabから人をインスタンス化する
        /// </summary>
        private void InstantiatePeople()
        {
            for (int i = 0; i < Count.Person; i++)
            {
                var start = new Vector3(Random.Range(X.Min, X.Max), Random.Range(Y.Min, Y.Max), Z.Person);
                var obj = this.container.InstantiatePrefab(this.person);
                obj.transform.position = start;
            }
        }

        /// <summary>
        /// 動作確認用に色々置いてるだけ
        /// </summary>
        private void initBoardForTest()
        {
            var generator = new RoadGenerator();
            generator.GenerateRoads();

            var q = new Queue<Core.RoadGen.Road>();
            var dict = new Dictionary<Vector2, Traffic.Node.IntersectionNode>();

            q.Enqueue(generator.roads[0]);

            while (q.Count != 0)
            {
                var road = q.Dequeue();

                var neigbors = road.GetNeigbors();
                Traffic.Node.IntersectionNode? before = null;

                if (road.line.start != neigbors[0].Item1)
                {
                    if (dict.ContainsKey(road.line.start))
                    {
                        before = dict[road.line.start];
                    }
                    else
                    {
                        before = this.Board.AddIntersectionNode(road.line.start.x, road.line.start.y);
                        dict.Add(road.line.start, before);
                    }
                }

                foreach (var (v, r) in neigbors)
                {
                    Traffic.Node.IntersectionNode node;

                    if (dict.ContainsKey(v))
                    {
                        node = dict[v];
                    }
                    else
                    {
                        node = this.Board.AddIntersectionNode(v.x, v.y);
                        dict.Add(v, node);
                        q.Enqueue(r);
                    }

                    if (before != null)
                    {
                        this.roadFactory.Create(before, node);
                    }
                    before = node;
                }


                if (neigbors.Last().Item1 != road.line.end)
                {
                    Traffic.Node.IntersectionNode node;

                    if (dict.ContainsKey(road.line.end))
                    {
                        node = dict[road.line.end];
                    }
                    else
                    {
                        node = this.Board.AddIntersectionNode(road.line.end.x, road.line.end.y);
                        dict.Add(road.line.end, node);
                    }
                
                    this.roadFactory.Create(before, node);
                }
            }

            /*
            // 交差点を追加
            var nodes = Enumerable.Range(0, (int)X.Max + 1).Select(x =>
            {
                return Enumerable.Range(0, (int)Y.Max + 1).Select(y =>
                {
                    return this.Board.AddIntersectionNode(x, y);
                }).ToList();
            }).ToList();

            // 道を追加
            roadFactory.Create(nodes[0][0], nodes[1][1]);

            foreach (var x in Enumerable.Range(0, (int)X.Max + 1))
            {
                foreach (var y in Enumerable.Range(0, (int)Y.Max + 1))
                {
                    if (x != X.Max) roadFactory.Create(nodes[x][y], nodes[x + 1][y]);
                    if (y != Y.Max) roadFactory.Create(nodes[x][y], nodes[x][y + 1]);
                }
            }

            // 駅を追加
            var station1 = this.StationManager.AddStation(new Vector3(2, 2, 5f));
            var station2 = this.StationManager.AddStation(new Vector3(2, 6, 5f));
            var station3 = this.StationManager.AddStation(new Vector3(10, 6, 5f));

            // 駅同士を繋げる
            this.Board.AddVehicleRoute(station1.Node, station2.Node, EdgeType.Train);
            this.Board.AddVehicleRoute(station2.Node, station1.Node, EdgeType.Train);
            this.Board.AddVehicleRoute(station2.Node, station3.Node, EdgeType.Train);
            this.Board.AddVehicleRoute(station3.Node, station2.Node, EdgeType.Train);

            // 電車を追加
            GameObject trainObject = container.InstantiatePrefab(this.train);
            var train = trainObject.GetComponent<Train>();

            var stations = new List<Station>() { station1, station2, station3 };

            var rail = this.railManager.AddRail(stations);

            rail.AddTrain(train);
            */
        }

        public void SetStatus(GameStatus status)
        {
            Debug.Log("Status Changed");
            this.Status = status;

            Time.timeScale = status switch
            {
                GameStatus.Normal => 1,
                _ => 0
            };
        }
    }
}