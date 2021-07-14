using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Awatable
{
    interface IRepository
    {
        Task CleanData();
    }

    class RepositorySync : IRepository
    {
        public Task CleanData()
        {
            // operaciones sincronas
            return Task.CompletedTask;
        }
    }

    class RepositoryAsync : IRepository
    {
        public async Task CleanData()
        {
            // operaciones asincronas
            await Task.CompletedTask;
        }
    }
}
