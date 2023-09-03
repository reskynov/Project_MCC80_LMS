using Client.Contracts;
using Client.Models;
using Client.ViewModels.Classrooms;
using Client.ViewModels.CombinedViews;
using Client.ViewModels.Lessons;
using Client.ViewModels.UserTasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly IClassroomRepository classroomRepository;
        private readonly IUserTaskRepository userTaskRepository;
        private readonly ILessonRepository lessonRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ClassroomController(IClassroomRepository classroomRepository, IUserTaskRepository userTaskRepository, ILessonRepository lessonRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.classroomRepository = classroomRepository;
            this.userTaskRepository = userTaskRepository;
            this.lessonRepository = lessonRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var result = await classroomRepository.GetAll();
            var ListClassroom = new List<Classroom>();

            if (result.Data != null)
            {
                ListClassroom = result.Data.ToList();
            }
            return View(ListClassroom);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewClassroomVM newClassroomVM)
        {

            var result = await classroomRepository.Post(newClassroomVM);
            if (result.Status == "200")
            {
                TempData["Success"] = "Data berhasil masuk";
                return RedirectToAction(nameof(Index));
            }
            else if (result.Status == "409")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await classroomRepository.Delete(guid);
            if (result.Status == "200")
            {
                TempData["Success"] = "Data Berhasil Dihapus";
            }
            else
            {
                TempData["Error"] = "Gagal Menghapus Data";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await classroomRepository.Get(id);
            var ListClassroom = new Classroom();

            if (result.Data != null)
            {
                ListClassroom = result.Data;
            }
            return View(ListClassroom);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Classroom classroom)
        {
            var result = await classroomRepository.Put(classroom.Guid, classroom);

            if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Index", "Classroom");
            }
            return RedirectToAction(nameof(Edit));
        }

        public async Task<IActionResult> LessonDetail(Guid lessonGuid)
        {
            var userGuidClaim = User.FindFirst("Guid");

            if (userGuidClaim != null && Guid.TryParse(userGuidClaim.Value, out Guid guid))
            {
                var resultSubmittedTask = await userTaskRepository.GetSubmittedTask(guid, lessonGuid);
                if (resultSubmittedTask is null)
                {
                    return View("LessonDetail", "Classroom");
                }
                var dataSubmittedTask = resultSubmittedTask.Data;

                var resultLessonDetail = await lessonRepository.GetLessonDetailByGuid(lessonGuid);
                if (resultLessonDetail != null && resultLessonDetail.Data != null)
                {
                    var lessonDetail = resultLessonDetail.Data;

                    var lessonDetailView = new LessonWithTaskVM
                    {
                        LessonDetailVM = lessonDetail,
                        GetSubmittedTaskVM = dataSubmittedTask
                    };

                    return View(lessonDetailView);
                }
                else
                {
                    var lessonDetailView = new LessonWithTaskVM
                    {
                        LessonDetailVM = new LessonDetailVM(),
                        GetSubmittedTaskVM = dataSubmittedTask
                    };

                    return View(lessonDetailView);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public async Task<IActionResult> UploadTask([FromForm] SubmitTaskVM submitTaskVM)
        {
            var fullNameClaim = User.FindFirst("FullName").Value;
            string externalUrl = "/Classroom/LessonDetail?lessonGuid=" + submitTaskVM.LessonGuid;
            if (submitTaskVM.AttachmentFile != null)
            {
                var fileName = fullNameClaim + "_" + DateTime.Now.ToString("MM_dd_yyyy HH_mm_ss") + "_" + Path.GetFileName(submitTaskVM.AttachmentFile.FileName);
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, "files", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await submitTaskVM.AttachmentFile.CopyToAsync(stream);
                }
                submitTaskVM.Attachment = fileName;
            }

            var result = await userTaskRepository.SubmitTask(submitTaskVM);

            if (result.Code is 200)
            {
                TempData["Success"] = "Data berhasil masuk";
                return Redirect(externalUrl);
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Redirect(externalUrl);
            }
            return Redirect(externalUrl);
        }

        public IActionResult DownloadTask(string fileName)
        {
            if(fileName is null)
            {
                return Content("File name's is not valid");
            }
            var filePath = Path.Combine(webHostEnvironment.WebRootPath, "files", fileName);


            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                //Jika ingin menggunakan ekstensi khusus
                //var fileExtension = Path.GetExtension(filePath);
                //var contentType = GetContentType(fileExtension);
                //return File(fileBytes, contentType, fileName);
                return File(fileBytes, "application/octet-stream", fileName);
            }
            else
            {
                return Content("file is not found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UnsubmitTask(Guid userTaskGuid, string fileName, Guid lessonGuid)
        {
            string externalUrl = "/Classroom/LessonDetail?lessonGuid=" + lessonGuid;
            var filePath = Path.Combine(webHostEnvironment.WebRootPath, "files", fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                var result = await userTaskRepository.Delete(userTaskGuid);
                if (result.Code is 200)
                {
                    TempData["Success"] = "Success to Unsubmit Your Task";
                }
                else
                {
                    TempData["Error"] = "Failed to Unsubmit Your Task";
                }
                return Redirect(externalUrl);
            }
            else
            {
                return Redirect(externalUrl);
            }

        }

        //private string GetContentType(string fileExtension)
        //{
        //    switch (fileExtension.ToLower())
        //    {
        //        case ".txt":
        //            return "text/plain";
        //        case ".pdf":
        //            return "application/pdf";
        //        case ".doc":
        //        case ".docx":
        //            return "application/msword";
        //        // Tambahkan tipe konten file lain sesuai kebutuhan Anda
        //        default:
        //            return "application/octet-stream";
        //    }
        //}
    }
}
