using Microsoft.EntityFrameworkCore;

namespace Talent;

public partial class RestaurantContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(AppConfig.ConnectionString);
    }

}
