
public class SubtractOperation : IOperation
{
    public string Name => "Subtract";

    public string Execute(string inputA, string inputB)
    {
        if (double.TryParse(inputA, out double a) && double.TryParse(inputB, out double b))
            return (a - b).ToString();
        return "Error";
    }
}
