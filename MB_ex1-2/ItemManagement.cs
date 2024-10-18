using System.Globalization;
using MB_ex1.Entity;

namespace MB_ex1;

public class ItemManagement: Management
{
     public override void ShowMenu()
    {
        Console.WriteLine("Item management");
        Console.WriteLine("1. List items");
        Console.WriteLine("2. List available items");
        Console.WriteLine("3. List borrowed items");
        Console.WriteLine("4. Add item");
        Console.WriteLine("5. Update item");
        Console.WriteLine("6. Delete item");
        Console.WriteLine("7. Search item");
        Console.WriteLine("8. Exit");
        Console.Write("Choose an option:");
    }

    public override void ChoseOption()
    {
        do
        {
            ShowMenu();
            int.TryParse(Console.ReadLine(),out var option);
            Console.Clear();
            switch (option)
            { 
                case 1:
                    ListItems();
                    break;
                case 2:
                    ListAvailableItems();
                    break;
                case 3:
                    ListBorrowedItems();
                    break;
                case 4:
                    AddItem();
                    break;
                case 5:
                    UpdateItem();
                    break;
                case 6:
                    DeleteItem();
                    break;
                case 7:
                    SearchItem();
                    break;
                case 8:
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            PauseAndClear();
        }while (true);
    }

    private void SearchItem()
    {
        Console.WriteLine("Search item");
        Console.Write("Id:");
        string id = Console.ReadLine()?.Trim()??string.Empty;
        Console.Write("Title:");
        string title = Console.ReadLine()?.Trim()??string.Empty;
        Console.Write("author:");
        string author = Console.ReadLine()?.Trim()??string.Empty;
        
        var listItem = _db.SearchLibraryItems(id, title, author);
        ShowItemsInfo(listItem.ToList());
    }

    private void ListBorrowedItems()
    {
        var list = _db.GetBorrowedLibaryItems();
        ShowItemsInfo(list.ToList());
    }

    private void ListAvailableItems()
    {
        var listItem = _db.GetAvailableLibaryItems();
        ShowItemsInfo(listItem.ToList());
    }

    private void ListItems()
    {
        var listItem = _db.GetLibraryItems();
        ShowItemsInfo(listItem.ToList());
        
    }
    

    private void DeleteItem()
    {
        Console.WriteLine("Delete item");
        Console.Write("Id:");
        Guid id = Guid.Parse(Console.ReadLine()?.Trim()??throw new Exception("Id is required"));
        if (!_db.IsInLibrary(id))
        {
            throw new Exception("Item not in library");
        }
        else
        {
            _db.DeleteLibraryItem(id);
            Console.WriteLine("delete complete!");
        }
    }

    private void UpdateItem()
    {
        Console.WriteLine("Update item");
        Console.Write("Id:");
        Guid id = Guid.Parse(Console.ReadLine()?.Trim()??throw new Exception("Id is required"));
        if (!_db.IsInLibrary(id))
        {
            throw new Exception("Item not in library");
        }
        else
        {
            var item = _db.GetLibraryItem(id);
            Console.WriteLine("Current item:");
            Console.WriteLine(item);
            var updatedItem = CreateLibraryItem();
            item.CopyFrom(updatedItem);
            switch (item)
            {
                case Book book:
                    Console.Write("Number of pages:");
                    int numberOfPages = int.Parse(Console.ReadLine()?.Trim()??"0");
                    book.SetNumberOfPages(numberOfPages);
                    break;
                case Dvd dvd:
                    Console.Write("Runtime:");
                    int runtime = int.Parse(Console.ReadLine()?.Trim()??"0");
                    dvd.SetRunTime(runtime);
                    break;
            }
            _db.UpdateLibraryItem(item);
            Console.WriteLine("update complete!");
        }
    }

    private void AddItem()
    {
        Console.WriteLine("Add item");
        var item =CreateItem();
        _db.AddLibraryItem(item);
        Console.WriteLine("Add complete!");
    }

    private LibraryItem CreateLibraryItem()
    {
        Console.Write("Title:");
        string title = Console.ReadLine()?.Trim()??throw new Exception("Title is required");
        Console.Write("Author:");
        string author = Console.ReadLine()?.Trim()??throw new Exception("Author is required");
        Console.Write("Publication date (dd/MM/yyyy): ");
        DateTime publicationDate = DateTime.ParseExact(Console.ReadLine()?.Trim()??throw new Exception("publicationDate is required"),
                                                        "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return new LibraryItem(title, author, publicationDate);
    }
    private LibraryItem CreateItem()
    {
        var item = CreateLibraryItem();
        ChoseType:
        Console.Write("Type(1.Book, 2.DVD, 3.Magazine):");
        int.TryParse(Console.ReadLine(), out var type);

        switch (type)
        {
            case 1:
                Console.Write("Number of pages:");
                int numberOfPages = int.Parse(Console.ReadLine()?.Trim()?? "0");
                item = new Book(item, numberOfPages);
                break;
            case 2:
                Console.Write("Runtime: ");
                int runtime = int.Parse(Console.ReadLine()?.Trim()?? "0");
                item = new Dvd(item, runtime);
                break;
            case 3:
                item = new Magazine(item);
                break;
            default:
                Console.Clear();
                goto ChoseType;
        }

            return item;

    }
}