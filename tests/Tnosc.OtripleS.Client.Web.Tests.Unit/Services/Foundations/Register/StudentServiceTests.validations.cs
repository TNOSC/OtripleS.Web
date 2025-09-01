// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Tnosc.OtripleS.Client.Application.Exceptions.Foundations.Students;
using Tnosc.OtripleS.Client.Domain.Students;
using Xeptions;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Foundations;

public partial class StudentServiceTests
{
    [Fact]
    public async Task ShouldThrowValidationExceptionOnRegisterWhenStudentIsNullAndLogItAsync()
    {
        // given
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        Student invalidStudent = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var nullStudentException = new NullStudentException(message: "The student is null.");

        var expectedStudentValidationException =
            new StudentValidationException(
                message: "Invalid input, fix the errors and try again.",
                innerException: nullStudentException);
        // when
#pragma warning disable CS8604 // Possible null reference argument.
        ValueTask<Student> registerStudentTask =
            _studentService.RegisterStudentAsync(student: invalidStudent);
#pragma warning restore CS8604 // Possible null reference argument.

        // then
        await Assert.ThrowsAsync<StudentValidationException>(() =>
         registerStudentTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Xeption>(actualException =>
                actualException.SameExceptionAs(expectedStudentValidationException)));

        _apiBrokerMock
            .ReceivedCalls()
            .ShouldBeEmpty();

    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ShouldThrowValidationExceptionOnRegisterWhenStudentIsInvalidAndLogItAsync(string? invalidText)
    {
        // given
#pragma warning disable CS8601 // Possible null reference assignment.
        var invalidStudent = new Student()
        {
            Id = Guid.Empty,
            UserId = invalidText,
            IdentityNumber = invalidText,
            FirstName = invalidText,
            LastName = invalidText,
        };
#pragma warning restore CS8601 // Possible null reference assignment.

        var invalidStudentException =
            new InvalidStudentException(message: "Invalid student. Please fix the errors and try again.");

        invalidStudentException.AddData(
            key: nameof(Student.Id),
            values: "Id is required");

        invalidStudentException.AddData(
            key: nameof(Student.UserId),
            values: "Text is required");

        invalidStudentException.AddData(
            key: nameof(Student.IdentityNumber),
            values: "Text is required");

        invalidStudentException.AddData(
            key: nameof(Student.FirstName),
            values: "Text is required");

        invalidStudentException.AddData(
            key: nameof(Student.LastName),
            values: "Text is required");

        invalidStudentException.AddData(
            key: nameof(Student.BirthDate),
            values: "Date is required");

        invalidStudentException.AddData(
            key: nameof(Student.CreatedBy),
            values: "Id is required");

        invalidStudentException.AddData(
            key: nameof(Student.UpdatedBy),
            values: "Id is required");

        invalidStudentException.AddData(
            key: nameof(Student.CreatedDate),
            values: "Date is required");

        invalidStudentException.AddData(
            key: nameof(Student.UpdatedDate),
            values: "Date is required");

        var expectedStudentValidationException =
            new StudentValidationException(
                message: "Invalid input, fix the errors and try again.",
                innerException: invalidStudentException);

        // when
        ValueTask<Student> registerStudentTask =
            _studentService.RegisterStudentAsync(student: invalidStudent);

        // then
        await Assert.ThrowsAsync<StudentValidationException>(() =>
            registerStudentTask.AsTask());


        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Xeption>(actualException =>
                actualException.SameExceptionAs(expectedStudentValidationException)));

        _apiBrokerMock
            .ReceivedCalls()
            .ShouldBeEmpty();
    }
}
