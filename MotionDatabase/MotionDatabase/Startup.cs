using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MotionDatabase.Helpers;
using MotionDatabase.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;

namespace MotionDatabase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MotionsContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            // JWT
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var settings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(settings.JWTSecret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers();
        }

        // Used to configure a development database instance form scratch
        public void ConfigureDevDatabase(MotionsContext db)
        {
            var hasher = new PasswordHasher<User>();

            var testUser = new User
            {
                Username = "test",
                Email = "test@example.com",
                IsAdmin = true,
                IsModerator = true,
                IsConfirmed = true
            };
            testUser.PasswordHash = hasher.HashPassword(testUser, "test");
            db.Users.Add(testUser);

            var testUser2 = new User
            {
                Username = "test2",
                Email = "test2@example.com",
                IsAdmin = false,
                IsModerator = false,
                IsConfirmed = true
            };
            testUser.PasswordHash = hasher.HashPassword(testUser, "test");
            db.Users.Add(testUser);
            db.Users.Add(testUser2);

            var cat1 = new MotionCategory
            {
                Name = "International Relations",
                Description = "Motions that discuss relations between nation states."
            };
            var cat2 = new MotionCategory
            {
                Name = "Politics",
                Description = "Motions that discuss (non-IR) politics."
            };
            db.MotionCategories.Add(cat1);
            db.MotionCategories.Add(cat2);

            var tag1 = new MotionTag
            {
                Name = "Trial Tag"
            };
            db.MotionTags.Add(tag1);

            db.Motions.Add(new Motion
            {
                MotionText = "Motion 1",
                Category = cat1,
                IsExplicit = false,
                State = MotionState.Approved,
                Difficulty = MotionDifficulty.Novice
            });

            db.SaveChanges();
            db.Motions.Find(1).AddTag(tag1);

            db.SaveChanges();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;

                using var serviceScope = app.ApplicationServices.CreateScope();

                var db = serviceScope.ServiceProvider.GetService<MotionsContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                ConfigureDevDatabase(db);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
