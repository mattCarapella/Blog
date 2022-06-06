using Blog.Core.Repositories;
using Blog.Data;
using Blog.Models;
using Blog.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("BlogContextConnection") 
//    ?? throw new InvalidOperationException("Connection string not found.");

var connectionString = builder.Configuration["ConnectionStrings:BlogContextConnectionDev"]
    ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => 
    options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<BlogContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

AddScoped();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        //var context = services.GetRequiredService<BlogContext>();
        //var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        //var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //await ContextSeed.SeedRolesAsync(userManager, roleManager);
        //await ContextSeed.SeedAdminUser(userManager, roleManager);
        //await ContextSeed.SeedTestUsers(userManager, roleManager);
        // await ContextSeed.SeedDb(context);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();


void AddScoped()
{
    //builder.Services.AddTransient(typeof(IBlogRepository<>), typeof(BlogRepository<>));
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped(typeof(IBlogRepository<>), typeof(BlogRepository<>));
    builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
    builder.Services.AddScoped<IBlogImageRepository, BlogImageRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<ITagRepository, TagRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
}









// ***** The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
