using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using URLShortener.DataAccessLayer.DBContext;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.DataAccessLayer.JWT;
using URLShortener.DataAccessLayer.Repositories;
using URLShortener.DataAccessLayer.Roles;
using URLShortener.DataAccessLayer.UOW;

namespace URLShortener
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            builder.Services.AddDbContext<AppDBContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("StrConnection"));
            });

            builder.Services.AddIdentityCore<Account>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
            })
                .AddSignInManager<SignInManager<Account>>()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDBContext>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(l =>
            {
                l.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            builder.Services.AddScoped<JWTService>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IShortURLRepository, ShortURLRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseCors(t => t.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<IUnitOfWork>();
                if (service.CanConnect)
                {
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Account>>();

                    if(await service._db.AboutContent.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == 1) == null)
                    {
                        service._db.AboutContent.Add(new AboutContent
                        {
                            Id = 0,
                            Content = "The algorithm was created for shortening URLs. " +
                            "First thing to do is to get a URL-address from client. The it will be separated to ascii-bytes and summarized with current date ticks. " +
                            "The last action ensures that new path will be unique. " +
                            "The algorithm then returns a new string, a new URL to be precise. " +
                            "Redirect to that new address will make a call to the URLShortener API then and the Angular will redirect user to a URL origin address. " +
                            "If the URL was never shortened, a home page will be opened."
                        });
                        await service._db.SaveChangesAsync();
                    }
                    foreach (var item in RoleHandler.GetAllRoles())
                    {
                        if (!await roleManager.RoleExistsAsync(item.ToString()))
                        {
                            await roleManager.CreateAsync(new IdentityRole(item.ToString()));
                        }
                    }
                    var Result = await service.Accounts.GetByUsername(builder.Configuration["AdminDefaults:Username"]);
                    if (Result.Data is null)
                    {
                        string adminPassword = builder.Configuration["AdminDefaults:Password"];

                        Account admin = new Account()
                        {
                            UserName = builder.Configuration["AdminDefaults:Username"],
                        };

                        await service.Accounts.Insert(admin, builder.Configuration["AdminDefaults:Password"], "Admin");
                    }
                }
            };

            app.Run();
        }
    }
}