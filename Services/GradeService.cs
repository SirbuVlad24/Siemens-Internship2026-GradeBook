using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Services;

public class GradeService : IGradeService
{
    private readonly IGradeReader _reader;

    public GradeService(IGradeReader reader)
    {
        _reader = reader;
    }

    public async Task<IEnumerable<Grade>> GetGradesAsync()
    {
        return await _reader.GetAllAsync();
    }

    public async Task<Grade?> GetGradeByIdAsync(int id)
    {
        return await _reader.GetByIdAsync(id);
    }

    public async Task<object> GetGradesWithStatisticsAsync()
    {
        var grades = await _reader.GetAllAsync();
        var gradeList = grades.ToList();

        var totalCount = gradeList.Count;
        var averageValue = gradeList.Any() ? gradeList.Average(g => g.Value) : 0;

        return new
        {
            Data = gradeList,
            Statistics = new
            {
                TotalCount = totalCount,
                AverageValue = averageValue,
                RetrievedAt = DateTime.UtcNow
            }
        };
    }

    public async Task<IEnumerable<Grade>> GetPassingGradesAsync(int count)
    {
        var grades = await _reader.GetAllAsync();
        
        return grades
            .Where(g => g.Value >= 5 && g.IsActive)
            .Take(count);
    }
}
