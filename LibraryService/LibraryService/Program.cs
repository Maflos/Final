using System;
using System.ServiceModel;

namespace LibraryService
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryService wcfService = new LibraryService();

            using (ServiceHost host = new ServiceHost(wcfService))
            {
                host.Open();
                Console.WriteLine("Server is open!");
                Console.WriteLine("Press enter to close server!");
                Console.ReadLine();
            }
        }
    }
}
