using InterviewPrepApi.Model;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrepApi
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<EmployeeRequest> EmployeeRequest { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("tblUser"); 
            modelBuilder.Entity<EmployeeRequest>().ToTable("tblEmploye").HasNoKey();
        }
    }
}
