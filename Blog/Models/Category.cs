﻿using System.ComponentModel.DataAnnotations;

namespace Blog.Models;

public class Category
{
    public Guid Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    public byte[]? CategoryImage { get; set; }

    public ICollection<Article> Articles { get; set; } = new List<Article>();

}
