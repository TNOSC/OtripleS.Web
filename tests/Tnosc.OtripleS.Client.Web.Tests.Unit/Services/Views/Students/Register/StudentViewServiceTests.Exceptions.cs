// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
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
    [MemberData(nameof(DependencyValidationExceptions))]
    public async Task ShouldThrowDependencyValidationExceptionOnRegisterIfDependencyValidationErrorOccursAndLogItAsync(
        Xeption dependencyValidationException)
    {
        // given
        StudentView someStudent = CreateRandomStudentView();

        var expectedDependencyValidationException =
            new StudentViewDependencyValidationException(
                message: "Student view dependency validation error occurred, try again.",
                innerException: (dependencyValidationException.InnerException as Xeption)!);

        _studentServiceMock.RegisterStudentAsync(student: Arg.Any<Student>())
            .ThrowsAsync(ex: dependencyValidationException);

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

    [Theory]
    [MemberData(nameof(DependencyExceptions))]
    public async Task ShouldThrowDependencyExceptionOnRegisterIfDependencyErrorOccursAndLogItAsync(
        Xeption dependencyException)
    {
        // given
        StudentView someStudent = CreateRandomStudentView();

        var expectedDependencyException =
            new StudentViewDependencyException(
                message: "Student view dependency error occurred, try again.",
                innerException: dependencyException);

        _studentServiceMock.RegisterStudentAsync(student: Arg.Any<Student>())
            .ThrowsAsync(ex: dependencyException);

        // when
        ValueTask<StudentView> registeredStudentViewTask =
            _studentViewService.RegisterStudentViewAsync(studentView: someStudent);

        // then
        await Assert.ThrowsAsync<StudentViewDependencyException>(() =>
            registeredStudentViewTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Xeption>(actualException =>
                actualException.SameExceptionAs(expectedDependencyException)));

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

    [Fact]
    public async Task ShouldThrowServiceExceptionOnRegisterIfServiceErrorOccursAndLogItAsync()
    {
        // given
        StudentView someStudent = CreateRandomStudentView();
        var serviceException = new Exception();

        var failedStudentViewServiceException =
           new FailedStudentViewServiceException(
               message: "Failed student view service occurred, please contact support.",
               innerException: serviceException);

        var expectedServiceException =
            new StudentViewServiceException(
                message: "Student service error occurred, try again.",
                innerException: failedStudentViewServiceException);

        _dateTimeBrokerMock.GetCurrentDateTime()
            .Throws(ex: serviceException);

        // when
        ValueTask<StudentView> registeredStudentViewTask =
            _studentViewService.RegisterStudentViewAsync(studentView: someStudent);

        // then
        await Assert.ThrowsAsync<StudentViewServiceException>(() =>
            registeredStudentViewTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Xeption>(actualException =>
                actualException.SameExceptionAs(expectedServiceException)));

        await _studentServiceMock
            .Received(requiredNumberOfCalls: 1)
            .RegisterStudentAsync(student: Arg.Any<Student>());

        _dateTimeBrokerMock
            .Received(requiredNumberOfCalls: 1)
            .GetCurrentDateTime();

        _userServiceMock
           .Received(requiredNumberOfCalls: 0)
           .GetCurrentlyLoggedInUser();
    }
}
