using NorthWind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Paralel
{
    class ProductDTO: Product
    {
        public ProductDTO()
        {
            Thread.Sleep(100);
        }
    }
}
