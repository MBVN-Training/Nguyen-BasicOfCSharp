namespace MB_ex1.Entity;

public class Magazine: LibraryItem
{
    public Magazine( string title, string author, DateTime publicationDate): base( title, author, publicationDate){}
    public Magazine(LibraryItem item): base(item.Title, item.Author, item.PublicationDate){}

}