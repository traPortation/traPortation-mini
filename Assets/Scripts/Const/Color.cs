using System.Collections.Generic;

namespace TraPortation.Const
{
    // 名前衝突の回避
    using UColor = UnityEngine.Color;

    public static class Color
    {
        public static readonly UColor Road = new UColor(212 / 255f, 212 / 255f, 212 / 255f);
        public static readonly IReadOnlyList<UColor> RailColors = new List<UColor>() {
            HexC("EE5050"),
            HexC("FFE920"),
            HexC("65EE62"),
            HexC("63C7FF"),
            HexC("5451F5"),
            HexC("F551D1"),
            HexC("FF6C01"),
            HexC("CAEE62"),
            HexC("35FBCB"),
            HexC("6398FF"),
            HexC("A351F5"),
            HexC("F55178"),
        };


        public static readonly IReadOnlyList<UColor> BusRails = new List<UColor>() {
            HexC("BC1B42"),
            HexC("450089"),
            HexC("065987"),
            HexC("00A47C"),
            HexC("6D9400"),
            HexC("BA4E00"),
            HexC("7A0260"),
            HexC("030098"),
            HexC("007ABF"),
            HexC("0C800A"),
            HexC("B9A600"),
            HexC("8C0000"),
        };

        //HEXを変換する関数定義
        public static UColor HexC(string hex)
        {
            //HEXをRGBに変換
            float r = (float)System.Convert.ToInt32(hex.Substring(0, 2), 16) / 255f;
            float g = (float)System.Convert.ToInt32(hex.Substring(2, 2), 16) / 255f;
            float b = (float)System.Convert.ToInt32(hex.Substring(4, 2), 16) / 255f;

            //RGBをColor型に変換
            UColor color = new UColor(r, g, b, 1);

            return color;
        }
    }
}