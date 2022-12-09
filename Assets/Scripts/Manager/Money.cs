using System;
using TraPortation.Const;

public class ManageMoney
{
    private int nowMoney = 100000;  // 所持金
    // const.cs内のnamespace Const class MoneyLimitにある
    // private static const int minMoney = 0; // 下限
    // private static const int maxMoney = 999999999; // 上限

    // 現在の所持金を返すだけの関数
    public int money
    {
        get => this.nowMoney;
    }

    // 所持金を下限と上限の間になるようにするやつ
    public bool ExpenseCheck(int expense)
    {
        if (MoneyLimit.minMoney <= this.nowMoney + expense && this.nowMoney + expense <= MoneyLimit.maxMoney)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ExpenseMoney(int expense, bool allowMinus = false)
    {
        if(allowMinus)
        {
            this.nowMoney += expense;
            return true;
        }
        else
        {
            if (ExpenseCheck(expense))
            {
                this.nowMoney += expense;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}