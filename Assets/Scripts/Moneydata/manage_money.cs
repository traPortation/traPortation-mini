using System;


class Money
{
    private int nowmoney = 0;  // 所持金
    private static int minmoney = 0; // 下限
    private static int maxmoney = 999999999; // 上限

    // 現在の所持金を返すだけの関数
    public int rmoney() { return nowmoney; }

    // 所持金を下限と上限の間になるようにするやつ
    public bool moneycheck(int expense, bool mincheck)
    {
        if (minmoney <= nowmoney + expense && nowmoney + expense <= maxmoney)
        {
            nowmoney += expense;
            return true;
        }
        else if (nowmoney + expense > maxmoney)
        {
            nowmoney = maxmoney;
            return true;
        }
         else
        {
            // 所持金が下限になるような場合、0円にするかそのままにするか判断するやつ
            if (mincheck == true) // 所持金が0円になる方。路線維持費とかあれば
            {
                nowmoney = minmoney;
                return true;
            }
            else // 所持金がそのままの方。新たな路線を買えない時用
            {
                return false; // false返す→エラーメッセージ呼び出し等に活用してほしい
            }
        }
        
    }

}