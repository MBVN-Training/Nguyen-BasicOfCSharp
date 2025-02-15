using MB_ex1.Entity;

namespace MB_ex1;

public class Database
{
    private static Database? _instance;
    private List<LibraryItem> _libraryItems= new List<LibraryItem>();
    private List<Borrower> _borrowers = new List<Borrower>();
    private List<BorrowingHistory> _borrowingHistories = new List<BorrowingHistory>();
    private readonly Random _random = new Random();
    private Database(){
        SeedData();
    }
    public static Database GetInstance(){
        if(_instance is null){
            _instance = new Database();
            
        }
        return _instance;
    }
    public List<LibraryItem> GetLibraryItems(){
        return _libraryItems.OrderBy(item => item.GetType().Name).ThenBy(item => item.Id).ToList();
    }
    public LibraryItem GetLibraryItem(Guid id){
        return _libraryItems.First(item => item.Id == id);
    }
    public List<Book> GetAllBooks()
    {
        return _libraryItems.OfType<Book>().ToList();
    }
    public List<Dvd> GetDvdPublicationIn2022()
    {
        return _libraryItems.OfType<Dvd>().Where(item=> item.PublicationDate.Year == 2022).ToList();
    }
    public List<Borrower> GetBorrowersBorrowBothBookAndDvd()
    {
        // Create a set of borrowers who borrowed books
        var bookBorrowers = new HashSet<int?>(
            _borrowingHistories.Where(history => _libraryItems.OfType<Book>().Any(book => book.Id == history.IdItem))
                .Select(history => history.BorrowerLibraryCardNumber)
        );

        // Create a set of borrowers who borrowed DVDs
        var dvdBorrowers = new HashSet<int?>(
            _borrowingHistories.Where(history => _libraryItems.OfType<Dvd>().Any(dvd => dvd.Id == history.IdItem))
                .Select(history => history.BorrowerLibraryCardNumber)
        );

        // Intersect the two sets to get borrowers who borrowed both
        var commonBorrowers = bookBorrowers.Intersect(dvdBorrowers);

        // Return the borrowers who borrowed both books and DVDs
        var result = _borrowers.Where(borrower => commonBorrowers.Contains(borrower.LibraryCardNumber)).ToList();

        return result;
    }
    public List<Borrower> GetBorrowers(){
        return _borrowers;
    }
    public List<BorrowingHistory> GetBorrowingHistories(){
        return _borrowingHistories;
    }
    public LibraryItem? GetItemInLibrary(Guid? id){
        if(id is null)
            return null;
        return _libraryItems.FirstOrDefault(item => item.Id == id);
    }
    public void AddLibraryItem(LibraryItem item){
        var itemInLibrary = GetItemInLibrary(item.Id);
        if (itemInLibrary is not null) throw new Exception("Item already exists in library");
        item.Return();
        _libraryItems.Add(item);
    }
    public void UpdateLibraryItem(LibraryItem item){
        var itemInLibrary = GetItemInLibrary(item.Id);
        if(itemInLibrary is null)
            AddLibraryItem(item);
        else
            itemInLibrary.CopyFrom(item);
    }
    public void DeleteLibraryItem(Guid id){
        if(!IsInLibrary(id))
            throw new Exception("Item not in library");
        _libraryItems.RemoveAll(item => item.Id == id);
    }
    public bool IsInLibrary(Guid id){
        return _libraryItems.Exists(item => item.Id == id);
    }
    public void BorrowItem(Guid idItem, int borrowerLibraryCardNumber, DateTime borrowDate){
        if(!_borrowers.Exists(borrower => borrower.LibraryCardNumber == borrowerLibraryCardNumber))
            throw new Exception("Borrower not found");
        var item = GetItemInLibrary(idItem);
        if (item is null )
        {
            throw new Exception("item not found");
        }
        if(item.IsBorrowed){
            throw new Exception("Item already borrowed");
        }
        item.Borrow();
        _borrowingHistories.Add(new BorrowingHistory( idItem, borrowerLibraryCardNumber, borrowDate));
    }
    public void ReturnItem(Guid idItem, int borrowerLibraryCardNumber, DateTime returnDate){
        var item = GetItemInLibrary(idItem);
        if(item is null){
            throw new Exception("Item not found");
        }
        if(!item.IsBorrowed){
            throw new Exception("Item not borrowed");
        }
        var history = _borrowingHistories.Find(history => history.IdItem == idItem && history.BorrowerLibraryCardNumber == borrowerLibraryCardNumber);
        if(history is null){
            throw new Exception("No borrowing history found");
        }
        item.Return();
        history.ReturnItem(returnDate);
    }
    public List<LibraryItem> GetAvailableLibraryItems(){
        return _libraryItems.FindAll(item => !item.IsBorrowed);
    }
    public List<LibraryItem> GetBorrowedLibraryItems(){
        return _libraryItems.FindAll(item => item.IsBorrowed);
    }
    public List<LibraryItem> SearchLibraryItems(string id,string title, string author)
    {
        return _libraryItems.FindAll(item=> item.Title.Contains(title)|| item.Author.Contains(author) || (item.Id?.ToString() ?? "").Contains(id));
    }
////seed data
    private void SeedData(){
        int numberOfItems = 10;
        int numberOfBorrowers = 5;
        for(int i = 0; i < numberOfItems; i++){
            int index =_libraryItems.Count+1;
            DateTime date = new DateTime(2022, 1, 1);
            date = date.AddDays(_random.Next(-50, 50));
            _libraryItems.Add(new Book("Title"+index, "Author"+index,date , _random.Next(50,300)));
            _libraryItems.Add(new Dvd( "Title"+index+1, "Author"+index+1,date , _random.Next(50,300)));
            _libraryItems.Add(new Magazine( "Title"+index+2, "Author"+index+2,date ));
        }

        for (int i = 0; i < numberOfBorrowers; i++)
        {
            _borrowers.Add(new Borrower("Name" + i, "Address" + i, _random.Next(1111,9999)));
        }

        foreach (var item in _libraryItems)
        {
            if (item.IsBorrowed)
            {
                _borrowingHistories.Add(new BorrowingHistory(item.Id, _borrowers[_random.Next(0,_borrowers.Count)].LibraryCardNumber, DateTime.Now.AddDays(_random.Next(-50,50))));
            }
        }
    }
}