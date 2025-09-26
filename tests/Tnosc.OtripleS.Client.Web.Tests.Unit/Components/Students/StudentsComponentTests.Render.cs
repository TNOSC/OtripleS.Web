using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.FluentUI.AspNetCore.Components;
using NSubstitute;
using Shouldly;
using Tnosc.Lib.Client.Web.Enums;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Tnosc.OtripleS.Client.Web.Client.Components.Students;
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
        var initialStudentsComponent =
            new StudentsComponent
            {
                DialogService = Substitute.For<IDialogService>(),
                ToastService = Substitute.For<IToastService>()
            };

        // then
        initialStudentsComponent.State.ShouldBe(expectedState);
        initialStudentsComponent.StudentViewService.ShouldBeNull();
        initialStudentsComponent.StudentViews.ShouldBeNull();
        initialStudentsComponent.StudentGrid.ShouldBeNull();
        initialStudentsComponent.ErrorMessage.ShouldBeNull();
        initialStudentsComponent.ErrorLabel.ShouldBeNull();
    }

    [Fact]
    public async Task ShouldRenderStudentsAsync()
    {
        // given
        ComponentState expectedState =
            ComponentState.Content;

        IEnumerable<StudentView> randomStudentViews =
            CreateRandomStudentViews();

        IEnumerable<StudentView> retrievedStudentViews =
            randomStudentViews;

        IEnumerable<StudentView> expectedStudentViews =
            retrievedStudentViews;

        _studentViewServiceMock.RetrieveAllStudentViewsAsync()
            .Returns(retrievedStudentViews);

        // when
        _renderedStudentsComponent =
            RenderComponent<StudentsComponent>();

        // then
        _renderedStudentsComponent.Instance.State
            .ShouldBe(expectedState);

        _renderedStudentsComponent.Instance.StudentViews
            .ShouldBeEquivalentTo(expectedStudentViews);

        _renderedStudentsComponent.Instance.StudentGrid
            .ShouldNotBeNull();

        _renderedStudentsComponent.Instance.StudentGrid.Items
            .ShouldBeEquivalentTo(expectedStudentViews.AsQueryable());

        _renderedStudentsComponent.Instance.ErrorMessage.
            ShouldBeNull();

        _renderedStudentsComponent.Instance.ErrorLabel.
            ShouldBeNull();

        await _studentViewServiceMock.Received(requiredNumberOfCalls: 1)
            .RetrieveAllStudentViewsAsync();
        _studentViewServiceMock.ReceivedCalls().Count().ShouldBe(1);
    }
}
