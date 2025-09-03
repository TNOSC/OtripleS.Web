// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Tnosc.OtripleS.Client.Domain.Students;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Views.Students;

public partial class StudentViewServiceTests
{
    [Fact]
    public async Task ShouldRegisterStudentViewAsync()
    {
        // given
        var randomUserId = Guid.NewGuid();
        DateTimeOffset randomDateTime = GetRandomDate();

        dynamic randomStudentViewProperties =
            CreateRandomStudentViewProperties(
                auditDates: randomDateTime,
                auditIds: randomUserId);

        var randomStudentView = new StudentView
        {
            IdentityNumber = randomStudentViewProperties.IdentityNumber,
            FirstName = randomStudentViewProperties.FirstName,
            MiddleName = randomStudentViewProperties.MiddleName,
            LastName = randomStudentViewProperties.LastName,
            Gender = randomStudentViewProperties.GenderView,
            BirthDate = randomStudentViewProperties.BirthDate
        };

        StudentView inputStudentView = randomStudentView;
        StudentView expectedStudentView = inputStudentView;

        var randomStudent = new Student
        {
            Id = randomStudentViewProperties.Id,
            UserId = randomStudentViewProperties.UserId,
            IdentityNumber = randomStudentViewProperties.IdentityNumber,
            FirstName = randomStudentViewProperties.FirstName,
            MiddleName = randomStudentViewProperties.MiddleName,
            LastName = randomStudentViewProperties.LastName,
            Gender = randomStudentViewProperties.Gender,
            BirthDate = randomStudentViewProperties.BirthDate,
            CreatedDate = randomDateTime,
            UpdatedDate = randomDateTime,
            CreatedBy = randomUserId,
            UpdatedBy = randomUserId
        };

        Student expectedInputStudent = randomStudent;
        Student returnedStudent = expectedInputStudent;

        _userServiceMock
            .GetCurrentlyLoggedInUser()
                .Returns(returnThis: randomUserId);

        _dateTimeBrokerMock
           .GetCurrentDateTime()
               .Returns(returnThis: randomDateTime);

        _studentServiceMock.
            RegisterStudentAsync(Arg.Is<Student>(student =>
                SameStudentAs(student, expectedInputStudent)))
                    .Returns(returnThis: returnedStudent);
        // when
        StudentView actualStudentView =
            await _studentViewService
                .RegisterStudentViewAsync(studentView: inputStudentView);

        // then
        actualStudentView.ShouldBeEquivalentTo(expected: expectedStudentView);

        _userServiceMock
            .Received(requiredNumberOfCalls: 1)
            .GetCurrentlyLoggedInUser();

        _userServiceMock
            .ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);

        _dateTimeBrokerMock
           .Received(requiredNumberOfCalls: 1)
           .GetCurrentDateTime();

        _dateTimeBrokerMock
            .ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);

        await _studentServiceMock.Received(requiredNumberOfCalls: 1)
                .RegisterStudentAsync(Arg.Is<Student>(student =>
                      SameStudentAs(student, expectedInputStudent)));

        _studentServiceMock
           .ReceivedCalls()
           .Count()
           .ShouldBe(expected: 1);

    }
}
