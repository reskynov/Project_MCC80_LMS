﻿using Client.Contracts;
using Client.Models;
using Client.ViewModels.Users;
using Client.ViewModels.UserTasks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class GradeController : Controller
    {
        private readonly IUserTaskRepository _userTaskRepository;

        public GradeController(IUserTaskRepository userTaskRepository)
        {
            _userTaskRepository = userTaskRepository;
        }

        public async Task<IActionResult> Index(Guid guid)
        {
            var result = await _userTaskRepository.GetTaskToGrade(guid);
            if (result != null)
            {
                var listTask = result.Data;
                return View(listTask);
            }
            else
            {
                return View(new List<GetTaskToGradeVM>());
            }
        }
    }
}
