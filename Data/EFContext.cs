using Microsoft.EntityFrameworkCore;

namespace CodeStresmAspNetCoreApiStarter.Data
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }

    }
}