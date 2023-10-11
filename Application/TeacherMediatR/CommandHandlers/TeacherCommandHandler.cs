using Application.Abstractions;
using Application.ApplicationExceptions;
using Application.TeacherMediatR.Commands;
using Domain.Models;
using MediatR;

namespace Application.TeacherMediatR.CommandHandlers
{
    public class TeacherCommandHandler :
    IRequestHandler<CreateTeacherCommand, Teacher?>,
    IRequestHandler<UpdateTeacherCommand, Teacher?>,
    IRequestHandler<DeleteTeacherCommand, Unit>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectTeachingRepository _subjectTeachingRepository;

        public TeacherCommandHandler(ITeacherRepository teacherRepository, ISubjectTeachingRepository subjectTeachingRepository)
        {
            _teacherRepository = teacherRepository;
            _subjectTeachingRepository = subjectTeachingRepository;
        }

        public async Task<Teacher?> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = new Teacher
            {
                FirstName = request.TeacherViewModels!.FirstName,
                LastName = request.TeacherViewModels!.LastName,
                Email = request.TeacherViewModels!.Email,
                Gender = request.TeacherViewModels!.Gender,
                Qualification = request.TeacherViewModels!.Qualification,
                PhoneNumber = request.TeacherViewModels!.PhoneNumber,
                NextOfKinName = request.TeacherViewModels!.NextOfKinName,
                NextOfkinNumber = request.TeacherViewModels!.NextOfkinNumber,
                Address = request.TeacherViewModels!.Address,
                DateEmployed = request.TeacherViewModels!.DateEmployed
            };

            teacher = await _teacherRepository.CreateAsync(teacher,request.TeacherViewModels);
            
            return teacher;
        }

        public async Task<Teacher?> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByIdAsync(request.TeacherViewModels!.Id);

            if (teacher == null)
            {
                throw new NotFoundException($"{nameof(Teacher)} {request.TeacherViewModels!.Id}");
            }

            teacher.FirstName = request.TeacherViewModels!.FirstName;
            teacher.LastName = request.TeacherViewModels!.LastName;
            teacher.Email = request.TeacherViewModels!.Email;
            teacher.Gender = request.TeacherViewModels!.Gender;
            teacher.Qualification = request.TeacherViewModels!.Qualification;
            teacher.PhoneNumber = request.TeacherViewModels!.PhoneNumber;
            teacher.NextOfKinName = request.TeacherViewModels!.NextOfKinName;
            teacher.NextOfkinNumber = request.TeacherViewModels!.NextOfkinNumber;
            teacher.Address = request.TeacherViewModels!.Address;
            teacher.DateEmployed = request.TeacherViewModels!.DateEmployed;

            teacher = await _teacherRepository.UpdateAsync(teacher, request.TeacherViewModels);
            
            
            
            
            return await _teacherRepository.GetByIdAsync(teacher.Id);
        }

        public async Task<Unit> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByIdAsync(request.Id);

            if (teacher == null)
            {
                throw new NotFoundException($"{nameof(Teacher)} {request.Id}");
            }

            await _teacherRepository.DeleteAsync(teacher.Id);

            return Unit.Value;
        }
    }

}
