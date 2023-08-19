using API.DTOs.Grades;
using API.DTOs.Roles;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/roles")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _roleService.GetAll();
        if (!result.Any())
        {
            return NotFound(new ResponseHandler<RoleDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found."
            });
        }

        return Ok(new ResponseHandler<IEnumerable<RoleDto>>
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
        var result = _roleService.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<RoleDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        return Ok(new ResponseHandler<RoleDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data retrieved successfully.",
            Data = result
        });
    }

    [HttpPost]
    public IActionResult Create(NewRoleDto newRoleDto)
    {
        var result = _roleService.Create(newRoleDto);
        if (result is null)
        {
            return StatusCode(500, new ResponseHandler<NewRoleDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<NewRoleDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been created successfully.",
            Data = newRoleDto
        });
    }

    [HttpPut]
    public IActionResult Update(RoleDto roleDto)
    {
        var result = _roleService.Update(roleDto);
        if (result is -1)
        {
            return NotFound(new ResponseHandler<RoleDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<RoleDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<RoleDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been updated successfully."
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var result = _roleService.Delete(guid);
        if (result is -1)
        {
            return NotFound(new ResponseHandler<RoleDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found."
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<RoleDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error: Unable to retrieve data from the database."
            });
        }

        return Ok(new ResponseHandler<RoleDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success! Data has been deleted successfully."
        });
    }
}
