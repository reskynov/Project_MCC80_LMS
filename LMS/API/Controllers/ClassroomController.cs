using API.DTOs.Accounts;
using API.DTOs.Classrooms;
using API.DTOs.Lessons;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/classrooms")]
    //[Authorize]
    public class ClassroomController : ControllerBase
    {
        private readonly ClassroomService _classroomService;

        public ClassroomController(ClassroomService classroomSService)
        {
            _classroomService = classroomSService;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var result = _classroomService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<ClassroomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<ClassroomDto>>
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
            var result = _classroomService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<ClassroomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "guid not found"
                });
            }

            return Ok(new ResponseHandler<ClassroomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewClassroomDto newClassroomDto)
        {
            var result = _classroomService.Create(newClassroomDto);
            if (result is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ClassroomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal server error"
                });
            }

            return Ok(new ResponseHandler<ClassroomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success insert data",
                Data = result
            });
        }

        [HttpPut]
        public IActionResult Update(ClassroomDto classroomDto)
        {
            var result = _classroomService.Update(classroomDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<ClassroomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "data not found"
                });
            }

            if (result is 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ClassroomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal server error"
                });
            }

            return Ok(new ResponseHandler<ClassroomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success update data"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _classroomService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<ClassroomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "data not found"
                });
            }

            if (result is 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ClassroomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal server error"
                });
            }

            return Ok(new ResponseHandler<ClassroomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success delete data"
            });
        }

        [HttpGet("enroll")]
        //[Authorize(Roles = "Student")]
        public IActionResult EnrollClassroom(string classCode)
        {
            var result = _classroomService.GetEnrollClassroomByCode(classCode);

            if (result is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ClassroomByCodeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Classroom not found or classroom code already expired"
                });
            }

            return Ok(new ResponseHandler<ClassroomByCodeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success get classroom by code",
                Data = result
            });
        }

        [HttpPost("enroll")]
        //[Authorize(Roles = "Student")]
        public IActionResult EnrollClassroom(EnrollClassroomDto enrollClassroomDto)
        {
            var result = _classroomService.EnrollClassroom(enrollClassroomDto);

            if (result is 0)
            {
                return NotFound(new ResponseHandler<EnrollClassroomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "data not found"
                });
            }

            if (result is -1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<EnrollClassroomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Classroom code already expired"
                });
            }

            if (result is -2)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<EnrollClassroomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "User has been enrolled to class"
                });
            }

            if (result is -3)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<EnrollClassroomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal server error"
                });
            }

            return Ok(new ResponseHandler<EnrollClassroomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success enroll to class"
            });
        }

        [HttpGet("people")]
        public IActionResult GetClassroomPeople(Guid guid)
        {
            var result = _classroomService.GetClassroomPeoples(guid);
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<ClassroomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<ClassroomPeopleDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpGet("lesson")]
        public IActionResult GetClassroomLesson(Guid guidClassroom, Guid guidUser)
        {
            var result = _classroomService.GetClassroomLessons(guidClassroom, guidUser);
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<ClassroomLessonDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<ClassroomLessonDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpPut("new-code")]
        //[Authorize(Roles = "Teacher")]
        public IActionResult CreateNewClassCode(Guid guid)
        {
            var result = _classroomService.CreateNewClassCode(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<ClassroomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "data not found"
                });
            }

            if (result is 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ClassroomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal server error"
                });
            }

            return Ok(new ResponseHandler<ClassroomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success create new class code"
            });
        }
    }
}
