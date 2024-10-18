namespace MB_ex1.Entity;

public class BorrowingHistory
{
    private Guid? Id { get; } = Guid.NewGuid();
    public int? BorrowerLibraryCardNumber{ get; private set; } 
    public DateTime? BorrowDate{ get; private set; }
    public DateTime? ReturnDate{ get; private set; }
    public Guid? IdItem{ get; private set; }
    public BorrowingHistory( Guid idItem,int borrowerLibraryCardNumber, DateTime borrowDate){
        IdItem = idItem;
        BorrowerLibraryCardNumber = borrowerLibraryCardNumber;
        BorrowDate = borrowDate;
    }
    public void ReturnItem(DateTime returnDate){
        ReturnDate = returnDate;
    }

    public override string ToString()
    {
        return ("BorrowHistoryID: "+Id+" BorrowerLibraryCardNumber: "+BorrowerLibraryCardNumber+" BorrowDate: "+BorrowDate?.ToString("dd/MM/yyyy")+" ReturnDate: "+ReturnDate?.ToString("dd/MM/yyyy")+" IdItem: "+IdItem);
    }
}