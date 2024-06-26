using CronoManage.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CronoManage.Infraestructure.Data.Context
{
    public class CronoContext : DbContext
    {
        public CronoContext(DbContextOptions<CronoContext> options) : base(options)
        {
        }
        public DbSet<MyProject> Projects { get; set; }

    }
}
