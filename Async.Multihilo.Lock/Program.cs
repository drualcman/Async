using System;
using System.Threading.Tasks;

namespace Async.Multihilo.Lock
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Product product = new Product(1000);
            Random r = new Random();
            Parallel.For(0,100, i => 
            {
                //product.PlaceOrder(r.Next(1,100));
                product.PlaceOrderWithoutLock(r.Next(1,100));
            });
            Console.WriteLine("Presione <enter> para finalizar la aplicacion");
            Console.ReadLine();
        }
    }
}
