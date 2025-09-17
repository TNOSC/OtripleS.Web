using Shouldly;
using Tnosc.Lib.Client.Web.Enums;
using Tnosc.OtripleS.Client.Web.Components.Students;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Students;

public partial class StudentsComponentTests
{
    [Fact]
    public void ShouldInitComponent()
    {
        // given
        ComponentState expectedState =
            ComponentState.Loading;

        // when
        var initialTeachersComponent =
            new StudentsComponent();

        // then
        initialTeachersComponent.State.ShouldBe(expectedState);
        initialTeachersComponent.StudentViewService.ShouldBeNull();
        initialTeachersComponent.StudentViews.ShouldBeNull();
        initialTeachersComponent.StudentGrid.ShouldBeNull();
        initialTeachersComponent.ErrorMessage.ShouldBeNull();
        initialTeachersComponent.ErrorLabel.ShouldBeNull();
    }
}
