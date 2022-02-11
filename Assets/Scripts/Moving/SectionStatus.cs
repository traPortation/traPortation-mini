namespace Moving
{
    public enum SectionStatus
    {
        // 共通
        NotStarted,
        Finished,

        // WalkSection用
        Walking,

        // TrainUsingSection用
        OnStation,
        OnTrain
    }
}