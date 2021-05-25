using Microsoft.EntityFrameworkCore;
using XSchool.Domain.App.Users;

namespace XSchool.EntityFrameworkCore.DbContexts
{
    public class XSchoolDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public XSchoolDbContext(DbContextOptions<XSchoolDbContext> options) : base(options) { }
    }
}
