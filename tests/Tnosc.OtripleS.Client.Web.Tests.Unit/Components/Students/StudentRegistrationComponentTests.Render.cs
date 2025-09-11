// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

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
        initialStudentRegistrationComponent.SubmitButton.ShouldBeNull();
        initialStudentRegistrationComponent.StudentView.ShouldBeNull();
    }
}
