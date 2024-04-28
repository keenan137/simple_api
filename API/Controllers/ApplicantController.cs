using API.Data.Repository;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ApplicantController : ControllerBase
{
    private readonly IApplicantRepository _repository;

    public ApplicantController(IApplicantRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("/get_all")]
    public async Task<ActionResult<List<Applicant>>> GetAllApplicants()
    {
        var applicants = await _repository.GetAllApplicants();

        if (!applicants.Any())
        {
            return Ok(new List<Applicant>());
        }

        return Ok(applicants);
    }

    [HttpGet("/{id}")]
    public async Task<ActionResult<Applicant>> GetApplicant(int id)
    {
        var applicant = await _repository.GetApplicantById(id);
        if (applicant is null)
        {
            return NotFound(new { message = $"Applicant with Id = {id} not found." });
        }

        return Ok(applicant);
    }

    [HttpPost("/create")]
    public async Task<IActionResult> CreateApplicant(Applicant applicant)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (applicant.Skills is null)
        {
            return BadRequest("The skill/s of the applicant are required. Please try again.");
        }

        bool isCreated = await _repository.CreateApplicant(applicant);
        if (!isCreated)
        {
            return NotFound("The applicant could not be created at this time. Please try again.");
        }

        return CreatedAtAction(nameof(GetApplicant), new { id = applicant.Id }, applicant);
    }

    [HttpPut("/update/{id}")]
    public async Task<IActionResult> UpdateApplicant(int id, Applicant applicant)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (applicant.Id != id)
        {
            return BadRequest("Id mismatch. Please ensure you're using the correct applicant id and try again.");
        }

        bool isUpdated = await _repository.UpdateApplicant(applicant);
        if (!isUpdated)
        {
            return NotFound($"Applicant with Id = {id} not found.");
        }

        return NoContent();
    }

    [HttpDelete("/delete/{id}")]
    public async Task<IActionResult> DeleteApplicant(int id)
    {
        var applicantToDelete = await _repository.GetApplicantById(id);
        if (applicantToDelete is null)
        {
            return NotFound($"Applicant with Id = {id} not found.");
        }

        bool isDeleted = await _repository.DeleteApplicant(id);
        if (!isDeleted)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting Applicant.");
        }

        return NoContent();
    }

    [HttpGet("/{applicantId}/skills/get_all")]
    public async Task<IActionResult> GetSkills(int applicantId)
    {
        var skills = await _repository.GetSkillsByApplicantId(applicantId);
        if (!skills.Any())
        {
            return NotFound("No skills found for the specified applicant.");
        }

        return Ok(skills);
    }

    [HttpGet("/{applicantId}/skills/{skillId}")]
    public async Task<IActionResult> GetSkill(int applicantId, int skillId)
    {
        var skill = await _repository.GetSkillByApplicantId(applicantId, skillId);
        if (skill is null)
        {
            return NotFound(new
            {
                message = $"The skill could not be found. Please ensure you are sending the correct 'applicantId' and 'skillId'."
            });
        }

        return Ok(skill);
    }

    [HttpPost("/{applicantId}/skills")]
    public async Task<IActionResult> CreateSkill(int applicantId, Skill skill)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (applicantId != skill.ApplicantId)
        {
            return BadRequest("Applicant Id mismatch. Ensure you're using a valid applicantId and try again.");
        }

        Applicant? applicant = await _repository.GetApplicantById(applicantId);
        if (applicant is null)
        {
            return BadRequest("Associated applicant not found. Ensure you're using a valid applicantId and try again.");
        }

        bool createdSkill = await _repository.CreateApplicantSkill(skill);
        if (!createdSkill)
        {
            return NotFound("The skill could not be created at this time. Please try again.");
        }

        return CreatedAtAction(nameof(GetSkill), new { applicantId = applicantId, skillId = skill.Id }, skill);
    }

    [HttpPut("/{applicantId}/skills/{skillId}")]
    public async Task<IActionResult> UpdateSkill(int applicantId, int skillId, Skill skill)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (skill.ApplicantId != applicantId || skill.Id != skillId)
        {
            return BadRequest("Applicant Id or Skill Id mismatch. Please confirm the Ids and try again.");
        }

        bool updatedSkill = await _repository.UpdateApplicantSkill(skill);
        if (!updatedSkill)
        {
            return NotFound("The skill could not be updated at this time. Please try again.");
        }

        return NoContent();
    }

    [HttpDelete("/{applicantId}/skills/{skillId}")]
    public async Task<IActionResult> DeleteSkill(int applicantId, int skillId)
    {
        var skillToDelete = await _repository.GetSkillById(skillId);
        if (skillToDelete is null || skillToDelete.ApplicantId != applicantId)
        {
            return NotFound("The skill could not be found for the specified applicant.");
        }

        bool isDeleted = await _repository.DeleteApplicantSkill(skillToDelete);
        if (!isDeleted)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the skill.");
        }

        return NoContent();
    }
}
