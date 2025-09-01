// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Shouldly;
using Tnosc.Lib.Client.Web.Enums;
using Tnosc.OtripleS.Client.Web.Components.Students;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit;

public partial class StudentFormComponentTests
{
    [Fact]
    public void ShouldInitializeComponent()
    {
        // given . when
        var initializedComponent = new StudentFormComponent();

        // then
        initializedComponent.StudentNameTextBox.ShouldBeNull();
    }

    [Fact]
    public void ShouldRenderComponent()
    {
        // given
        ComponentState expectedState = ComponentState.Content;

        // then
        renderedStudentFormComponent =
            RenderComponent<StudentFormComponent>();

        // then
        renderedStudentFormComponent.Instance.State
            .ShouldBeEquivalentTo(expected: expectedState);
        renderedStudentFormComponent.Instance.StudentNameTextBox
            .ShouldNotBeNull();
        renderedStudentFormComponent.Instance.StudentNameTextBox.PlaceHolder
            .ShouldBeEquivalentTo(expected: "Name");
    }
}
