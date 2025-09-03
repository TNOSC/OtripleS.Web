// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using NSubstitute;
using Tnosc.OtripleS.Client.Application.Brokers.DateTimes;
using Tnosc.OtripleS.Client.Application.Brokers.Loggings;
using Tnosc.OtripleS.Client.Application.Services.Foundations.Students;
using Tnosc.OtripleS.Client.Application.Services.Foundations.Users;
using Tnosc.OtripleS.Client.Application.Services.Views.Students;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Tnosc.OtripleS.Client.Domain.Students;
using Tynamix.ObjectFiller;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Views.Students;

public partial class StudentViewServiceTests
{
    private readonly IUserService _userServiceMock;
    private readonly IDateTimeBroker _dateTimeBrokerMock;
    private readonly IStudentService _studentServiceMock;
    private readonly ILoggingBroker _loggingBroker;
    private readonly StudentViewService _studentViewService;

    public StudentViewServiceTests()
    {
        _userServiceMock = Substitute.For<IUserService>();
        _dateTimeBrokerMock = Substitute.For<IDateTimeBroker>();
        _studentServiceMock = Substitute.For<IStudentService>();
        _loggingBroker = Substitute.For<ILoggingBroker>();

        _studentViewService = new StudentViewService(
            studentService: _studentServiceMock,
            userService: _userServiceMock,
            dateTimeBroker: _dateTimeBrokerMock,
            loggingBroker: _loggingBroker);
    }

    private static dynamic CreateRandomStudentViewProperties(
        DateTimeOffset auditDates,
        Guid auditIds)
    {
        StudentGender randomStudentGender = GetRandomGender();

        return new
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid().ToString(),
            IdentityNumber = GetRandomString(),
            FirstName = GetRandomName(),
            MiddleName = GetRandomName(),
            LastName = GetRandomName(),
            BirthDate = GetRandomDate(),
            Gender = randomStudentGender,
            GenderView = (StudentViewGender)randomStudentGender,
            CreatedDate = auditDates,
            UpdatedDate = auditDates,
            CreatedBy = auditIds,
            UpdatedBy = auditIds
        };
    }

    private static StudentGender GetRandomGender()
    {
        int studentGenderCount =
            Enum.GetValues<StudentGender>().Length;

        int randomStudentGenderValue =
            new IntRange(
                min: 0,
                max: studentGenderCount).GetValue();

        return (StudentGender)randomStudentGenderValue;
    }

    private static string GetRandomString() =>
        new MnemonicString().GetValue();

    private static string GetRandomName() =>
        new RealNames(NameStyle.FirstName).GetValue();

    private static DateTimeOffset GetRandomDate() =>
        new DateTimeRange(earliestDate: DateTime.UtcNow).GetValue();


    private static bool SameStudentAs(Student actualStudent, Student expectedStudent) =>
        actualStudent.IdentityNumber == expectedStudent.IdentityNumber &&
        actualStudent.FirstName == expectedStudent.FirstName &&
        actualStudent.MiddleName == expectedStudent.MiddleName &&
        actualStudent.LastName == expectedStudent.LastName &&
        actualStudent.Gender == expectedStudent.Gender &&
        actualStudent.BirthDate == expectedStudent.BirthDate &&
        actualStudent.CreatedDate == expectedStudent.CreatedDate &&
        actualStudent.UpdatedDate == expectedStudent.UpdatedDate &&
        actualStudent.CreatedBy == expectedStudent.CreatedBy &&
        actualStudent.UpdatedBy == expectedStudent.UpdatedBy;
}
