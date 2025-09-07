// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Tnosc.OtripleS.Client.Application.Brokers.DateTimes;
using Tnosc.OtripleS.Client.Application.Brokers.Loggings;
using Tnosc.OtripleS.Client.Application.Services.Foundations.Students;
using Tnosc.OtripleS.Client.Application.Services.Foundations.Users;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Tnosc.OtripleS.Client.Domain.Students;

namespace Tnosc.OtripleS.Client.Application.Services.Views.Students;

public partial class StudentViewService : IStudentViewService
{
#pragma warning disable S4487 // Unread "private" fields should be removed
    private readonly IUserService _userService;
    private readonly IDateTimeBroker _dateTimeBroker;
    private readonly IStudentService _studentService;
    private readonly ILoggingBroker _loggingBroker;
#pragma warning restore S4487 // Unread "private" fields should be removed

    public StudentViewService(
        IStudentService studentService,
        IUserService userService,
        IDateTimeBroker dateTimeBroker,
        ILoggingBroker loggingBroker)
    {
        _userService = userService;
        _dateTimeBroker = dateTimeBroker;
        _studentService = studentService;
        _loggingBroker = loggingBroker;
    }

    public async ValueTask<StudentView> RegisterStudentViewAsync(StudentView studentView) =>
    await TryCatch(async () =>
    {
        ValidateStudentView(studentView: studentView);
        Student student = MapToStudent(studentView);
        await _studentService.RegisterStudentAsync(student);

        return studentView;
    });

    private Student MapToStudent(StudentView studentView)
    {
        Guid currentLoggedInUserId = _userService.GetCurrentlyLoggedInUser();
        DateTimeOffset currentDateTime = _dateTimeBroker.GetCurrentDateTime();

        return new Student
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid().ToString(),
            IdentityNumber = studentView.IdentityNumber,
            FirstName = studentView.FirstName,
            MiddleName = studentView.MiddleName,
            LastName = studentView.LastName,
            Gender = (StudentGender)studentView.Gender,
            BirthDate = studentView.BirthDate,
            CreatedBy = currentLoggedInUserId,
            UpdatedBy = currentLoggedInUserId,
            CreatedDate = currentDateTime,
            UpdatedDate = currentDateTime
        };
    }
}
