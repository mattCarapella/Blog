using System.ComponentModel.DataAnnotations;

namespace Blog.Models;

public class BlogImage
{
    public Guid Id { get; set; }

    [MaxLength(225)]
    public string? Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public byte[] Image { get; set; } = Array.Empty<byte>();

    public Guid ArticleId { get; set; }
    public Article? Article { get; set; }

}
