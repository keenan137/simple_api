using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository;

public class ApplicantRepo : IApplicantRepository
{
    private readonly DataContext _context;
    private readonly ILogger<ApplicantRepo> _logger;

    public ApplicantRepo(DataContext dataContext, ILogger<ApplicantRepo> logger)
    {
        _context = dataContext;
        _logger = logger;
    }

    public async Task<List<Applicant>> GetAllApplicants()
    {
        try
        {
            return await _context.Applicants
                .Include(x => x.Skills)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            throw;
        }
    }

    public async Task<Applicant?> GetApplicantById(int id)
    {
        try
        {
            return await _context.Applicants
                .Include(x => x.Skills)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            throw;
        }
    }

    public async Task<bool> CreateApplicant(Applicant applicant)
    {
        try
        {
            _context.Applicants.Add(applicant);
            int addCount = await _context.SaveChangesAsync();
            return addCount > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            throw;
        }
    }

    public async Task<bool> UpdateApplicant(Applicant applicant)
    {
        try
        {
            _context.Applicants.Update(applicant);
            int updateCount = await _context.SaveChangesAsync();
            return updateCount > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            throw;
        }
    }

    public async Task<bool> DeleteApplicant(int id)
    {
        try
        {
            Applicant? applicant = await _context.Applicants.FirstOrDefaultAsync(x => x.Id == id);

            if (applicant is null) return false;

            _context.Applicants.Remove(applicant);
            int deleteCount = await _context.SaveChangesAsync();
            return deleteCount > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            throw;
        }
    }

    public async Task<List<Skill>> GetSkillsByApplicantId(int id)
    {
        try
        {
            return await _context.Skills
                .Where(x => x.ApplicantId == id)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            throw;
        }
    }

    public async Task<Skill?> GetSkillById(int id)
    {
        try
        {
            return await _context.Skills
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            throw;
        }
    }

    public async Task<Skill?> GetSkillByApplicantId(int applicantId, int skillId)
    {
        try
        {
            return await _context.Skills
                .FirstOrDefaultAsync(x => x.Id == skillId && x.ApplicantId == applicantId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            throw;
        }
    }

    public async Task<bool> CreateApplicantSkill(Skill skill)
    {
        try
        {
            _context.Skills.Add(skill);
            int addedCount = await _context.SaveChangesAsync();
            return addedCount > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            throw;
        }
    }

    public async Task<bool> UpdateApplicantSkill(Skill skill)
    {
        try
        {
            _context.Skills.Update(skill);
            int updateCount = await _context.SaveChangesAsync();
            return updateCount > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            throw;
        }
    }

    public async Task<bool> DeleteApplicantSkill(Skill skill)
    {
        try
        {
            _context.Skills.Remove(skill);
            int deleteCount = await _context.SaveChangesAsync();
            return deleteCount > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            throw;
        }
    }
}
