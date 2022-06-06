using Blog.Core;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using static Blog.Core.Enums;

namespace Blog.Data;

public static class ContextSeed
{
    public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager,
                                            RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Editor.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
    }


    public static async Task SeedAdminUser(UserManager<ApplicationUser> userManager,
                                           RoleManager<IdentityRole> roleManager)
    {
        var defaultUser = new ApplicationUser
        {
            UserName = "MattC",
            Email = "admin@mattcarapella.com",
            FirstName = "Matt",
            LastName = "Carapella",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user is null)
            {
                await userManager.CreateAsync(defaultUser, "Pa$$w0rd1");
                await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.Editor.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
            }
        }
    }


    public static async Task SeedTestUsers(UserManager<ApplicationUser> userManager,
                                            RoleManager<IdentityRole> roleManager)
    {
        var user1 = new ApplicationUser
        {
            UserName = "SaulG",
            Email = "saul@test.com",
            FirstName = "Saul",
            LastName = "Goodman",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
        };
        if (userManager.Users.All(u => u.Id != user1.Id))
        {
            var user = await userManager.FindByEmailAsync(user1.Email);
            if (user is null)
            {
                await userManager.CreateAsync(user1, "Pa$$w0rd1");
                await userManager.AddToRoleAsync(user1, Roles.User.ToString());
            }
        }

        var user2 = new ApplicationUser
        {
            UserName = "Yan1",
            Email = "yan@test.com",
            FirstName = "Yuxing",
            LastName = "Yan",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
        };
        if (userManager.Users.All(u => u.Id != user2.Id))
        {
            var user = await userManager.FindByEmailAsync(user2.Email);
            if (user is null)
            {
                await userManager.CreateAsync(user2, "Pa$$w0rd1");
                await userManager.AddToRoleAsync(user2, Roles.Editor.ToString());
                await userManager.AddToRoleAsync(user2, Roles.User.ToString());
            }
        }
    }


    public static async Task SeedDb(BlogContext context)
    {
        if (context.Articles.Any()) return;
        if (context.Categories.Any()) return;
        if (context.Tags.Any()) return;

        var categories = new Category[]
        {
            new Category { Name = ".NET" },
            new Category { Name = "C#" },
            new Category { Name = "Python" },
            new Category { Name = "Javascript" },
            new Category { Name = "Software Design" },
            new Category { Name = "DevOps" }
        };

        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();

        var tags = new Tag[]
        {
            new Tag { Name="MVC" },
            new Tag { Name="Razor Pages" },
            new Tag { Name="Blazor" },
            new Tag { Name="React" },
            new Tag { Name="Design Patterns" },
            new Tag { Name="CQRS" },
            new Tag { Name="Repository" },
            new Tag { Name="UoW" },
            new Tag { Name="Azure" },
            new Tag { Name="AWS" },
        };

        context.Tags.AddRange(tags);
        await context.SaveChangesAsync();

        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var id3 = Guid.NewGuid();
        var id4 = Guid.NewGuid();

        var articles = new Article[]
        {
            new Article
            {
                Id = id1,
                Title = "Entity Framework: Efficient Database Queries",
                Entry = "Varius duis at consectetur lorem. Id diam vel quam elementum pulvinar etiam non. In fermentum et sollicitudin ac.",
                Level = Enums.Level.Intermediate,
                AuthorId = "3f54264c-9604-4a77-ab10-5bf5ae87953b"
            },
            new Article
            {
                Id = id2,
                Title = "Nullable Reference Types in C#",
                Entry = "Vitae auctor eu augue ut lectus arcu bibendum. Mi eget mauris pharetra et ultrices neque massa tempor nec.",
                Level = Enums.Level.Beginner,
                AuthorId = "3f54264c-9604-4a77-ab10-5bf5ae87953b"
            },
            new Article
            {
                Id = id3,
                Title = "Overposting Prevention in ASP.NET Core",
                Entry = "Varius duis at consectetur lorem. Id diam vel quam elementum pulvinar etiam non. In fermentum et sollicitudin",
                Level = Enums.Level.Advanced,
                AuthorId = "3f54264c-9604-4a77-ab10-5bf5ae87953b"
            },
            new Article
            {
                Id = id4,
                Title = "Design Patterns: CQRS in ASP.NET Core Web API",
                Entry = "Varius duis at consectetur lorem. Id diam vel quam elementum pulvinar etiam non. In fermentum et sollicitudin",
                Level = Enums.Level.Advanced,
                AuthorId = "3f54264c-9604-4a77-ab10-5bf5ae87953b"
            }

        };

        context.Articles.AddRange(articles);
        await context.SaveChangesAsync();
    }


}





//,
//            new Article { Title="Article 4", Entry="Arcu cursus vitae congue mauris rhoncus. A diam sollicitudin tempor id eu nisl nunc mi. Blandit massa enim nec dui nunc mattis. Pretium lectus quam id leo.", Level=Enums.Level.Advanced },
//            new Article { Title = "Article 5", Entry = "Pulvinar elementum integer enim neque. Purus faucibus ornare suspendisse sed. aliquam. Varius morbi enim nunc faucibus a pellentesque sit amet." },
//            new Article { Title = "Article 6", Entry = "Sed faucibus turpis in eu. Lorem donec massa sapien faucibus et. Nibh viverra mauris in. Eget est lorem ipsum dolor sit amet consectetur. Magna ac placerat vestibulum lectus mauris.", Level = Enums.Level.Beginner }