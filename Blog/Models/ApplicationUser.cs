using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models;

public class ApplicationUser : IdentityUser
{
    [Required, MaxLength(50), Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(50), Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Profile Picture")]
    public byte[]? ProfilePicture { get; set; }

    [Display(Name = "Created On"), DataType(DataType.Date)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Display(Name = "Last Login"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:g}")]
    public DateTime? LastLoggedInAt { get; set; }


    public ICollection<Article> Articles { get; set; } = new List<Article>();


    
    [Display(Name = "Name")]
    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }
}
