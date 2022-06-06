using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Blog.Models; 

namespace Blog.Data;

public class BlogContext : IdentityDbContext<ApplicationUser>
{
    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<BlogImage> BlogImages => Set<BlogImage>();
    public DbSet<Tag> Tags => Set<Tag>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Identity");
        
        builder.Entity<ApplicationUser>().ToTable(name: "User");
        builder.Entity<IdentityRole>().ToTable(name: "Role");
        builder.Entity<IdentityUserRole<string>>().ToTable(name: "UserRoles");
        builder.Entity<IdentityUserClaim<string>>().ToTable(name: "UserClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable(name: "UserLogins");
        builder.Entity<IdentityRoleClaim<string>>().ToTable(name: "RoleClaims");
        builder.Entity<IdentityUserToken<string>>().ToTable(name: "UserTokens");
        builder.Entity<Article>().ToTable("Articles");
        builder.Entity<BlogImage>().ToTable("BlogImages");
        builder.Entity<Tag>().ToTable("Tags");
        builder.Entity<Category>().ToTable("Categories");


        builder.Entity<Article>()
            .HasMany(a => a.Tags)
            .WithMany(t => t.Articles);

        builder.Entity<Article>()
            .HasMany(a => a.Categories)
            .WithMany(c => c.Articles);

        builder.Entity<Article>()
            .HasOne(a => a.Author)
            .WithMany(u => u.Articles)
            .HasForeignKey(a => a.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<BlogImage>()
            .HasOne(b => b.Article)
            .WithMany(a => a.Images)
            .HasForeignKey(b => b.ArticleId);
    }

}











//builder.Entity<ApplicationUser>(entity =>
//        {
//            entity.ToTable(name: "User");
//        });
//builder.Entity<IdentityRole>(entity =>
//{
//    entity.ToTable(name: "Role");
//});
//builder.Entity<IdentityUserRole<string>>(entity =>
//{
//    entity.ToTable(name: "UserRoles");
//});
//builder.Entity<IdentityUserClaim<string>>(entity =>
//{
//    entity.ToTable(name: "UserClaims");
//});
//builder.Entity<IdentityUserLogin<string>>(entity =>
//{
//    entity.ToTable(name: "UserLogins");
//});
//builder.Entity<IdentityRoleClaim<string>>(entity =>
//{
//    entity.ToTable(name: "RoleClaims");
//});
//builder.Entity<IdentityUserToken<string>>(entity =>
//{
//    entity.ToTable(name: "UserTokens");
//});






