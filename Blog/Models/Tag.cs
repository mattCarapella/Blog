using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models;

public class Tag
{
    public Guid Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Article> Articles { get; set; } = new List<Article>();

}