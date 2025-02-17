public abstract class AbstractUserInterface
{
    public void printOperations(List<string> operations) {
        Console.WriteLine("Choose operation : {");
        operations.ForEach(x => Console.WriteLine(x));
        Console.WriteLine("}");
    }

    public int getOperationChoosed() {
        string text = Console.ReadLine();
        int answear = Convert.ToInt16(text);
        return answear;
    }

    public abstract void handleChoosedOperation();
}
