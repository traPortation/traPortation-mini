namespace Const
{
    public static class Train
    {
        public const int Capacity = 4;
        public const int Wage = 100;
        public const float StopStationTime = 1f;
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
    }
}