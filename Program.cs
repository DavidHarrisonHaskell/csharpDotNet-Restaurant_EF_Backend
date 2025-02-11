using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;

namespace Talent;

public class Program
{
    /*
        Install-Package Microsoft.EntityFrameworkCore.Design
        Install-Package Microsoft.EntityFrameworkCore.Tools
        Install-Package Microsoft.EntityFrameworkCore.SqlServer
        Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 8.0.0
        Scaffold-DbContext -Provider Microsoft.EntityFrameworkCore.SqlServer -Connection "Server=.\SqlExpress;Database=Restaurant;Trusted_Connection=True;TrustServerCertificate=True" -OutputDir 3-Models/EF -Namespace Talent -DataAnnotations -Force
    */

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        builder.Services.AddScoped<DishService>();
        builder.Services.AddScoped<OrderService>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddMvc(options => options.Filters.Add<CatchAllFilter>()); // for every controller, add the CatchAllFilter
        builder.Services.AddDbContext<RestaurantContext>();

        // Don't check validation errors using built-in filter, we're going to check them:
        builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtHelper.SetBearerOptions); // use JWT as the authentication mechanism

        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles) // stops endless loop when bringing shows
            .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull); // doesn't include null values

        var app = builder.Build();

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
