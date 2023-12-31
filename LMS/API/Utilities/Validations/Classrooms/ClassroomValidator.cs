﻿using API.DTOs.Classrooms;
using FluentValidation;

namespace API.Utilities.Validations.Classrooms
{
    public class ClassroomValidator : AbstractValidator<ClassroomDto>
    {
        public ClassroomValidator() 
        {
            RuleFor(c => c.Name)
                .NotEmpty();
        }
    }
}
