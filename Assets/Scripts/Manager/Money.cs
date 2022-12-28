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

        public bool ExpenseCheck(int expense)
        {
            return this.nowMoney - expense >= Const.Money.Min;
        }

        public bool Expense(int expense, bool allowMinus = false)
        {
            if (allowMinus)
            {
                this.nowMoney -= expense;
                return true;
            }
            else
            {
                if (ExpenseCheck(expense))
                {
                    this.nowMoney -= expense;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Income(int income)
        {
            this.nowMoney = Math.Min(this.nowMoney + income, Const.Money.Max);
        }

    }
}