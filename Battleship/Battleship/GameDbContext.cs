using Microsoft.EntityFrameworkCore;


namespace Battleship
{
    /// <summary>
    /// A database context for the game database.
    /// </summary>
    public class GameDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the GameDbContext class.
        /// </summary>
        public GameDbContext()
        {
        }

        /// <summary>
        /// Configures the database context with the specified options.
        /// </summary>
        /// <param name="optionsBuilder">The options builder to use.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB; Database = ServerDb; Integrated Security=True;");
            }
        }

        /// <summary>
        /// A collection of games stored in the database.
        /// </summary>
        public DbSet<Game> Games { get; set; }
    }
}