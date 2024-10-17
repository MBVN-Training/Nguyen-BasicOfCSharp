namespace MB_ex1.Entity;

public class LibraryItem
{
    private Random _random = new Random();
    public Guid? Id { get; private set; }
    public string Title { get; private set; }
    public string Author{ get; private set; }
    public DateTime PublicationDate{ get; private set; }
    public bool IsBorrowed { get; private set; }=false ;
    
    public LibraryItem(string title, string author, DateTime publicationDate){
        Id = Guid.NewGuid();
        Title = title;
        Author = author;
        PublicationDate = publicationDate;
        IsBorrowed = _random.Next(0, 3) != 0;

    }

    public override string ToString()
    {
        return ("Id: " + Id + " Title: " + Title + " Author: " + Author + " Publication date: " + PublicationDate);
    }
    
    // Method to copy properties from another LibraryItem
    public void CopyFrom(LibraryItem other){
        Title = other.Title;
        Author = other.Author;
        PublicationDate = other.PublicationDate;
    }
    public void Borrow(){
        IsBorrowed = true;
    }
    public void Return(){
        IsBorrowed = false;
    }
}