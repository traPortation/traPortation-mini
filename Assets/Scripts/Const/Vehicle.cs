namespace TraPortation.Const
{
    public static class Train
    {
        public const int Capacity = 10;
        public const int Wage = 100;
        public const float StopStationTime = 0.1f;
        public const int VehicleCost = -1000;
        public const int StationCost = -3000;
    }

    /// <summary>
    /// バス及びその移動に関する定数
    /// </summary>
    public static class Bus
    {
        public const int Capacity = 2;
        public const int Wage = 50;
        public const float BusVelocity = 0.03f;
        public const float StopStationTime = 0.6f;
        public const int VehicleCost = -100;
        public const int StationCost = -50;
    }
}