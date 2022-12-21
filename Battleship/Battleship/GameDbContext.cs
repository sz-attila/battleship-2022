using Microsoft.EntityFrameworkCore;


namespace Battleship
{
    public class GameDbContext : DbContext
    {
        public GameDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB; Database = ServerDb; Integrated Security=True;");
            }
        }

        public DbSet<Game> Games { get; set; }
    }
}