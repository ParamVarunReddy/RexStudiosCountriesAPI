using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RexStudios.DynamicsCRM.Actions.Modals;

namespace RexStudios.DynamicsCRM.Actions
{
    interface ICountryService
    {
        // <summary>
        /// Get country By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Countries> GetCountryByName(string name);

    }
}
