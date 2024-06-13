using InterviewPrepApi.Model;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrepApi
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("tblUser"); // Replace "Users" with your actual table name
        }
    }
}
