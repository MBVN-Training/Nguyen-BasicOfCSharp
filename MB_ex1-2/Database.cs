using System;
using MB_ex1.Entity;
using Microsoft.VisualBasic.CompilerServices;

namespace MB_ex1;

public class Database
{
    private static Database? _instance;
    private List<LibraryItem> _libraryItems;
    private List<Borrower> _borrowers;
    private List<BorrowingHistory> _borrowingHistories;
    private Random _random = new Random();
    public Database(){
        SeedData();
    }
    public static Database? GetInstance(){
        if(_instance is null){
            _instance = new Database();
            
        }
        return _instance;
    }

    public List<LibraryItem> GetLibraryItems(){
        return _libraryItems;
    }

    public List<Book> GetAllBooks()
    {
        return _libraryItems.OfType<Book>().ToList();
    }

    public List<Dvd> GetDVDPublicationIn2022()
    {
        return _libraryItems.OfType<Dvd>().Where(item=> item.PublicationDate.Year == 2022).ToList();
    }

    public List<Borrower> GetBorrowersBorrowBothBookAndDvd()
    {
        //get borrower borrow book
        var BookBorrower= _libraryItems.OfType<Book>()
            .Join(_borrowingHistories, book => book.Id, history => history.IdItem,
                (book, history) => history.BorrowerLibraryCardNumber).Distinct();
        //get borrower borrow dvd
        var borrowDvd = _libraryItems.OfType<Dvd>().Join(_borrowingHistories, dvd => dvd.Id, history => history.IdItem, (dvd, history) => history.BorrowerLibraryCardNumber).Distinct();
        //return list of borrower borrow both book and dvd
        var result = BookBorrower.Intersect(borrowDvd).Join(_borrowers, borrower => borrower, borrower => borrower.LibraryCardNumber, (borrower, borrower1) => borrower1);
        return result.ToList();
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
        foreach(LibraryItem item in _libraryItems){
            if(item.Id == id){
                return item;
            }
        }
        return null;
    }
    public void AddLibraryItem(LibraryItem item){
        var itemInLibrary = GetItemInLibrary(item.Id);
        if (itemInLibrary is not null) throw new Exception("Item already exists in library");
        _libraryItems.Add(item);
        return;
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
        if(!IsInLibrary(idItem))
            throw new Exception("Item not in library");
        
        if(!_borrowers.Exists(borrower => borrower.LibraryCardNumber == borrowerLibraryCardNumber))
            throw new Exception("Borrower not found");
        else
        {
            var item = GetItemInLibrary(idItem);
            if(item.IsBorrowed){
                throw new Exception("Item already borrowed");
            }
            item.Borrow();
            _borrowingHistories.Add(new BorrowingHistory( idItem, borrowerLibraryCardNumber, borrowDate));
        }
    }
    public void ReturnItem(Guid idItem, int borrowerLibraryCardNumber, DateTime returnDate){
        if(!IsInLibrary(idItem))
            throw new Exception("Item not in library");
        var item = GetItemInLibrary(idItem);
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
    public List<LibraryItem> GetAvailableLibaryItems(){
        return _libraryItems.FindAll(item => !item.IsBorrowed);
    }
    public List<LibraryItem> GetBorrowedLibaryItems(){
        return _libraryItems.FindAll(item => item.IsBorrowed);
    }
    public List<LibraryItem> SearchLibraryItems(string id,string title, string author)
    {
        return _libraryItems.FindAll(item=> item.Title.Contains(title)|| item.Author.Contains(author) || item.Id.ToString().Contains(id));
    }
////seed data
    private void SeedData(){
        _libraryItems = new List<LibraryItem>();
        _borrowers = new List<Borrower>();
        _borrowingHistories = new List<BorrowingHistory>();
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
                _borrowingHistories.Add(new BorrowingHistory(item.Id.Value, _borrowers[_random.Next(0,_borrowers.Count)].LibraryCardNumber.Value, DateTime.Now.AddDays(_random.Next(-50,50))));
            }
        }
    }
}