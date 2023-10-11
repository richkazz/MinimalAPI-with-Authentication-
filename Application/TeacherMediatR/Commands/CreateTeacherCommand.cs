﻿using Domain.Models;
using Domain.ViewModel;
using MediatR;

namespace Application.TeacherMediatR.Commands
{
    public class CreateTeacherCommand : IRequest<Teacher?>
    {
        public TeacherViewModel? TeacherViewModels { get; set; }
    }
}
