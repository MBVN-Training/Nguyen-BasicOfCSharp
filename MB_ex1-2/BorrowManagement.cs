using MB_ex1.Entity;

namespace MB_ex1;

public class BorrowManagement: Management
{
    private Database _db = Database.GetInstance();
    public override void ShowMenu()
    {
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
            int option = int.Parse(Console.ReadLine().Trim());
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
        var borrowers= _db.GetBorrowers();
        ShowItemsInfo(borrowers.Cast<Object>().ToList());
    }

    private void ShowHistory()
    {
        var histories = _db.GetBorrowingHistories();
        ShowItemsInfo(histories.OrderBy(histories=>histories.BorrowerLibraryCardNumber).Cast<Object>().ToList());
    }

    private void ReturnItem()
    {
        Console.WriteLine("Enter your library card number:");
        int libraryCardNumber = int.Parse(Console.ReadLine().Trim());
        Console.WriteLine("Enter the item id:");
        Guid itemId = Guid.Parse(Console.ReadLine().Trim());
        _db.ReturnItem(itemId, libraryCardNumber, DateTime.Now);
        Console.WriteLine("Item returned successfully");
    }

    private void BorrowItem()
    {
        Console.WriteLine("Enter your library card number:");
        int libraryCardNumber = int.Parse(Console.ReadLine().Trim());
        Console.WriteLine("list available items:");
        var listItem = _db.GetAvailableLibaryItems();
        ShowItemsInfo(listItem.Cast<object>().ToList());
        Console.WriteLine("Enter the item id:");
        Guid itemId = Guid.Parse(Console.ReadLine().Trim());
        _db.BorrowItem(itemId, libraryCardNumber, DateTime.Now);
        Console.WriteLine("Item borrowed successfully");
    }
}