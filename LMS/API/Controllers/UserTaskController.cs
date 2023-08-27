using API.DTOs.UserTasks;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/user-tasks")]
public class UserTaskController : ControllerBase
{
    private readonly UserTaskService _userTaskService;

    public UserTaskController(UserTaskService userTaskService)
    {
        _userTaskService = userTaskService;
    }

    [HttpPost("submit-task")]


    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _userTaskService.GetAll();
        if (!result.Any())
        {
            return NotFound(new ResponseHandler<UserTaskDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found."
            });
        }

        return Ok(new ResponseHandler<IEnumerable<UserTaskDto>>
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
        var result = _userTaskService.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<UserTaskDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        return Ok(new ResponseHandler<UserTaskDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data retrieved successfully.",
            Data = result
        });
    }

    [HttpPost]
    public IActionResult Create(NewUserTaskDto newUserTaskDto)
    {
        var result = _userTaskService.Create(newUserTaskDto);
        if (result is null)
        {
            return StatusCode(500, new ResponseHandler<NewUserTaskDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<NewUserTaskDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been created successfully.",
            Data = newUserTaskDto
        });
    }

    [HttpPut]
    public IActionResult Update(UserTaskDto UserTaskDto)
    {
        var result = _userTaskService.Update(UserTaskDto);
        if (result is -1)
        {
            return NotFound(new ResponseHandler<UserTaskDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<UserTaskDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<UserTaskDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been updated successfully."
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var result = _userTaskService.Delete(guid);
        if (result is -1)
        {
            return NotFound(new ResponseHandler<UserTaskDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<UserTaskDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<UserTaskDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been deleted successfully."
        });
    }

}
