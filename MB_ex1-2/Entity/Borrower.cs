namespace MB_ex1.Entity;

public class Borrower
{
    public string Name { get; private set; }
    public string Address { get; private set; }

    public int? LibraryCardNumber { get; private set; }

    public Borrower(string name, string address, int libraryCardNumber){
        Name = name;
        Address = address;
        LibraryCardNumber = libraryCardNumber;
    }

    public override string ToString()
    {
        return ("Name: " + Name + " Address: " + Address + " LibraryCardNumber: " + LibraryCardNumber);
    }
}
