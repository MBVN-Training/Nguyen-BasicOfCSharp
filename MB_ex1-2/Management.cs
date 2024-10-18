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
    protected static void ShowItemsInfo<T>(List<T> items)
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
    // protected static void ShowItemsInfo<T>(List<T> items)
    // {
    //     if (items == null || items.Count == 0)
    //     {
    //         Console.WriteLine("No items to display.");
    //         return;
    //     }
    //
    //     var properties = typeof(T).GetProperties();
    //     // Print header
    //     foreach (var prop in properties)
    //     {
    //         Console.Write($"{prop.Name}\t");
    //     }
    //     Console.WriteLine();
    //
    //     // Print items
    //     foreach (var item in items)
    //     {
    //         foreach (var prop in properties)
    //         {
    //             Console.Write($"{prop.GetValue(item)}\t");
    //         }
    //         Console.WriteLine();
    //     }
    // }
}