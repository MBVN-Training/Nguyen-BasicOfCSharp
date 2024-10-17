using MB_ex1.Entity;

namespace MB_ex1;

public class ItemManagement: Management
{
    
    public ItemManagement(){}

     public override void ShowMenu()
    {
        Console.Clear();
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
            int option = int.Parse(Console.ReadLine().Trim());
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
        Console.Clear();
        Console.WriteLine("Search item");
        Console.Write("Id:");
        string id = Console.ReadLine().Trim();
        Console.Write("Title:");
        string title = Console.ReadLine().Trim();
        Console.Write("author:");
        string author = Console.ReadLine().Trim();
        
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
        Console.Clear();
        var listItem = _db.GetLibraryItems();
        ShowItemsInfo(listItem.ToList());
        
    }
    

    private void DeleteItem()
    {
        Console.Clear();
        Console.WriteLine("Delete item");
        Console.Write("Id:");
        Guid id = Guid.Parse(Console.ReadLine().Trim());
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
        Console.Clear();
        Console.WriteLine("Update item");
        Console.Write("Id:");
        Guid id = Guid.Parse(Console.ReadLine().Trim());
        if (!_db.IsInLibrary(id))
        {
            throw new Exception("Item not in library");
        }
        else
        {
            var item = CreateItem("update");
            _db.UpdateLibraryItem(item);
            Console.WriteLine("update complete!");
        }
    }

    private void AddItem()
    {
        Console.Clear();
        Console.WriteLine("Add item");
        var item =CreateItem();
        _db.AddLibraryItem(item);
        Console.WriteLine("Add complete!");
    }

    private LibraryItem CreateItem(string action = "add")
    {
        Console.Write("Title:");
        string title = Console.ReadLine().Trim();
        Console.Write("Author:");
        string author = Console.ReadLine().Trim();
        Console.Write("Publication date:");
        DateTime publicationDate = DateTime.Now;
        try
        {
            publicationDate = DateTime.Parse(Console.ReadLine().Trim());
        }
        catch (Exception e)
        {
            throw new Exception("Invalid date");
        }
        if(action == "add")
        {
            ChoseType:
            Console.Write("Type(1.Book, 2.DVD, 3.Magazine):");
            int type = int.Parse(Console.ReadLine().Trim());
            LibraryItem item = null;
            
            switch (type)
            {
                case 1:
                    Console.Write("Number of pages:");
                    int numberOfPages = int.Parse(Console.ReadLine().Trim());
                    item = new Book(title, author, publicationDate, numberOfPages);
                    break;
                case 2:
                    Console.Write("Runtime: ");
                    int runtime = int.Parse(Console.ReadLine().Trim());
                    item = new Dvd(title, author, publicationDate, runtime);
                    break;
                case 3:
                    item = new Magazine(title, author, publicationDate);
                    break;
                default:
                    Console.Clear();
                    goto ChoseType;
            }

            return item;
        }
        return new LibraryItem( title, author, publicationDate);
    }
}