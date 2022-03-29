using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ChatApp.Data
{
    public class DataContext: IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
