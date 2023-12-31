﻿using Client.Models;
using Client.Utilities.Handlers;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.Classrooms;

public class NewClassroomVM
{
    [Required(ErrorMessage = "Classroom Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Description is required")]
    public string? Description { get; set; }

    public static implicit operator Classroom(NewClassroomVM newClassroomDto)
    {
        return new Classroom
        {
            Guid = new Guid(),
            Code = GenerateHandler.ClassCode(),
            Name = newClassroomDto.Name,
            Description = newClassroomDto.Description,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator NewClassroomVM(Classroom classroom)
    {
        return new NewClassroomVM
        {
            Name = classroom.Name,
            Description = classroom.Description
        };
    }
}
