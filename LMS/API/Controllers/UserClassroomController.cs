using API.DTOs.UserClassrooms;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class UserClassroomController : ControllerBase
    {
        private readonly UserClassroomService _userClassroomService;

        public UserClassroomController(UserClassroomService userClassroomService)
        {
            _userClassroomService = userClassroomService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userClassroomService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<IEnumerable<UserClassroomDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<UserClassroomDto>>
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
            var result = _userClassroomService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<UserClassroomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found",
                    Data = result
                });
            }

            return Ok(new ResponseHandler<UserClassroomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewUserClassroomDto newUserClassroomDto)
        {
            var result = _userClassroomService.Create(newUserClassroomDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<NewUserClassroomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Error retrieve from database"
                });
            }

            return Ok(new ResponseHandler<NewUserClassroomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = newUserClassroomDto
            });
        }

        [HttpPut]
        public IActionResult Update(UserClassroomDto userClassroomDto)
        {
            var result = _userClassroomService.Update(userClassroomDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<UserClassroomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<UserClassroomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Error retrieve from database"
                });
            }

            return Ok(new ResponseHandler<UserClassroomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success update data"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _userClassroomService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<UserClassroomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<UserClassroomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Error retrieve from database"
                });
            }

            return Ok(new ResponseHandler<UserClassroomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success delete data"
            });
        }
    }
}
