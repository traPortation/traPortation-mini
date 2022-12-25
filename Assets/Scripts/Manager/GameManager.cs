﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public GameObject roadFolder;
        public GameObject peopleFolder;

        StationManager StationManager;
        BusStationManager busStationManager;
        RailManager railManager;
        Traffic.Road.Factory roadFactory;
        // TODO: 消す
        DiContainer container;
        public ManageMoney ManageMoney { get; private set; }
        public GameStatus Status { get; private set; }
        AudioSwitcher audioSwitcher;
#nullable enable

        void Start()
        {
            this.SetStatus(GameStatus.Normal);

            this.initBoardForTest();
            this.InstantiatePeople();
        }

        [Inject]
        public void Construct(Board board, DiContainer container, StationManager stationManager,
            BusStationManager busStationManager, RailManager railManager,
            Traffic.Road.Factory roadFactory, ManageMoney manageMoney, AudioSwitcher audioSwitcher)
        {
            this.Board = board;
            this.container = container;
            this.StationManager = stationManager;
            this.busStationManager = busStationManager;
            this.railManager = railManager;
            this.roadFactory = roadFactory;

            this.ManageMoney = manageMoney;
            this.audioSwitcher = audioSwitcher;

            Utils.NullChecker.Check(this.person, this.building, this.train, this.StationManager);
        }

        /// <summary>
        /// Prefabから人をインスタンス化する
        /// </summary>
        private void InstantiatePeople()
        {
            for (int i = 0; i < Const.General.PersonCount; i++)
            {
                var start = new Vector3(Random.Range(Const.X.Min, Const.X.Max), Random.Range(Const.Y.Min, Const.Y.Max), Const.Z.Person);
                var obj = this.container.InstantiatePrefab(this.person);
                obj.transform.position = start;
                obj.transform.parent = this.peopleFolder.transform;
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
                var length = road.line.Distance();

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
                        this.roadFactory.Create(before, node, length);
                    }
                    before = node;
                }

                var (last, _) = neigbors.Last();
                if (last != road.line.end)
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

                    this.roadFactory.Create(before, node, length);
                }
            }
        }

        public void SetStatus(GameStatus status)
        {
            Debug.Log("Status Changed");
            this.Status = status;

            if (status == GameStatus.Normal)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }

        }
    }
}