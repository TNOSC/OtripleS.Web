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
using Tnosc.OtripleS.Client.Application.Exceptions.Views.Students;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Xeptions;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Views.Students;

public partial class StudentViewServiceTests
{
    [Theory]
    [MemberData(nameof(DependencyExceptions))]
    public async Task ShouldThrowDependencyExceptionOnRetrieveAllStudentsIfDependencyErrorOccursAndLogItAsync(
        Xeption dependencyException)
    {
        // given
        var expectedDependencyException =
            new StudentViewDependencyException(
                message: "Student view dependency error occurred, try again.",
                innerException: dependencyException);

        _studentServiceMock.RetrieveAllStudentsAsync()
            .ThrowsAsync(ex: dependencyException);

        // when
        ValueTask<IEnumerable<StudentView>> retrieveAllStudentViewsTask =
           _studentViewService.RetrieveAllStudentViewsAsync();

        // then
        await Assert.ThrowsAsync<StudentViewDependencyException>(() =>
            retrieveAllStudentViewsTask.AsTask());

        _loggingBrokerMock.Received(requiredNumberOfCalls: 1)
            .LogError(Arg.Is<Xeption>(actualException =>
                actualException.SameExceptionAs(expectedDependencyException)));
        _loggingBrokerMock.ReceivedCalls().Count().ShouldBe(expected: 1);

        await _studentServiceMock.Received(requiredNumberOfCalls: 1)
            .RetrieveAllStudentsAsync();
        _studentServiceMock.ReceivedCalls().Count().ShouldBe(expected: 1);

        _dateTimeBrokerMock.ReceivedCalls().ShouldBeEmpty();
        _userServiceMock.ReceivedCalls().ShouldBeEmpty();
    }
}
