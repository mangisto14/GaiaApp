
public interface IOperation
{
    string Name { get; }
    string Execute(string inputA, string inputB);
}
