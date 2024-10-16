namespace MB_ex1.Entity;

public class Magazine: LibraryItem
{
    public Magazine( string title, string author, DateTime publicationDate): base( title, author, publicationDate){}
    public override string ToString()
    {
        return base.ToString()+" Type: Magazine";
    }
}
