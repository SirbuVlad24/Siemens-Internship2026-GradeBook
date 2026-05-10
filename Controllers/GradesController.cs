using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradesController : ControllerBase
{
    private readonly IGradeService _gradeService;
    private readonly ILogger<GradesController> _logger;

    public GradesController(IGradeService gradeService, ILogger<GradesController> logger)
    {
        _gradeService = gradeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("GET api/grades called");

        var result = await _gradeService.GetGradesWithStatisticsAsync();
        
        _logger.LogInformation("Returning all grades with statistics");

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("GET api/grades/{Id} called", id);

        if (id <= 0)
        {
            _logger.LogWarning("Invalid id provided: {Id}", id);
            return BadRequest("Id must be a positive integer.");
        }

        var grade = await _gradeService.GetGradeByIdAsync(id);
        if (grade == null)
        {
            _logger.LogWarning("Grade with Id {Id} not found", id);
            return NotFound($"Grade with Id {id} was not found.");
        }

        return Ok(grade);
    }

    [HttpGet("passing")]
    public async Task<IActionResult> GetPassing([FromQuery] int count = 5)
    {
        _logger.LogInformation("GET api/grades/passing?count={Count} called", count);

        if (count < 0)
        {
            _logger.LogWarning("Invalid count provided: {Count}", count);
            return BadRequest("Count must be non-negative.");
        }

        var grades = await _gradeService.GetPassingGradesAsync(count);
        return Ok(grades);
    }

    [HttpGet("view")]
    public ContentResult GetInputPage()
    {
        var html = @"
        <html>
            <body style='font-family: Arial; text-align: center; padding-top: 50px;'>
                <h1>Interfata GradeBook</h1>
                <p>Introduceti numarul de note de trecere pe care vreti sa le vedeti (N):</p>
                <input type='number' id='count' value='5' style='padding: 10px; width: 80px; font-size: 16px;'>
                <button onclick='window.location=""/api/Grades/passing?count="" + document.getElementById(""count"").value' 
                        style='padding: 10px 20px; cursor: pointer; font-size: 16px; background: #007bff; color: white; border: none; border-radius: 4px;'>
                    Afiseaza Notele
                </button>
            </body>
        </html>";

        return base.Content(html, "text/html");
    }
}
