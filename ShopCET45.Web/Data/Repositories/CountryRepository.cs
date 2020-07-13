using ShopCET45.Web.Data.Entities;

namespace ShopCET45.Web.Data.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(DataContext context) : base(context)
        {

        }
    }
}
