using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aync.ValueTask
{
    class ValueTaskDemo
    {
        // Necesita mas manejo de memoria porque es tipo referencia y el garbage collector si no tiene
        // memoria necesita entrar en ejecucion
        public async Task<int> GetNewProductId()
        {
            // operaciones
            await Task.Delay(3000);

            // logica

            return 10;
        }

        // Requiere menos memoria porque es tipo valor y el valor se pasa directo por el stack
        public async ValueTask<int> GetAnotherNewProductId()
        {
            // operaciones
            await Task.Delay(3000);

            // logica

            return 10;
        }

        interface IRepository<T>
        {
            ValueTask<T> GetNew();
        }

        class RepositorySync<T> : IRepository<T>
        {
            public ValueTask<T> GetNew()
            {
                var value = default(T);

                // logica

                return new ValueTask<T>(value);
            }
        }
        class RepositoryAsync<T> : IRepository<T>
        {
            public async ValueTask<T> GetNew()
            {
                var value = default(T);

                // logica

                return await new ValueTask<T>(value);
            }
        }
    }
}
