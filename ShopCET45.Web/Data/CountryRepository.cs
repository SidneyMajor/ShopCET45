using ShopCET45.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET45.Web.Data
{
    public class CountryRepository: GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(DataContext context ) : base(context)
        {

        }
    }
}
