using Microsoft.EntityFrameworkCore;
namespace assignment_crudelicious_03.Models
{ 
    public class MyContext : DbContext 
    { 
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<Dish> Dishes { get; set; }
    }
}