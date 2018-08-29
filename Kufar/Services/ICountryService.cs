using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kufar.Services
{
    public interface ICountryService
    {
        /// <summary>
        /// Delete Country with dependencies
        /// </summary>
        /// <param name="Id">Country Id</param>
        /// <returns>async task</returns>
        Task DeleteCountryAsync(int Id);
    }
}
