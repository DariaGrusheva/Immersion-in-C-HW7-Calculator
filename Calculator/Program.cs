namespace Task1
{
    internal interface ICalc
    {
        event EventHandler<EventArgs> GotResult;

        void Sum(double Value);
        void Subatruct(double Value);
        void Multiply(double Value);
        void Divide(double Value);
        void CancelLast();

    }



    internal class Program
    {
        static void Calculator_GotResult(object sendler, EventArgs evenArgs)
        {
            Console.WriteLine($"{((Calculator)sendler).Result}");
        }

        static void Execute(Action<double> action, double value)
        {
            try
            {
                action.Invoke(value);
            }
            catch (CalculatorDivideByZeroException ex)
            {
                Console.WriteLine(ex);
            }
            catch (CalculateOperationCauseOverflowException ex)
            {
                Console.WriteLine(ex);
            }
        }
        static void Main(string[] args)
        {

            ICalc calc = new Calculator();
            calc.GotResult += Calculator_GotResult;

            calc.Sum(10);
            calc.Sum(10.5);
            calc.Divide(0.5);
            calc.Subatruct(10);

            /*Execute(calc.Subatruct, int.MaxValue);
            Execute(calc.Subatruct, int.MaxValue);
            Execute(calc.Sum, int.MaxValue);
            Execute(calc.Sum, 10);
            Execute(calc.Divide, 0);
            Execute(calc.Multiply);*/
        }
    }
}


