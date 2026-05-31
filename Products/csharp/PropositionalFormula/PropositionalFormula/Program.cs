namespace PropositionalFormula
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string logicFormula = Console.ReadLine();
            Console.WriteLine(PropositionalFormula.Create(logicFormula).ToString());

            Dictionary<string, bool> inputValues = new Dictionary<string, bool>();
            if (int.TryParse(Console.ReadLine(), out int variableCount))
            {
                for (int i = 0; i < variableCount; i++)
                {
                    inputValues.Add(Console.ReadLine(), bool.Parse(Console.ReadLine()));
                }
            }
            Console.WriteLine(PropositionalFormula.Create(logicFormula).TryGetValue(inputValues, out bool b));
            Console.WriteLine(b);

            Console.ReadLine();
        }
    }
}
