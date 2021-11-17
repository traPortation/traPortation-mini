using System;
using Const;

class ManageMoney
{
    private int nowMoney = 0;  // 所持金
    // const.cs内のnamespace Const class MoneyLimitにある
    // private static const int minMoney = 0; // 下限
    // private static const int maxMoney = 999999999; // 上限

    // 現在の所持金を返すだけの関数
    public int money { get; private set; }

    // 所持金を下限と上限の間になるようにするやつ
    public bool ExpenseCheck(int expense, bool allowMinus)
    {
        if (MoneyLimit.minMoney <= this.nowMoney + expense && this.nowMoney + expense <= MoneyLimit.maxMoney)
        {
            this.nowMoney += expense;
        }
        else if (this.nowMoney + expense > MoneyLimit.maxMoney)
        {
            this.nowMoney = MoneyLimit.maxMoney;
        }
        else
        {
            // 所持金が下限になるような場合、0円にするかそのままにするか判断するやつ
            // 所持金が0円になる方。路線維持費とかあれば
            if (allowMinus == true)
            {
                this.nowMoney = MoneyLimit.minMoney;
            }

            // 所持金がそのままの方。新たな路線を買えない時用
            else
            {
                // false返す→エラーメッセージ呼び出し等に活用してほしい
                return false;
            }
        }

        return true;

    }

}