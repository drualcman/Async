using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Awatable
{
    interface IServices
    {
        Task<int> GetIntId();
    }

    class ServicesSync : IServices
    {
        Task<int> IServices.GetIntId()
        {
            int Id = new Random().Next(1000);
            return Task.FromResult(Id);
        }
    }

    class ServicesAsync : IServices
    {
        public async Task<int> GetIntId()
        {
            int Id = new Random().Next(1000);
            await Task.Delay(1);
            return Id;
        }
    }

    class TestServices
    {
        public async void Test()
        {
            IServices services = new ServicesSync();
            int syndId = await services.GetIntId();

            services = new ServicesAsync();
            int asyndId = await services.GetIntId();
        }
    }
}
