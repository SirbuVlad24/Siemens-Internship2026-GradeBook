using System.Net.Http.Json;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class ExternalGradeRepository : IGradeReader
{
    private readonly HttpClient _httpClient;
    private const string Url = "https://gist.githubusercontent.com/ArdeleanTudor/8ea407832cd9794960e0e6bbd1319f6e/raw/145b121103dd1cee3737a681c487f7295ac82e6b/gistfile1.txt";

    public ExternalGradeRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private class ExternalData
    {
        public List<Grade> Grades { get; set; } = new();
    }

    public async Task<IEnumerable<Grade>> GetAllAsync()
    {
        try
        {
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = await _httpClient.GetFromJsonAsync<ExternalData>(Url, options);
            return data?.Grades ?? Enumerable.Empty<Grade>();
        }
        catch (Exception)
        {
            return Enumerable.Empty<Grade>();
        }
    }

    public async Task<Grade?> GetByIdAsync(int id)
    {
        var grades = await GetAllAsync();
        return grades.FirstOrDefault(g => g.Id == id);
    }
}
