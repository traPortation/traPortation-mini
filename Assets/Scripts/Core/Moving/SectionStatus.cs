namespace TraPortation.Moving
{
    public enum SectionStatus
    {
        // 共通
        NotStarted,
        Finished,
        Moving,

        // TrainUsingSection用
        OnStation,
        OnTrain,

        // BusUsingSection用
        OnBusStation,
        OnBus,
    }
}