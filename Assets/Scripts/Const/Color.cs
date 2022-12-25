using System.Collections.Generic;

namespace TraPortation.Const
{
    // 名前衝突の回避
    using UColor = UnityEngine.Color;

    public static class Color
    {
        public static readonly UColor Road = new UColor(212 / 255f, 212 / 255f, 212 / 255f);
        public static readonly IReadOnlyList<UColor> RailColors = new List<UColor>() {
            new UColor(1, 0, 0, 1),
            new UColor(0, 1, 0, 1),
            new UColor(0, 0, 1, 1),
            new UColor(1, 1, 0, 1),
            new UColor(1, 0, 1, 1),
            new UColor(0, 1, 1, 1),
            new UColor(0.5f, 0, 0, 1),
            new UColor(0, 0.5f, 0, 1),
            new UColor(0, 0, 0.5f, 1),
            new UColor(0.5f, 0.5f, 0, 1),
            new UColor(0.5f, 0, 0.5f, 1),
            new UColor(0, 0.5f, 0.5f, 1),
            new UColor(0.5f, 0.5f, 0.5f, 1),
            new UColor(1, 0.5f, 1, 1),
            new UColor(1f, 0.7f, 0.5f, 1f),
            new UColor(1f, 0.9f, 0.3f, 1f),
            new UColor(0.3f, 0.3f, 0.3f, 1f),
            new UColor(1, 0.4f, 0.2f, 1f),
            new UColor(0.7f, 0.3f, 0.4f, 1f),
            new UColor(0.2f, 0.6f, 0.3f, 1f),
        };


        public static readonly IReadOnlyList<UColor> BusRails = new List<UColor>() {
            UColor.yellow,
            UColor.green,
        };


        // 路線設置時の色
        // 設置後の色と揃えたほうがいいかも
        public static readonly UColor SetRail = new UColor(0, 0, 1, 0.5f);
        public static readonly UColor SetBusRail = new UColor(1, 0, 0, 0.5f);

    }
}