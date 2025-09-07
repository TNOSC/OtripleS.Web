// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Tnosc.OtripleS.Client.Application.Exceptions.Views.Students;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Xeptions;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Views.Students;

public partial class StudentViewServiceTests
{
    [Fact]
    public async Task ShouldThrowValidationExceptionOnRegisterIfStudentViewIsNullAndLogItAsync()
    {
        // given
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        StudentView nullStudentView = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var nullStudentViewException = new NullStudentViewException(message: "The student is null.");

        var expectedStudentViewValidationException =
            new StudentViewValidationException(
                message: "Invalid input, fix the errors and try again.",
                innerException: nullStudentViewException);
        // when
#pragma warning disable CS8604 // Possible null reference argument.
        ValueTask<StudentView> registerStudentViewTask =
            _studentViewService.RegisterStudentViewAsync(studentView: nullStudentView);
#pragma warning restore CS8604 // Possible null reference argument.

        // then
        await Assert.ThrowsAsync<StudentViewValidationException>(() =>
         registerStudentViewTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Xeption>(actualException =>
                actualException.SameExceptionAs(expectedStudentViewValidationException)));

        _studentServiceMock
            .ReceivedCalls()
            .ShouldBeEmpty();

        _dateTimeBrokerMock
            .ReceivedCalls()
            .ShouldBeEmpty();
    }
}
