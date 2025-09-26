// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shouldly;
using Tnosc.Lib.Client.Web.Enums;
using Tnosc.OtripleS.Client.Web.Client.Components.Students;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Students;

public partial class StudentsComponentTests
{
    [Fact]
    public async Task ShouldRenderErrorIfExceptionOccursAsync()
    {
        // given
        ComponentState expectedState =
            ComponentState.Error;

        string randomMessage = GetRandomMessage();
        string exceptionErrorMessage = randomMessage;
        string expectedErrorMessage = exceptionErrorMessage;
        var exception = new Exception(exceptionErrorMessage);

        _studentViewServiceMock.RetrieveAllStudentViewsAsync()
            .ThrowsAsync(exception);

        // when
        _renderedStudentsComponent =
            RenderComponent<StudentsComponent>();

        // then
        _renderedStudentsComponent.Instance.State.
            ShouldBe(expectedState);

        _renderedStudentsComponent.Instance.ErrorMessage.
            ShouldBe(expectedErrorMessage);

        _renderedStudentsComponent.Instance.ErrorLabel
            .ShouldNotBeNull();

        _renderedStudentsComponent.Instance.ErrorLabel.Value
            .ShouldBe(expectedErrorMessage);

        _renderedStudentsComponent.Instance.StudentViews
            .ShouldBeNull();

        await _studentViewServiceMock.Received(requiredNumberOfCalls: 1)
             .RetrieveAllStudentViewsAsync();
        _studentViewServiceMock.ReceivedCalls().Count().ShouldBe(1);
    }
}
