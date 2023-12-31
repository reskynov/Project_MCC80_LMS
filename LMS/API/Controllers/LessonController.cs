﻿using API.DTOs.Lessons;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/lessons")]
[Authorize]
public class LessonController : ControllerBase
{
    private readonly LessonService _lessonService;
    public LessonController(LessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet("detail")]
    public IActionResult GetLessonDetailByGuid(Guid guid)
    {
        var result = _lessonService.GetLessonDetailByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<LessonDetailDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        return Ok(new ResponseHandler<LessonDetailDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data retrieved successfully.",
            Data = result
        });
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

    [HttpGet("task")]
    [Authorize(Roles = "Teacher")]
    public IActionResult GetLessonTaskByGuid(Guid guid)
    {
        var result = _lessonService.GetLessonTaskByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<UpdateLessonTaskDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        return Ok(new ResponseHandler<UpdateLessonTaskDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data retrieved successfully.",
            Data = result
        });
    }

    [HttpPost("task")]
    [Authorize(Roles = "Teacher")]
    public IActionResult CreateLessonTask(NewLessonTaskDto newLessonTaskDto)
    {
        var result = _lessonService.CreateLessonTask(newLessonTaskDto);
        if (result is null)
        {
            return StatusCode(500, new ResponseHandler<NewLessonTaskDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<NewLessonTaskDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been created successfully.",
            Data = newLessonTaskDto
        });
    }

    [HttpPut("task")]
    [Authorize(Roles = "Teacher")]
    public IActionResult UpdateLessonTask(UpdateLessonTaskDto updateLessonTaskDto)
    {
        var result = _lessonService.UpdateLessonTask(updateLessonTaskDto);
        if (result is -1)
        {
            return NotFound(new ResponseHandler<UpdateLessonTaskDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Lesson is not found."
            });
        }

        if (result is -2)
        {
            return NotFound(new ResponseHandler<UpdateLessonTaskDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Task is not found."
            });
        }

        if (result is -3)
        {
            return StatusCode(500, new ResponseHandler<UpdateLessonTaskDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to update task to the database."
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<UpdateLessonTaskDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to update lesson to the database."
            });
        }

        return Ok(new ResponseHandler<UpdateLessonTaskDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been updated successfully."
        });
    }
}
