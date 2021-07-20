using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Multihilo.Lock
{
    class Product
    {
        int UnitsInStock;
        public Product(int unitsInStock)
        {
            UnitsInStock = unitsInStock;
        }

        // solo sirve para ser bloqueado
        object ObjectToLock = new object();

        /// <summary>
        /// Bloqueo de exclusion mutua (mutual-exclution)
        /// </summary>
        /// <param name="requestedUnits"></param>
        /// <returns></returns>
        public bool PlaceOrder(int requestedUnits)
        {
            bool acepted = false;
            if (UnitsInStock < 0)
            {
                throw new Exception("La existencia no puede ser negativa");
            }
            // lock necesita de una variable privada tipo objecto para bloquear el codigo
            lock (ObjectToLock)
            {
                // esta es la seccion critica del codigo que necesitamos bloquear
                if (UnitsInStock >= requestedUnits)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"Existencia antes del pedido: {UnitsInStock}");
                    UnitsInStock -= requestedUnits;
                    Console.WriteLine($"Existencia despues del pedido: {UnitsInStock}");
                    acepted = true;
                }
                else
                {
                    Console.WriteLine($"Existencia insuficiente: {requestedUnits} de {UnitsInStock}");
                }                
            }
            return acepted;
        }

        /// <summary>
        /// Bloqueo de exclusion mutua (mutual-exclution)
        /// </summary>
        /// <param name="requestedUnits"></param>
        /// <returns></returns>
        public bool PlaceOrderWithoutLock(int requestedUnits)
        {
            bool acepted = false;
            if (UnitsInStock < 0)
            {
                throw new Exception("La existencia no puede ser negativa");
            }
            // lock necesita de una variable privada tipo objecto para bloquear el codigo
            Monitor.Enter(ObjectToLock);
            try
            {
                // esta es la seccion critica del codigo que necesitamos bloquear
                if (UnitsInStock >= requestedUnits)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"Existencia antes del pedido: {UnitsInStock}");
                    UnitsInStock -= requestedUnits;
                    Console.WriteLine($"Existencia despues del pedido: {UnitsInStock}");
                    acepted = true;
                }
                else
                {
                    Console.WriteLine($"Existencia insuficiente: {requestedUnits} de {UnitsInStock}");
                }
            }
            finally
            {
                Monitor.Exit(ObjectToLock);
            }
            return acepted;
        }
    }
}
