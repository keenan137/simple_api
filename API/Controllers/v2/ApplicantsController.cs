using API.Data.Repository;
using API.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v2;

[ApiVersion("2.0")]                                         // This attribute specifies that this controller belongs to API version 2. It allows the API versioning system to route requests to the appropriate controller based on the specified version in the request URL or headers.
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]             // The route template includes the API version as a URL segment, allowing clients to specify the desired API version in the request URL (e.g., "api/v1/applicant" or "api/v2/applicant").
public class ApplicantsController : ControllerBase
{
    private readonly IApplicantRepository _repository;

    public ApplicantsController(IApplicantRepository repository)
    {
        _repository = repository;
    }

    [MapToApiVersion("2.0")]                                // This attribute indicates that this action method is specifically mapped to API version 2. It ensures that when a request is made to this endpoint, it will only be routed to this method if the API version specified in the request matches version 2. Note: Because this controller is v2 specific this attribute is redudant and only kept for learning.
    [HttpGet("/")]
    public async Task<ActionResult<List<Applicant>>> GetAllApplicants()
    {
        var applicants = await _repository.GetAllApplicants();

        if (!applicants.Any())
        {
            return Ok(new List<Applicant>());
        }

        return Ok(applicants);
    }

    [MapToApiVersion("2.0")]
    [HttpGet("/{applicantId}/skills")]
    public async Task<IActionResult> GetSkills(int applicantId)
    {
        var skills = await _repository.GetSkillsByApplicantId(applicantId);
        if (!skills.Any())
        {
            return NotFound("No skills found for the specified applicant.");
        }

        return Ok(skills);
    }
}
