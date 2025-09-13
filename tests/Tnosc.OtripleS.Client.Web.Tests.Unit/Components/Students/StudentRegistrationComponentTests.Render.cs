// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Tnosc.Lib.Client.Web.Enums;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Tnosc.OtripleS.Client.Web.Client.Components.Students;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Students;

public partial class StudentRegistrationComponentTests
{
    [Fact]
    public void ShouldInitializeComponent()
    {
        // given
        ComponentState expectedComponentState =
            ComponentState.Loading;

        // when
        var initialStudentRegistrationComponent = new StudentRegistrationComponent();

        // then
        initialStudentRegistrationComponent.State.ShouldBe(expectedComponentState);
        initialStudentRegistrationComponent.Exception.ShouldBeNull();
        initialStudentRegistrationComponent.StudentIdentityTextBox.ShouldBeNull();
        initialStudentRegistrationComponent.StudentFirstNameTextBox.ShouldBeNull();
        initialStudentRegistrationComponent.StudentMiddleNameTextBox.ShouldBeNull();
        initialStudentRegistrationComponent.StudentLastNameTextBox.ShouldBeNull();
        initialStudentRegistrationComponent.StudentGenderDropDown.ShouldBeNull();
        initialStudentRegistrationComponent.DateOfBirthPicker.ShouldBeNull();
        initialStudentRegistrationComponent.SubmitButton.ShouldBeNull();
        initialStudentRegistrationComponent.StudentView.ShouldBeNull();
        initialStudentRegistrationComponent.ErrorLabel.ShouldBeNull();
    }

    [Fact]
    public void ShouldRenderComponent()
    {
        // given
        ComponentState expectedComponentState =
           ComponentState.Content;

        string expectedIdentityTextBoxPlaceholder = "Student Identity";
        string expectedFirstNameTextBoxPlaceholder = "First Name";
        string expectedMiddleNameTextBoxPlaceholder = "Middle Name";
        string expectedLastNameTextBoxPlaceholder = "Last Name";
        string expectedSubmitButtonLabel = "SUBMIT";

        // when
        _renderedStudentRegistrationComponent =
            RenderComponent<StudentRegistrationComponent>();

        // then
        _renderedStudentRegistrationComponent.Instance.StudentView
            .ShouldNotBeNull();

        _renderedStudentRegistrationComponent.Instance.State
            .ShouldBe(expectedComponentState);

        _renderedStudentRegistrationComponent.Instance.StudentIdentityTextBox
            .ShouldNotBeNull();

        _renderedStudentRegistrationComponent.Instance.StudentIdentityTextBox.PlaceHolder
            .ShouldBe(expectedIdentityTextBoxPlaceholder);

        _renderedStudentRegistrationComponent.Instance.StudentFirstNameTextBox
            .ShouldNotBeNull();

        _renderedStudentRegistrationComponent.Instance.StudentFirstNameTextBox.PlaceHolder
            .ShouldBe(expectedFirstNameTextBoxPlaceholder);

        _renderedStudentRegistrationComponent.Instance.StudentMiddleNameTextBox
            .ShouldNotBeNull();

        _renderedStudentRegistrationComponent.Instance.StudentMiddleNameTextBox.PlaceHolder
            .ShouldBe(expectedMiddleNameTextBoxPlaceholder);

        _renderedStudentRegistrationComponent.Instance.StudentLastNameTextBox
            .ShouldNotBeNull();

        _renderedStudentRegistrationComponent.Instance.StudentLastNameTextBox.PlaceHolder
            .ShouldBe(expectedLastNameTextBoxPlaceholder);

        _renderedStudentRegistrationComponent.Instance.StudentGenderDropDown.Value
            .ShouldBeOfType<StudentViewGender>();

        _renderedStudentRegistrationComponent.Instance.DateOfBirthPicker
            .ShouldNotBeNull();

        _renderedStudentRegistrationComponent.Instance.SubmitButton.Label
            .ShouldBe(expectedSubmitButtonLabel);

        _renderedStudentRegistrationComponent.Instance.SubmitButton
            .ShouldNotBeNull();

        _renderedStudentRegistrationComponent.Instance.ErrorLabel
            .ShouldNotBeNull();

        _renderedStudentRegistrationComponent.Instance.ErrorLabel.Color
           .ShouldBe(Color.Red);

        _studentViewServiceMock
            .ReceivedCalls()
            .ShouldBeEmpty();
    }

    [Fact]
    public async Task ShouldSubmitStudentRegistration()
    {
        // given
        StudentView randomStudentView = CreateRandomStudentView();
        StudentView inputStudentView = randomStudentView;

        // when
        _renderedStudentRegistrationComponent =
            RenderComponent<StudentRegistrationComponent>();

        _renderedStudentRegistrationComponent.Instance.StudentIdentityTextBox
            .SetValue(inputStudentView.IdentityNumber);

        _renderedStudentRegistrationComponent.Instance.StudentFirstNameTextBox
            .SetValue(inputStudentView.FirstName);

        _renderedStudentRegistrationComponent.Instance.StudentMiddleNameTextBox
            .SetValue(inputStudentView.MiddleName);

        _renderedStudentRegistrationComponent.Instance.StudentLastNameTextBox
            .SetValue(inputStudentView.LastName);

        _renderedStudentRegistrationComponent.Instance.StudentGenderDropDown
            .SetValue(inputStudentView.Gender);

        _renderedStudentRegistrationComponent.Instance.DateOfBirthPicker
            .SetValue(inputStudentView.BirthDate);

        _renderedStudentRegistrationComponent.Instance.SubmitButton.Click();

        // then
        _renderedStudentRegistrationComponent.Instance.StudentIdentityTextBox.Value
            .ShouldBeEquivalentTo(inputStudentView.IdentityNumber);

        _renderedStudentRegistrationComponent.Instance.StudentFirstNameTextBox.Value
            .ShouldBeEquivalentTo(inputStudentView.FirstName);

        _renderedStudentRegistrationComponent.Instance.StudentMiddleNameTextBox.Value
            .ShouldBeEquivalentTo(inputStudentView.MiddleName);

        _renderedStudentRegistrationComponent.Instance.StudentGenderDropDown.Value
            .ShouldBeEquivalentTo(inputStudentView.Gender);

        _renderedStudentRegistrationComponent.Instance.DateOfBirthPicker.Value
            .ShouldBeEquivalentTo(inputStudentView.BirthDate);

        _renderedStudentRegistrationComponent.Instance.ErrorLabel.Value
           .ShouldBeEmpty();

        await _studentViewServiceMock.Received(requiredNumberOfCalls: 1)
            .RegisterStudentViewAsync(studentView: _renderedStudentRegistrationComponent.Instance.StudentView);

        _studentViewServiceMock
            .ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);
    }
}
