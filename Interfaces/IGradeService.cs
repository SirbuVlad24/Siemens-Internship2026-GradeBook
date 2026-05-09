using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IGradeService
{
    Task<IEnumerable<Grade>> GetGradesAsync();
    Task<Grade?> GetGradeByIdAsync(int id);
    Task<object> GetGradesWithStatisticsAsync();
    Task<IEnumerable<Grade>> GetPassingGradesAsync(int count);
}
