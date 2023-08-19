using API.DTOs.Grades;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/grades")]
public class GradeController : ControllerBase
{
    private readonly GradeService _gradeService;

    public GradeController(GradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _gradeService.GetAll();
        if (!result.Any())
        {
            return NotFound(new ResponseHandler<GradeDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found."
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GradeDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data retrieved successfully.",
            Data = result
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _gradeService.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<GradeDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        return Ok(new ResponseHandler<GradeDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data retrieved successfully.",
            Data = result
        });
    }

    [HttpPost]
    public IActionResult Create(NewGradeDto newGradeDto)
    {
        var result = _gradeService.Create(newGradeDto);
        if (result is null)
        {
            return StatusCode(500, new ResponseHandler<NewGradeDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<NewGradeDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been created successfully.",
            Data = newGradeDto
        });
    }

    [HttpPut]
    public IActionResult Update(GradeDto gradeDto)
    {
        var result = _gradeService.Update(gradeDto);
        if (result is -1)
        {
            return NotFound(new ResponseHandler<GradeDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<GradeDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<GradeDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been updated successfully."
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var result = _gradeService.Delete(guid);
        if (result is -1)
        {
            return NotFound(new ResponseHandler<GradeDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<GradeDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<GradeDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been deleted successfully."
        });
    }

}
