using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 乗り物が駅に着いたときに発行されるイベント 
/// </summary>
public class VehicleArrivedEvent
{
    public int StationId { get; }
    // TODO: Vehicleをそのまま渡さないようにする
    public Vehicle Vehicle { get; }
    public VehicleArrivedEvent(int stationId, Vehicle vehicle)
    {
        this.StationId = stationId;
        this.Vehicle = vehicle;
    }
}
