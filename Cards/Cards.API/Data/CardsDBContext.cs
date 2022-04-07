//buoc 2

using Cards.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Data
{
    public class CardsDBContext: DbContext
    {
        public CardsDBContext(DbContextOptions options) : base(options) {
        }

        //dbset
        public DbSet<Card> Cards { get; set; }
    }
}
