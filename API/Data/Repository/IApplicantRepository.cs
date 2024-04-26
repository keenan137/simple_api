using API.Models;

namespace API.Data.Repository;

public interface IApplicantRepository
{
    /// <summary>
    /// Gets the Applicant by Applicant's Id and includes their Skills.
    /// </summary>
    /// <param name="id">Applicant Id</param>
    Task<Applicant> GetApplicantById(int id);

    /// <summary>
    /// Adds a new Applicant to the Database, including their skills.
    /// </summary>
    /// <param name="applicant">New Applicant</param>
    Task AddApplicant(Applicant applicant);

    /// <summary>
    /// Updates the properties of an existing Applicant in the Database, including their skills.
    /// </summary>
    /// <param name="applicant"></param>
    Task UpdateApplicant(Applicant applicant);

    /// <summary>
    /// Deletes an Applicant from the Database, including their skills.
    /// </summary>
    /// <param name="applicant"></param>
    Task DeleteApplicant(Applicant applicant);
}
