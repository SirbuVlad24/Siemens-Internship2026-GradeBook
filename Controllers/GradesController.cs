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

    [HttpGet("/")]
    public IActionResult Index()
    {
        return Redirect("/api/Grades/view");
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
            <body style='font-family: Arial; text-align: center; padding-top: 50px; background-color: #f4f7f6;'>
                <div style='display: inline-block; background: white; padding: 40px; border-radius: 10px; border: 1px solid #ddd;'>
                    <h1 style='color: #333;'>Dashboard GradeBook</h1>
                    
                    <div style='margin-bottom: 30px; padding: 20px; border-bottom: 1px solid #eee;'>
                        <h3>1. Toate Notele</h3>
                        <p>Vezi lista completa si statisticile generale.</p>
                        <button onclick='window.location=""/api/Grades""' 
                                style='padding: 10px 20px; cursor: pointer; background: #28a745; color: white; border: none; border-radius: 4px; font-size: 14px;'>
                            Vezi Toate Notele
                        </button>
                    </div>

                    <div style='padding: 20px;'>
                        <h3>2. Filtrare Note Trecere</h3>
                        <p>Introdu numarul N de note pe care vrei sa le vezi:</p>
                        <input type='number' id='count' value='5' style='padding: 10px; width: 80px; margin-bottom: 10px; font-size: 16px;'>
                        <br>
                        <button onclick='window.location=""/api/Grades/passing?count="" + document.getElementById(""count"").value' 
                                style='padding: 10px 20px; cursor: pointer; background: #007bff; color: white; border: none; border-radius: 4px; font-size: 14px;'>
                            Filtreaza Primele N
                        </button>
                    </div>
                </div>
            </body>
        </html>";

        return base.Content(html, "text/html");
    }
}
