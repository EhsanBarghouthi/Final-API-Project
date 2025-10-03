
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RDP_NTier_Task.BL.General_Services;
using RDP_NTier_Task.BL.OrderServices;
using RDP_NTier_Task.BL.PaymentServices;
using RDP_NTier_Task.BL.ReviewServices;
using RDP_NTier_Task.BL.ServicesRepository;
using RDP_NTier_Task.BL.ServicesRepository.Authentication_Services;
using RDP_NTier_Task.BL.ServicesRepository.BrandServices;
using RDP_NTier_Task.BL.ServicesRepository.CartServices;
using RDP_NTier_Task.BL.ServicesRepository.ProductServices;
using RDP_NTier_Task.BL.ServicesRepostry.ProductServices.Classes;
using RDP_NTier_Task.BL.ServicesRepostry.ProductServices.Interfaces;
using RDP_NTier_Task.BL.userServices;
using RDP_NTier_Task.DAL.DATA;
using RDP_NTier_Task.DAL.DTO.Configurations;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.BrandRepository;
using RDP_NTier_Task.DAL.Repostry.CartRepository;
using RDP_NTier_Task.DAL.Repostry.CategoryRepostry;
using RDP_NTier_Task.DAL.Repostry.GenericReporstry;
using RDP_NTier_Task.DAL.Repostry.OrderRepository;
using RDP_NTier_Task.DAL.Repostry.ProductRepository;
using RDP_NTier_Task.DAL.Repostry.ReviewRepository;
using RDP_NTier_Task.DAL.Seed_Data;
using Stripe;
using System.Text;
using FileService = RDP_NTier_Task.BL.General_Services.FileService;
//using RDP_NTier_Task.DAL.DTO.Configurations;

namespace RDP_NTier_Task.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //builder.Services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // project services : 

            // Get connection string from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register DbContext with DI
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));


            builder.Services.AddScoped<IAuthonticationServices, AuthenticationServices>();
            // for Repostiry : 
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ISeedData,SeedData>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductServices, ProductServices>();


            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IBrandServices, BrandServices>();

            //builder.Services.AddScoped(typeof(IProductServices), typeof(ProductServices));
            builder.Services.AddScoped<IFileService,FileService>();

            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICartServices, CartServices>();

            // for payment: 
            builder.Services.AddScoped<IPaymentServices, PaymentServices>();
            //builder.Services.AddScoped<ICartServices, CartServices>();

            // for success order : 
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderServices, OrderServices>();

            // for user Services : 
            builder.Services.AddScoped<IUserServices,UserServices>();

            // for Review : 
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IReviewServices,ReviewServices>();

            // it is contain all things for http request . (used to get id for user )
            builder.Services.AddHttpContextAccessor();

            //builder.Services.AddScoper<>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options=>
                options.User.RequireUniqueEmail = true // This enforces uniqueness
            
            )
               .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


            //for email: 
            // Bind EmailSettings
            builder.Services.Configure<EmailSettings>(
                builder.Configuration.GetSection("EmailSettings"));

            // Register Email Service
            builder.Services.AddScoped<IEmailSender, EmailSender>();

            // Add Authentication (JWT)
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                //ValidIssuer = builder.Configuration["Jwt:Issuer"],
                //ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };     
            });

            // end project services 

            builder.Services.AddSwaggerGen();

            // for Stripe Payment configuration : 
            // Configure Stripe settings
            builder.Services.Configure<StripeConfig>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];



            var app = builder.Build();

            //  Run the seeder here
            var scope = app.Services.CreateScope();
            {
                var seeder = scope.ServiceProvider.GetRequiredService<ISeedData>();
                await seeder.DataSeedAsync();
                await seeder.IdentitySeedAsync(); // if you add identity seeding later
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseStaticFiles();
            }
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseHttpsRedirection();

            app.UseAuthentication();   // Needed if you use Identity
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
