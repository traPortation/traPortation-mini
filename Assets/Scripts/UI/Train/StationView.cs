using MessagePipe;
using TraPortation.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation.UI
{
    public class StationView : MonoBehaviour
    {
        public Station Station { get; private set; }
        public void SetStation(Station station)
        {
            this.Station = station;
        }
    }
}