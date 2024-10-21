// See https://aka.ms/new-console-template for more information

namespace MB_ex1;

internal class Program
{
    public static void Main(string[] args)
    {
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
            
            int.TryParse(Console.ReadLine(), out var option);
            switch (option)
            {
                case 1:
                    itemManagement.Option();
                    break;
                case 2:
                    borrowManagement.Option();
                    break;
                case 3:
                    searchManagement.Option();
                    break;
                case 4:
                    Console.WriteLine("Goodbye!");
                    //exit the program after 1 seconds
                    Thread.Sleep(1000);
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }
    }
}