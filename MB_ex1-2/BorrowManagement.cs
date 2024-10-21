
namespace MB_ex1;

public class BorrowManagement: Management
{
    public override void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Borrow Management");
        Console.WriteLine("1. Borrow an item");
        Console.WriteLine("2. Return an item");
        Console.WriteLine("3. borrow history");
        Console.WriteLine("4. list borrowers");
        Console.WriteLine("5. Exit");
        Console.Write("Choose an option:");
    }

    public override void ChoseOption()
    {
        do
        {
            ShowMenu();
            int.TryParse(Console.ReadLine(), out var option);
            Console.Clear();
            switch (option)
            {
                case 1:
                    BorrowItem();
                    break;
                case 2:
                    ReturnItem();
                    break;
                case 3:
                    ShowHistory();
                    break;
                case 4:
                    ShowBorrowers();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            PauseAndClear();
        } while (true);
    }
    
    private void ShowBorrowers()
    {
        var borrowers= Db.GetBorrowers();
        ShowItemsInfo(borrowers.ToList());
    }

    private void ShowHistory()
    {
        var histories = Db.GetBorrowingHistories();
        ShowItemsInfo(histories.OrderBy(h=>h.BorrowerLibraryCardNumber).ToList());
    }

    private void ReturnItem()
    {
        Console.WriteLine("Enter your library card number:");
        int libraryCardNumber = int.Parse(Console.ReadLine()?.Trim()??string.Empty);
        Console.WriteLine("Enter the item id:");
        Guid itemId = Guid.Parse(Console.ReadLine()?.Trim()??string.Empty);
        Db.ReturnItem(itemId, libraryCardNumber, DateTime.Now);
        Console.WriteLine("Item returned successfully");
    }

    private void BorrowItem()
    {
        Console.WriteLine("Enter your library card number:");
        int libraryCardNumber = int.Parse(Console.ReadLine()?.Trim()??string.Empty);
        Console.WriteLine("list available items:");
        var listItem = Db.GetAvailableLibraryItems();
        ShowItemsInfo(listItem.ToList());
        Console.WriteLine("Enter the item id:");
        Guid itemId = Guid.Parse(Console.ReadLine()?.Trim()??string.Empty);
        Db.BorrowItem(itemId, libraryCardNumber, DateTime.Now);
        Console.WriteLine("Item borrowed successfully");
    }
}