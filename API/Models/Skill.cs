using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Skill
{
    [Key]
    [BindNever]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int ApplicantId { get; set; }
}
