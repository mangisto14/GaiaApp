
using System.Collections.Generic;
using System.Linq;

public static class OperationFactory
{
    private static List<IOperation> operations = new List<IOperation>
    {
        new AddOperation(),
        new SubtractOperation(),
        new ConcatOperation()
    };

    public static List<string> GetOperationNames() => operations.Select(o => o.Name).ToList();

    public static IOperation GetOperationByName(string name)
    {
        return operations.FirstOrDefault(o => o.Name == name);
    }
}
