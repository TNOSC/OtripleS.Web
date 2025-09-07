// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Tnosc.OtripleS.Client.Application.Exceptions.Views.Students;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Tnosc.OtripleS.Client.Domain.Students;
using Xeptions;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Views.Students;

public partial class StudentViewServiceTests
{
    [Theory]
    [MemberData(nameof(StudentServiceValidationExceptions))]
    public async Task ShouldThrowDependencyValidationExceptionOnRegisterIfStudentValidationErrorOccuredAndLogItAsync(
            Xeption studentServiceValidationException)
    {
        // given
        StudentView someStudent = CreateRandomStudentView();

        var expectedDependencyValidationException =
            new StudentViewDependencyValidationException(
                message: "Student view dependency validation error occurred, try again.",
                innerException: (studentServiceValidationException.InnerException as Xeption)!);

        _studentServiceMock.RegisterStudentAsync(student: Arg.Any<Student>())
            .ThrowsAsync(ex: studentServiceValidationException);

        // when
        ValueTask<StudentView> registeredStudentViewTask =
            _studentViewService.RegisterStudentViewAsync(studentView: someStudent);

        // then
        await Assert.ThrowsAsync<StudentViewDependencyValidationException>(() =>
            registeredStudentViewTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Xeption>(actualException =>
                actualException.SameExceptionAs(expectedDependencyValidationException)));

        await _studentServiceMock
            .Received(requiredNumberOfCalls: 1)
            .RegisterStudentAsync(student: Arg.Any<Student>());

        _dateTimeBrokerMock
            .Received(requiredNumberOfCalls: 1)
            .GetCurrentDateTime();

        _userServiceMock
           .Received(requiredNumberOfCalls: 1)
           .GetCurrentlyLoggedInUser();
    }
}
