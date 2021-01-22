using Microsoft.EntityFrameworkCore;

namespace Angular.Api.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Student> Students {get;set;}
    }
}