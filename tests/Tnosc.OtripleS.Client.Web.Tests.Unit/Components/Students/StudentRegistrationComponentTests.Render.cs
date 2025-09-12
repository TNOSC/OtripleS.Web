// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using NSubstitute;
using Shouldly;
using Tnosc.Lib.Client.Web.Enums;
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
        initialStudentRegistrationComponent.SubmitButton.ShouldBeNull();
        initialStudentRegistrationComponent.StudentView.ShouldBeNull();
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
        string expectedLastnameTextBoxPlaceholder = "Last Name";
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
            .ShouldBe(expectedLastnameTextBoxPlaceholder);

        _renderedStudentRegistrationComponent.Instance.StudentGenderDropDown
            .ShouldNotBeNull();

        _renderedStudentRegistrationComponent.Instance.SubmitButton.Label
            .ShouldBe(expectedSubmitButtonLabel);

        _renderedStudentRegistrationComponent.Instance.SubmitButton
            .ShouldNotBeNull();

        _studentViewServiceMock
            .ReceivedCalls()
            .ShouldBeEmpty();
    }
}
