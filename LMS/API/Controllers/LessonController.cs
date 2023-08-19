using API.DTOs.Grades;
using API.DTOs.Lessons;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/lessons")]
public class LessonController : ControllerBase
{
    private readonly LessonService _lessonService;
    public LessonController(LessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _lessonService.GetAll();
        if (!result.Any())
        {
            return NotFound(new ResponseHandler<LessonDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found."
            });
        }

        return Ok(new ResponseHandler<IEnumerable<LessonDto>>
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
        var result = _lessonService.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<LessonDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        return Ok(new ResponseHandler<LessonDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data retrieved successfully.",
            Data = result
        });
    }

    [HttpPost]
    public IActionResult Create(NewLessonDto newLessonDto)
    {
        var result = _lessonService.Create(newLessonDto);
        if (result is null)
        {
            return StatusCode(500, new ResponseHandler<NewLessonDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<NewLessonDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been created successfully.",
            Data = newLessonDto
        });
    }

    [HttpPut]
    public IActionResult Update(LessonDto lessonDto)
    {
        var result = _lessonService.Update(lessonDto);
        if (result is -1)
        {
            return NotFound(new ResponseHandler<LessonDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<LessonDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<LessonDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been updated successfully."
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var result = _lessonService.Delete(guid);
        if (result is -1)
        {
            return NotFound(new ResponseHandler<LessonDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<LessonDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<LessonDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been deleted successfully."
        });
    }
}
