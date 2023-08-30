using API.DTOs.Accounts;
using API.DTOs.Users;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<IEnumerable<UserDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<UserDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _userService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<UserDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found",
                    Data = result
                });
            }

            return Ok(new ResponseHandler<UserDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewUserDto newUserDto)
        {
            var result = _userService.Create(newUserDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<NewUserDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error retrieve from database"
                });
            }

            return Ok(new ResponseHandler<NewUserDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success insert data",
                Data = newUserDto
            });
        }

        [HttpPut]
        public IActionResult Update(UserDto userDto)
        {
            var result = _userService.Update(userDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<UserDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<UserDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error retrieve from database"
                });
            }

            return Ok(new ResponseHandler<UserDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success update data"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _userService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<UserDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<UserDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error retrieve from database"
                });
            }

            return Ok(new ResponseHandler<UserDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success delete data"
            });
        }

        [HttpGet("classroom")]
        public IActionResult GetClassroomByUser(Guid guid)
        {
            var result = _userService.GetCLassroomByUser(guid);
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<IEnumerable<ClassroomByUserDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Classroom not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<ClassroomByUserDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpGet("profile")]
        public IActionResult GetProfile(Guid guid)
        {
            var result = _userService.GetProfile(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<ProfileDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Account not found"
                });
            }

            return Ok(new ResponseHandler<ProfileDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpPut("profile-change-password")]
        [AllowAnonymous]
        public IActionResult ChangePassword(ProfileChangePasswordDto changePasswordDto)
        {
            var update = _userService.ChangePassword(changePasswordDto);
            if (update == 0)
            {
                return NotFound(new ResponseHandler<ProfileChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            if (update == -1)
            {
                return NotFound(new ResponseHandler<ProfileChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Current password incorret"
                });
            }

            if (update == -2)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed to update password"
                });
            }
            return Ok(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfully updated new password"
            });
        }

        [HttpGet("student-task")]
        public IActionResult GetStudentTasks(Guid guid)
        {
            var result = _userService.GetStudentTasks(guid);
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<IEnumerable<StudentTaskDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "No assignment found",
                    Data = result
                });
            }

            return Ok(new ResponseHandler<IEnumerable<StudentTaskDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpGet("teacher-task")]
        public IActionResult GetTeacherTasks(Guid guid)
        {
            var result = _userService.GetTeacherTasks(guid);
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<IEnumerable<TeacherTaskDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "No task found",
                    Data = result
                });
            }

            return Ok(new ResponseHandler<IEnumerable<TeacherTaskDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpGet("student-dashboard")]
        public IActionResult DashboardStudent(Guid guid)
        {
            var result = _userService.GetDashboardStudent(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<DashboardStudentDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found",
                    Data = result
                });
            }

            return Ok(new ResponseHandler<DashboardStudentDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpGet("teacher-dashboard")]
        public IActionResult DashboardTeacher(Guid guid)
        {
            var result = _userService.GetDashboardTeacher(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<DashboardTeacherDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found",
                    Data = result
                });
            }

            return Ok(new ResponseHandler<DashboardTeacherDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }
    }
}
