using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Task1
{
    internal class Calculator : ICalc
    {
        public event EventHandler<EventArgs> GotResult;
        public double Result = 0;
        private Stack<double> Results = new Stack<double>();
        private Stack<CalcActionLog> actions = new Stack<CalcActionLog>();

        public void Divide(int value)
        {
            if (value == 0)
            {
                actions.Push(new CalcActionLog(CalcAction.Divide, value));
                throw new CalculatorDivideByZeroException("Нельзя делить на 0!", actions);
            }

            Results.Push(Result);
            Result /= value;
            RaisEvent();

        }

        public void Divide(double value)
        {
            if (value == 0)
            {
                actions.Push(new CalcActionLog(CalcAction.Divide, value));
                throw new CalculatorDivideByZeroException("Нельзя делить на 0!", actions);
            }

            Results.Push(Result);
            Result /= value;
            RaisEvent();
        }

        public void Multiply(int value)
        {
            /*long temp = value * Result;
            if (temp > int.MaxValue)
            {
                actions.Push(new CalcActionLog(CalcAction.Multiply, value));
                throw new CalculateOperationCauseOverflowException("Переполнение", actions);
            }*/
            Results.Push(Result);
            Result *= value;
            RaisEvent();
        }

        public void Multiply(double value)
        {
            Results.Push(Result);
            Result *= value;
            RaisEvent();
        }

        public void Subatruct(int value)
        {
            /*long temp = Result - value;
            if (temp < int.MinValue || (Result == int.MinValue && value == int.MaxValue))
            {
                actions.Push(new CalcActionLog(CalcAction.Subatruct, value));
                throw new CalculateOperationCauseOverflowException("Переполнение", actions);
            }*/
            Results.Push(Result);
            Result -= value;
            RaisEvent();
        }

        public void Subatruct(double value)
        {     
            Results.Push(Result);
            Result -= value;
            RaisEvent();
        }

        public void Sum(int value)
        {
            ulong temp = (ulong)(value + Result);

            if (temp > int.MaxValue)
            {
                actions.Push(new CalcActionLog(CalcAction.Sum, value));
                throw new CalculateOperationCauseOverflowException("Переполнение", actions);
            }
            Results.Push(Result);
            Result += value;
            RaisEvent();
        }

        public void Sum(double value)
        {
            Results.Push(Result);
            Result += value;
            RaisEvent();
        }

        private void RaisEvent()
        {
            GotResult?.Invoke(this, EventArgs.Empty);
        }

        public void CancelLast()
        {
            if (Results.Count > 0)
            {
                Result = Results.Pop();
                RaisEvent();
            }
        }
    }
}

