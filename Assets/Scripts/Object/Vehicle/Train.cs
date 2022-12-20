using System.Collections;
using System;
using UnityEngine;
using MessagePipe;
using Zenject;
using TraPortation.Event.Train;

#nullable enable

namespace TraPortation
{
    public class Train : Vehicle
    {
        [Inject] ISubscriber<TrainRideEvent> Boarded { get; set; }
        private IDisposable disposable;
        void Awake(){
            Debug.Log("Awake");
            var d = DisposableBag.CreateBuilder();
            Boarded.Subscribe(e => {
                Debug.Log("hoge" + e.TrainID);
            }).AddTo(d);
            disposable = d.Build();
            //log
            Debug.Log(this.ID + " " + " " + this.PassengerCount);
        }
        void Start()
        {
            this.Capacity = Const.Train.Capacity;
            this.Wage = Const.Train.Wage;
            this.velocity = Const.Velocity.Train;
            this.PassengerCount = 0;
        }

        void FixedUpdate()
        {
            this.Move(this.velocity);
        }
         void OnDestroy(){
            disposable.Dispose();
        }
    }
}
