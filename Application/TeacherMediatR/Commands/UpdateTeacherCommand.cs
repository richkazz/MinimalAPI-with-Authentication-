using Domain.Models;
using Domain.ViewModel;
using MediatR;

namespace Application.TeacherMediatR.Commands
{
    public class UpdateTeacherCommand : IRequest<Teacher?>
    {
        public TeacherViewModel? TeacherViewModels { get; set; }
    }
}
