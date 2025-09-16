// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shouldly;
using Tnosc.OtripleS.Client.Application.Exceptions.Foundations.Students;
using Tnosc.OtripleS.Client.Domain.Students;
using Xeptions;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Foundations.Students;

public partial class StudentServiceTests
{
    [Theory]
    [MemberData(nameof(CriticalApiException))]
    public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfCriticalErrorOccursAndLogItAsync(
        Exception httpResponseCriticalException)
    {
        // given
        var failedStudentCriticalDependencyException =
            new FailedStudentCriticalDependencyException(
                message: "Failed student critical dependency error occurred, please contact support.",
                innerException: httpResponseCriticalException);

        var expectedDependencyValidationException =
            new StudentDependencyException(
                message: "Student dependency error occurred, please contact support.",
                innerException: failedStudentCriticalDependencyException);

        _apiBrokerMock.GetAllStudentsAsync()
            .ThrowsAsync(failedStudentCriticalDependencyException);

        // when
        ValueTask<IEnumerable<Student>> retrievedStudentTask =
            _studentService.RetrieveAllStudentsAsync();

        // then
        await Assert.ThrowsAsync<StudentDependencyException>(() =>
            retrievedStudentTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogCritical(Arg.Is<Exception>(actualException =>
                actualException.SameExceptionAs(expectedDependencyValidationException)));

        await _apiBrokerMock
            .Received(requiredNumberOfCalls: 1)
            .GetAllStudentsAsync();

       _apiBrokerMock.ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);

        _loggingBrokerMock.ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);
    }

    [Theory]
    [MemberData(nameof(DependencyApiException))]
    public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfErrorOccursAndLogItAsync(
        Exception httpResponseCriticalException)
    {
        // given
        var failedStudentDependencyException =
            new FailedStudentDependencyException(
                message: "Failed student dependency error occurred, please contact support.",
                innerException: httpResponseCriticalException);

        var expectedDependencyValidationException =
            new StudentDependencyException(
                message: "Student dependency error occurred, please contact support.",
                innerException: failedStudentDependencyException);

        _apiBrokerMock.GetAllStudentsAsync()
            .ThrowsAsync(failedStudentDependencyException);

        // when
        ValueTask<IEnumerable<Student>> retrievedStudentTask =
           _studentService.RetrieveAllStudentsAsync();

        // then
        await Assert.ThrowsAsync<StudentDependencyException>(() =>
            retrievedStudentTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Exception>(actualException =>
                actualException.SameExceptionAs(expectedDependencyValidationException)));

        _apiBrokerMock.ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);

        _loggingBrokerMock.ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);
    }

    [Fact]
    public async Task ShouldThrowServiceExceptionOnRetrieveAllIfErrorOccursAndLogItAsync()
    {
        // given
        var serviceException = new Exception();

        var failedStudentServiceException =
            new FailedStudentServiceException(
                message: "Failed student service error occurred, contact support.",
                innerException: serviceException);

        var expectedStudentServiceException =
            new StudentServiceException(
                message: "Service error occurred, contact support.",
                innerException: failedStudentServiceException);

        _apiBrokerMock.GetAllStudentsAsync()
            .ThrowsAsync(ex: serviceException);

        // when
        ValueTask<IEnumerable<Student>> retrievedStudentTask =
           _studentService.RetrieveAllStudentsAsync();

        // then
        await Assert.ThrowsAsync<StudentDependencyException>(() =>
            retrievedStudentTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Exception>(actualException =>
                actualException.SameExceptionAs(expectedStudentServiceException)));

        _apiBrokerMock.ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);

        _loggingBrokerMock.ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);
    }
}
