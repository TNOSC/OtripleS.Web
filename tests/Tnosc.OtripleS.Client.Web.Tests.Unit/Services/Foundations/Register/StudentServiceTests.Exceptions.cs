// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RESTFulSense.Exceptions;
using Tnosc.OtripleS.Client.Application.Exceptions.Foundations.Students;
using Tnosc.OtripleS.Client.Domain.Students;
using Xeptions;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Foundations;

public partial class StudentServiceTests
{
    [Fact]
    public async Task ShouldThrowDependencyValidationExceptionRegisterIfBadRequestErrorOccursAndLogItAsync()
    {
        // given
        string exceptionMessage = GetRandomString();
        var responseMessage = new HttpResponseMessage();

        var httpResponseBadRequestException =
            new HttpResponseBadRequestException(
                responseMessage: responseMessage,
                message: exceptionMessage);

        Student someStudent = CreateRandomStudent();

        var invalidStudentDependencyException =
            new InvalidStudentDependencyException(
                message: "Invalid input, fix the errors and try again.",
                innerException: httpResponseBadRequestException);

        var expectedDependencyValidationException =
            new StudentDependencyValidationException(
                message: "Student dependency validation error occurred, try again.",
                innerException: invalidStudentDependencyException);

        _apiBrokerMock.PostStudentAsync(Arg.Any<Student>())
            .ThrowsAsync(invalidStudentDependencyException);

        // when
        ValueTask<Student> registerStudentTask =
            _studentService.RegisterStudentAsync(student: someStudent);

        // then
        await Assert.ThrowsAsync<StudentDependencyValidationException>(() =>
            registerStudentTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Xeption>(actualException =>
                actualException.SameExceptionAs(expectedDependencyValidationException)));

        await _apiBrokerMock
            .Received(requiredNumberOfCalls: 1)
            .PostStudentAsync(student: someStudent);
    }

    [Fact]
    public async Task ShouldThrowDependencyValidationExceptionOnRegisterIfConflictErrorOccursAndLogItAsync()
    {
        // given
        string exceptionMessage = GetRandomString();
        var responseMessage = new HttpResponseMessage();

        var httpResponseConflictException =
            new HttpResponseConflictException(
                responseMessage: responseMessage,
                message: exceptionMessage);

        Student someStudent = CreateRandomStudent();

        var alreadyExistStudentException =
            new AlreadyExistsStudentException(
                message: "Student with the same id already exists.",
                innerException: httpResponseConflictException);

        var expectedDependencyValidationException =
            new StudentDependencyValidationException(
                message: "Student dependency validation error occurred, try again.",
                innerException: alreadyExistStudentException);

        _apiBrokerMock.PostStudentAsync(Arg.Any<Student>())
            .ThrowsAsync(alreadyExistStudentException);

        // when
        ValueTask<Student> registerStudentTask =
            _studentService.RegisterStudentAsync(student: someStudent);

        // then
        await Assert.ThrowsAsync<StudentDependencyValidationException>(() =>
            registerStudentTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Xeption>(actualException =>
                actualException.SameExceptionAs(expectedDependencyValidationException)));

        await _apiBrokerMock
            .Received(requiredNumberOfCalls: 1)
            .PostStudentAsync(student: someStudent);
    }

    [Theory]
    [MemberData(nameof(CriticalApiException))]
    public async Task ShouldThrowCriticalDependencyExceptionOnRegisterIfCriticalErrorOccursAndLogItAsync(
        Exception httpResponseCriticalException)
    {
        // given
        Student someStudent = CreateRandomStudent();

        var failedStudentDependencyException =
            new FailedStudentDependencyException(
                message: "Failed student dependency error occurred, please contact support.",
                innerException: httpResponseCriticalException);

        var expectedDependencyValidationException =
            new StudentDependencyException(
                message: "Student dependency error occurred, please contact support.",
                innerException: failedStudentDependencyException);

        _apiBrokerMock.PostStudentAsync(Arg.Any<Student>())
            .ThrowsAsync(failedStudentDependencyException);

        // when
        ValueTask<Student> registerStudentTask =
            _studentService.RegisterStudentAsync(student: someStudent);

        // then
        await Assert.ThrowsAsync<StudentDependencyException>(() =>
            registerStudentTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogCritical(Arg.Is<Exception>(actualException =>
                actualException.SameExceptionAs(expectedDependencyValidationException)));

        await _apiBrokerMock
            .Received(requiredNumberOfCalls: 1)
            .PostStudentAsync(student: someStudent);
    }

    [Theory]
    [MemberData(nameof(DependencyApiException))]
    public async Task ShouldThrowDependencyExceptionOnRegisterIfCriticalErrorOccursAndLogItAsync(
       Exception httpResponseCriticalException)
    {
        // given
        Student someStudent = CreateRandomStudent();

        var failedStudentDependencyException =
            new FailedStudentDependencyException(
                message: "Failed student dependency error occurred, please contact support.",
                innerException: httpResponseCriticalException);

        var expectedDependencyValidationException =
            new StudentDependencyException(
                message: "Student dependency error occurred, please contact support.",
                innerException: failedStudentDependencyException);

        _apiBrokerMock.PostStudentAsync(Arg.Any<Student>())
            .ThrowsAsync(failedStudentDependencyException);

        // when
        ValueTask<Student> registerStudentTask =
            _studentService.RegisterStudentAsync(student: someStudent);

        // then
        await Assert.ThrowsAsync<StudentDependencyException>(() =>
            registerStudentTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Exception>(actualException =>
                actualException.SameExceptionAs(expectedDependencyValidationException)));

        await _apiBrokerMock
            .Received(requiredNumberOfCalls: 1)
            .PostStudentAsync(student: someStudent);
    }
}
