using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Applicant
{
    [Key]
    [BindNever]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    // Navigation Property
    public virtual ICollection<Skill>? Skills { get; set; }
}
