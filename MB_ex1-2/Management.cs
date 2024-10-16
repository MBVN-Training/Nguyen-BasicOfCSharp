namespace MB_ex1;

public abstract class Management
{
    protected readonly Database? _db=Database.GetInstance();
    public abstract void ShowMenu();
    public abstract void ChoseOption();

    protected static void PauseAndClear()
    {
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        Console.Clear();
    }
    protected static void ShowItemsInfo(List<Object> items)
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
}