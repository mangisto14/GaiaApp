
public class AddOperation : IOperation
{
    public string Name => "Add";

    public string Execute(string inputA, string inputB)
    {
        if (double.TryParse(inputA, out double a) && double.TryParse(inputB, out double b))
            return (a + b).ToString();
        return "Error";
    }
}
