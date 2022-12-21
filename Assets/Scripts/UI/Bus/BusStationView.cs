using MessagePipe;
using TraPortation.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation.UI
{
    public class BusStationView : MonoBehaviour
    {
        public BusStation BusStation { get; private set; }
        public void SetBusStation(BusStation busStation)
        {
            this.BusStation = busStation;
        }
    }
}