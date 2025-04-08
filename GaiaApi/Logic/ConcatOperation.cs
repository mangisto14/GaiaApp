
public class ConcatOperation : IOperation
{
    public string Name => "Concat";

    public string Execute(string inputA, string inputB)
    {
        return inputA + inputB;
    }
}
