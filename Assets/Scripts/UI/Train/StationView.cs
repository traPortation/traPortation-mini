using MessagePipe;
using TMPro;
using TraPortation.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation.UI
{
    public class StationView : MonoBehaviour, IStationView
    {
        public Station Station { get; private set; }
        [SerializeField] TextMeshProUGUI info;
        public void SetStation(Station station)
        {
            this.Station = station;
            this.info.text = "0";
        }

        public void SetPeopleCount(int count)
        {
            this.info.text = count.ToString();
        }
    }
}