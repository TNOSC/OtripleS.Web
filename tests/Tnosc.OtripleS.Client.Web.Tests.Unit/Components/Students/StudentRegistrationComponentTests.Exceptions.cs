// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shouldly;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Tnosc.OtripleS.Client.Web.Client.Components.Students;
using Xeptions;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Students;

public partial class StudentRegistrationComponentTests
{
    [Theory]
    [MemberData(nameof(StudentViewValidationExceptions))]
    public async Task ShouldRenderInnerExceptionMessageIfValidationErrorOccurred(
            Xeption studentViewValidationException)
    {
        // given
        string expectedErrorMessage =
            studentViewValidationException.InnerException!.Message;

        _studentViewServiceMock.RegisterStudentViewAsync(studentView: Arg.Any<StudentView>())
           .ThrowsAsync(ex: studentViewValidationException);

        // when
        _renderedStudentRegistrationComponent =
            RenderComponent<StudentRegistrationComponent>();

        _renderedStudentRegistrationComponent.Instance.SubmitButton.Click();

        // then
        _renderedStudentRegistrationComponent.Instance.StatusLabel.Value
            .ShouldBeEquivalentTo(expected: expectedErrorMessage);

        await _studentViewServiceMock.Received(requiredNumberOfCalls: 1)
            .RegisterStudentViewAsync(studentView: _renderedStudentRegistrationComponent.Instance.StudentView);

        _studentViewServiceMock
            .ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);
    }


    [Theory]
    [MemberData(nameof(StudentViewDependencyServiceExceptions))]
    public async Task ShouldRenderExceptionMessageIfDependencyOrServiceErrorOccurred(
            Xeption studentViewDependencyServiceException)
    {
        // given
        string expectedErrorMessage =
            studentViewDependencyServiceException.Message;

        _studentViewServiceMock.RegisterStudentViewAsync(studentView: Arg.Any<StudentView>())
           .ThrowsAsync(ex: studentViewDependencyServiceException);

        // when
        _renderedStudentRegistrationComponent =
            RenderComponent<StudentRegistrationComponent>();

        _renderedStudentRegistrationComponent.Instance.SubmitButton.Click();

        // then
        _renderedStudentRegistrationComponent.Instance.StatusLabel.Value
            .ShouldBeEquivalentTo(expected: expectedErrorMessage);

        await _studentViewServiceMock.Received(requiredNumberOfCalls: 1)
            .RegisterStudentViewAsync(studentView: _renderedStudentRegistrationComponent.Instance.StudentView);

        _studentViewServiceMock
            .ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);
    }
}
