namespace MB_ex1.Entity;

public class Dvd: LibraryItem
{
    public int RunTime { get; private set; }
    public Dvd( string title, string author, DateTime publicationDate, int runTime): base( title, author, publicationDate){
        RunTime = runTime;
    }
    public Dvd (LibraryItem item, int runTime): base(item.Title, item.Author, item.PublicationDate){
        RunTime = runTime;
    }
    public void SetRunTime(int runTime){
        RunTime = runTime;
    }

    public override string ToString()
    {
        return base.ToString() + " RunTime: " + RunTime;
    }
}