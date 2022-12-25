namespace TraPortation.Game
{
    // 末尾以外に追加するとボタンが壊れる
    public enum GameStatus
    {
        Normal,
        Pause,
        SubMenu,

        SetTrain,
        SetRail,
        SetStation,

        SetBus,
        SetBusStation,
        SetBusRail,
    }
}