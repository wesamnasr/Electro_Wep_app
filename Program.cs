
using MY_API_PROJECT.Models;
using MY_API_PROJECT.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MY_API_PROJECT.Repositories.Interfaces;
using MY_API_PROJECT.Repositories;
using System.Net.Mail;
using MY_API_PROJECT.Interfaces;

namespace MY_API_PROJECT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            builder.Services.AddLogging(c =>
            {
                c.AddDebug();
            }); 

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("con"));
            });


            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));// Generic  Repo
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();





            // var attachment = builder.Configuration.GetSection("Attachment").Get<AttachmentOptions>();
            //  builder.Services.AddSingleton(attachment);



            //var AttachmentOptions = new AttachmentOptions();
            //builder.Configuration.GetSection("Attachment").Bind("AttachmentOptions");
            // builder.Services.AddSingleton(AttachmentOptions);



         //   builder.Services.Configure


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();





            // Configure JWT authentication
            
            builder.Services.AddAuthentication(options =>
            { 
                
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),


                    ValidateLifetime = true
                };
            });




            var app = builder.Build();


            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
