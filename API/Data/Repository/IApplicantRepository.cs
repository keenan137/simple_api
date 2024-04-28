using API.Models;

namespace API.Data.Repository;

public interface IApplicantRepository
{
    /// <summary>
    /// Gets all Applicants in the Database.
    /// </summary>
    /// <returns>List of All Applicants.</returns>
    Task<List<Applicant>> GetAllApplicants();

    /// <summary>
    /// Gets the Applicant by Applicant's Id and includes their Skills.
    /// </summary>
    /// <param name="id">Applicant Id</param>
    /// <returns>Applicant | null</returns>
    Task<Applicant?> GetApplicantById(int id);

    /// <summary>
    /// Adds a new Applicant to the Database, including their skills.
    /// </summary>
    /// <param name="applicant">New Applicant</param>
    /// <returns>Create Status</returns>
    Task<bool> CreateApplicant(Applicant applicant);

    /// <summary>
    /// Updates the properties of an existing Applicant in the Database, including their skills.
    /// </summary>
    /// <param name="applicant">Applicant to Update.</param>
    /// <returns>Update Status</returns>
    Task<bool> UpdateApplicant(Applicant applicant);

    /// <summary>
    /// Deletes an Applicant from the Database, including their skills.
    /// </summary>
    /// <param name="id">Id of the Applicant to delete.</param>
    /// <returns>Deletion Status</returns>
    Task<bool> DeleteApplicant(int id);

    /// <summary>
    /// Gets all the skills of a specified Applicant.
    /// </summary>
    /// <param name="id">Id of the Applicant.</param>
    /// <returns>An applicatn't skills.</returns>
    Task<List<Skill>> GetSkillsByApplicantId(int id);

    /// <summary>
    /// Gets a specified skill for a specified Applicant.
    /// </summary>
    /// <param name="applicantId">Applicant Id</param>
    /// <param name="skillId">Skill Id</param>
    /// <returns>Skill | null</returns>
    Task<Skill?> GetSkillByApplicantId(int applicantId, int skillId);

    /// <summary>
    /// Gets the skill by Skill's Id.
    /// </summary>
    /// <param name="id">Skill Id</param>
    /// <returns>Skill | null</returns>
    Task<Skill?> GetSkillById(int id);


    /// <summary>
    /// Creates/Adds a new skill to an existing applicant.
    /// </summary>
    /// <param name="skill">New Skill to be added</param>
    /// <returns>Whether or now the skill was added.</returns>
    Task<bool> CreateApplicantSkill(Skill skill);

    /// <summary>
    /// Updates the properties of an existing Skill in the Database.
    /// </summary>
    /// <param name="applicant">Skill to Update.</param>
    /// <returns>Update Status.</returns>
    Task<bool> UpdateApplicantSkill(Skill skill);

    /// <summary>
    /// Deletes the specified skill from the database.
    /// </summary>
    /// <param name="skill">Skill to delete.</param>
    /// <returns>Deletion Status.</returns>
    Task<bool> DeleteApplicantSkill(Skill skill);
}
