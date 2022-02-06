using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 乗り物が駅に着いたときに発行されるイベント 
/// </summary>
public class VehicleArrivedEvent
{
    public readonly Vehicle Vehicle;
    public readonly Station NextStation;
    public VehicleArrivedEvent(Vehicle vehicle, Station nextStation)
    {
        this.Vehicle = vehicle;
        this.NextStation = nextStation;
    }
}
