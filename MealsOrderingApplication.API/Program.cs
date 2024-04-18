
using DrinksOrderingApplication.Domain.Interfaces.Validations.DrinkValidation;
using DrinksOrderingApplication.Services.Validation.DrinkValidation;
using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces;
using MealsOrderingApplication.Domain.Interfaces.Validations.AdminValidation;
using MealsOrderingApplication.Domain.Interfaces.Validations.ApplicationUserValidation;
using MealsOrderingApplication.Domain.Interfaces.Validations.CustomerValidation;
using MealsOrderingApplication.Domain.Interfaces.Validations.MealValidation;
using MealsOrderingApplication.Domain.Interfaces.Validations.OrderValidation;
using MealsOrderingApplication.Domain.Interfaces.Validations.ProductValidation;
using MealsOrderingApplication.Domain.Interfaces.Validations.ReviewValidation;
using MealsOrderingApplication.Services;
using MealsOrderingApplication.Services.Helpers;
using MealsOrderingApplication.Services.IServices;
using MealsOrderingApplication.Services.Repositories;
using MealsOrderingApplication.Services.Services;
using MealsOrderingApplication.Services.Validation.AdminValidation;
using MealsOrderingApplication.Services.Validation.ApplicationUserValidation;
using MealsOrderingApplication.Services.Validation.CustomerValidation;
using MealsOrderingApplication.Services.Validation.MealValidation;
using MealsOrderingApplication.Services.Validation.OrderValidation;
using MealsOrderingApplication.Services.Validation.ProductValidation;
using MealsOrderingApplication.Services.Validation.ReviewValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MealsOrderingApplication.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add DbContext Configurations
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Add Unit Of Work Repository
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add Repository Services
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IMealRepository, MealRepository>();
            builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            // Validation Services
            builder.Services.AddScoped<IAddApplicationUserValidation, AddApplicationUserValidation>();
            builder.Services.AddScoped<IUpdateApplicationUserValidation, UpdateApplicationUserValidation>();

            builder.Services.AddScoped<IAddAdminValidation, AddAdminValidation>();
            builder.Services.AddScoped<IUpdateAdminValidation, UpdateAdminValidation>();

            builder.Services.AddScoped<IAddCustomerValidation, AddCustomerValidation>();
            builder.Services.AddScoped<IUpdateCustomerValidation, UpdateCustomerValidation>();

            builder.Services.AddScoped<IAddProductValidation, AddProductValidation>();
            builder.Services.AddScoped<IUpdateProductValidation, UpdateProductValidation>();

            builder.Services.AddScoped<IAddMealValidation, AddMealValidation>();
            builder.Services.AddScoped<IUpdateMealValidation, UpdateMealValidation>();

            builder.Services.AddScoped<IAddDrinkValidation, AddDrinkValidation>();
            builder.Services.AddScoped<IUpdateDrinkValidation, UpdateDrinkValidation>();

            builder.Services.AddScoped<IAddOrderValidation, AddOrderValidation>();
            builder.Services.AddScoped<IUpdateOrderValidation, UpdateOrderValidation>();

            builder.Services.AddScoped<IAddReviewValidation, AddReviewValidation>();
            builder.Services.AddScoped<IUpdateReviewValidation, UpdateReviewValidation>();

            builder.Services.AddScoped<JWTToken, JWTToken>();


            // Add Identity Config
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddDefaultTokenProviders();

            // Add JWT Config
            builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JWT"));
            builder.Services.AddSingleton<JWTConfig>();

            // Add Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // TODO: Allow Https in Production
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? "")),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Add Authorization
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
