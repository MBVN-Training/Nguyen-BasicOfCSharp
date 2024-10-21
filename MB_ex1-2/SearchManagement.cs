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
        Console.Write("Choose an option:");
    }

    public override void ChoseOption()
    {
        while (true)    
        {
            ShowMenu();
            int.TryParse(Console.ReadLine(), out var option);
            Console.Clear();
            switch (option)
            {
                case 1:
                    GetAllBooks();
                    break;
                case 2:
                    SortBookByTitle();
                    break;
                case 3:
                    DvdPublicationIn2022();
                    break;
                case 4:
                    BorrowerBorrowBothBookAndDvd();
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

    private void BorrowerBorrowBothBookAndDvd()
    {
        var borrower = Db.GetBorrowersBorrowBothBookAndDvd();
        Console.WriteLine("Borrower borrow both book and DVD");
        Console.WriteLine("Number of borrowers: "+borrower.Count);
        ShowItemsInfo(borrower.ToList());
    }

    private void DvdPublicationIn2022()
    {
        var items = Db.GetDvdPublicationIn2022();
        Console.WriteLine("DVD publication in 2022");
        Console.WriteLine("Number of DVD: "+items.Count);
        ShowItemsInfo(items.ToList());
    }

    private void SortBookByTitle()
    {
        ShowItemsInfo(Db.GetAllBooks().OrderBy(item=>item.Title).ToList());
    }

    private void GetAllBooks()
    {
        ShowItemsInfo(Db.GetAllBooks().OrderBy(item=>item.PublicationDate).ToList());
    }
}