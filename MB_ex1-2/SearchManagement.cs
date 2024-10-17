namespace MB_ex1;
public class SearchManagement:Management
{
    public override void ShowMenu()
    {
        Console.WriteLine("1. Get all books");
        Console.WriteLine("2. sort book by title");
        Console.WriteLine("3. DVD publication in 2022");
        Console.WriteLine("4. Borrower borrow both book and DVD");
        Console.WriteLine("5. Exit");
    }

    public override void ChoseOption()
    {
        while (true)    
        {
            ShowMenu();
            Console.Write("Choose an option:");
            int option = int.Parse(Console.ReadLine().Trim());
            switch (option)
            {
                case 1:
                    GetAllBooks();
                    break;
                case 2:
                    SortBookByTitle();
                    break;
                case 3:
                    DVDPublicationIn2022();
                    break;
                case 4:
                    BorrowerBorrowBothBookAndDVD();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            PauseAndClear();
        }
    }

    private void BorrowerBorrowBothBookAndDVD()
    {
        var borrower = _db.GetBorrowersBorrowBothBookAndDvd();
        Console.WriteLine("Borrower borrow both book and DVD");
        Console.WriteLine("Number of borrowers: "+borrower.Count);
        ShowItemsInfo(borrower.Cast<object>().ToList());
    }

    private void DVDPublicationIn2022()
    {
        var items = _db.GetDVDPublicationIn2022();
        Console.WriteLine("DVD publication in 2022");
        Console.WriteLine("Number of DVD: "+items.Count);
        ShowItemsInfo(items.Cast<object>().ToList());
    }

    private void SortBookByTitle()
    {
        ShowItemsInfo(_db.GetAllBooks().OrderBy(item=>item.Title).Cast<object>().ToList());
    }

    private void GetAllBooks()
    {
        ShowItemsInfo(_db.GetAllBooks().OrderBy(item=>item.PublicationDate).Cast<object>().ToList());
    }
}