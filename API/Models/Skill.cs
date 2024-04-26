using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Skill
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public int ApplicantId { get; set; }

    // Navigation Property
    [ForeignKey(nameof(ApplicantId))]
    public virtual Applicant Applicant { get; set; }
}
