using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Async.Awatable
{
    public static class Extensions
    {
        public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
        {
            return Task.Delay(timeSpan).GetAwaiter();
        }

        public static TaskAwaiter GetAwaiter(this Int32 miliseconds)
        {
            return TimeSpan.FromMilliseconds(miliseconds).GetAwaiter();
        }
    }
}
