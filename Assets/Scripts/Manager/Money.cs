using System;


namespace TraPortation
{

    public class ManageMoney
    {
        private int nowMoney = Const.Money.Start;  // 所持金

        // 現在の所持金を返すだけの関数
        public int money
        {
            get => this.nowMoney;
        }

        // 所持金を下限と上限の間になるようにするやつ
        public bool ExpenseCheck(int expense)
        {
            if (Const.Money.minMoney <= this.nowMoney + expense && this.nowMoney + expense <= Const.Money.maxMoney)
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
            if (allowMinus)
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
}