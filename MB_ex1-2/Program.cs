using MB_ex1;
// See https://aka.ms/new-console-template for more information

internal class Program
{
    public static void Main(string[] args)
    {
        var db = new Database();
        var itemManagement = new ItemManagement();
        var borrowManagement = new BorrowManagement();
        var searchManagement = new SearchManagement();
        while (true)
        {
            Console.WriteLine("Library Management System");
            Console.WriteLine("1. Item management");
            Console.WriteLine("2. Borrow management");
            Console.WriteLine("3. Search Management");
            Console.WriteLine("4. Exit");
            Console.Write("chose an option:");
            int option;
            try{
                option = int.Parse(Console.ReadLine().Trim());
                Console.Clear();
                switch (option)
                {
                    case 1:
                        itemManagement.ChoseOption();
                        break;
                    case 2:
                        borrowManagement.ChoseOption();
                        break;
                    case 3:
                        searchManagement.ChoseOption();
                        break;
                    case 4:
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
            finally{
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
