// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RESTFulSense.Exceptions;
using Shouldly;
using Tnosc.OtripleS.Client.Application.Exceptions.Foundations.Students;
using Tnosc.OtripleS.Client.Domain.Students;
using Xeptions;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Foundations;

public partial class StudentServiceTests
{
    [Fact]
    public async Task ShouldThrowDependencyValidationExceptionAddIfBadRequestErrorOccursAndLogItAsync()
    {
        // given
        string exceptionMessage = GetRandomString();
        var responseMessage = new HttpResponseMessage();

        var httpResponseBadRequestException =
            new HttpResponseBadRequestException(
                responseMessage: responseMessage,
                message: exceptionMessage);

        Student someStudent = CreateRandomStudent();

        var invalidStudentException =
            new InvalidStudentException(
                message: "Invalid input, fix the errors and try again.",
                innerException: httpResponseBadRequestException);

        var expectedDepndencyValidationException =
            new StudentDependencyValidationException(
                message: "Student dependency validation error occurred, try again.",
                innerException: invalidStudentException);

        _apiBrokerMock.PostStudentAsync(Arg.Any<Student>())
                .ThrowsAsync(httpResponseBadRequestException);

        // when
        ValueTask<Student> registerStudentTask =
            _studentService.RegisterStudentAsync(student: someStudent);

        // then
        await Assert.ThrowsAsync<StudentValidationException>(() =>
            registerStudentTask.AsTask());


        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Xeption>(actualException =>
                actualException.SameExceptionAs(expectedDepndencyValidationException)));

        _apiBrokerMock
            .ReceivedCalls()
            .ShouldBeEmpty();
    }
}
