namespace DAL.Entities
{
    using DAL.Initializer;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GamesInCart> GamesInCart { get; set; }
        public DbSet<UserCart> UserCarts { get; set; }
        public DbSet<OrderDetails> OrdersDetails { get; set; }
        public ApplicationContext()
            : base("name=AppContext")
        {
            Database.SetInitializer(new GamesInitializer());
        }
    }
}