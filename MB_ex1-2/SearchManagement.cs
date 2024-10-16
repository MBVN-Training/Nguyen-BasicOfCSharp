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
        throw new NotImplementedException();
    }

    private void DVDPublicationIn2022()
    {
        throw new NotImplementedException();
    }

    private void SortBookByTitle()
    {
        throw new NotImplementedException();
    }

    private void GetAllBooks()
    {
        ShowItemsInfo(_db.GetAllBooks().Cast<object>().ToList());
    }
}