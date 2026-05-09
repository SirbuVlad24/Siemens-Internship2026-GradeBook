using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class InMemoryGradeRepository : IGradeReader
{
    protected readonly List<Grade> _grades = new();
    protected int _nextId = 1;
    
    public InMemoryGradeRepository()
    {
        _grades.Add(new Grade { Id = _nextId++, Value = 10.5m });
        _grades.Add(new Grade { Id = _nextId++, Value = 20.0m });
        _grades.Add(new Grade { Id = _nextId++, Value = 15.75m });
    }

    public virtual Task<Grade?> GetByIdAsync(int id)
    {
        var grade = _grades.FirstOrDefault(g => g.Id == id && g.IsActive);
        return Task.FromResult(grade);
    }

    public virtual Task<IEnumerable<Grade>> GetAllAsync()
    {
        var activeGrades = _grades.Where(g => g.IsActive).AsEnumerable();
        return Task.FromResult(activeGrades);
    }
}
