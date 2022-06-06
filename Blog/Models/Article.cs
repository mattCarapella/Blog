using Blog.Core;
using System.ComponentModel.DataAnnotations;
using static Blog.Core.Enums;

namespace Blog.Models;

public class Article
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Entry { get; set; } = string.Empty;

    //[MaxLength(500)]
    //public string? Description { get; set; } = string.Empty;
    //public bool Published = false;


    public Level Level { get; set; }

    public Status Status { get; set; } = Enums.Status.InProgress;


    [Display(Name = "Created On")]
    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;


    [Display(Name = "Updated At")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:g}")]
    public DateTime? UpdatedAt { get; set; }

    [Display(Name = "Published On")]
    [DataType(DataType.Date)]
    public DateTime? PublishedAt { get; set; }


    public string AuthorId { get; set; } = string.Empty;         // Foriegn Key                              
    public ApplicationUser? Author { get; set; }                 // Navigation property


    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public List<BlogImage> Images { get; set; } = new List<BlogImage>();

}
