using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NonogramPuzzle.Models;

namespace NonogramPuzzle
{
  class Program
  {
    static void Main(string[] args)
    {

      WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
      builder.Services.AddControllersWithViews();

      builder.Services.AddDbContext<NonogramPuzzleContext>(
        dbContextOptions => dbContextOptions
        .UseMySql(
          builder.Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(
            builder.Configuration["ConnectionStrings:DefaultConnection"]
          )
        )
      );

      WebApplication app = builder.Build();

      
      // app.UseDeveloperExceptionPage();
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
      );

      app.Run();
    }
  }
}

// depreciated program builder code:
// // Add services to the container.
// builder.Services.AddControllersWithViews();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseRouting();

// app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

// app.Run();


// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;

// using Puzzle.Models;


// namespace Puzzle
// {
//   class Program
//   {
//     static void Main(string[] args)
//     {

//       WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//       builder.Services.AddControllersWithViews();

//       builder.Services.AddDbContext<NonogramContext>(
//         dbContextOptions => dbContextOptions
//         .UseMySql(
//           builder.Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"]
//           )
//         )
//       );

//       builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//       .AddEntityFrameworkStores<NonogramContext>()
//       .AddDefaultTokenProviders();

//       builder.Services.Configure<IdentityOptions>(options => 
//       {
//         options.Password.RequireDigit = false;
//         options.Password.RequireLowercase = false;
//         options.Password.RequireNonAlphanumeric = false;
//         options.Password.RequireUppercase = false;
//         options.Password.RequiredLength = 0;
//         options.Password.RequiredUniqueChars = 0;
//       });

//       WebApplication app = builder.Build();

//       // app.UseDeveloperExceptionPage();
//       app.UseHttpsRedirection();
//       app.UseStaticFiles();

//       app.UseRouting();

//       app.UseAuthentication();
//       app.UseAuthorization();

//       app.MapControllerRoute(
//         name: "default",
//         pattern: "{controller=Home}/{action=Index}/{id?}");

//       app.Run();
//     }
//   }
// }