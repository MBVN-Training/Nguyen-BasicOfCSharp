namespace MB_ex1.Entity;

public class Book: LibraryItem
{
    public int NumberOfPages { get; private set; }
    public Book( string title, string author, DateTime publicationDate,int numberOfPages): base( title, author, publicationDate){
        NumberOfPages = numberOfPages;
    }

    public override string ToString()
    {
        return base.ToString() + " Number of pages: " + NumberOfPages+" Type: Book";
    }
}