using System;
using System.Collections.Generic;
using Bunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using NSubstitute;
using Tnosc.OtripleS.Client.Application.Services.Views.Students;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Tnosc.OtripleS.Client.Web.Components.Students;
using Tynamix.ObjectFiller;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Students;

public partial class StudentsComponentTests : TestContext
{
    private readonly IStudentViewService _studentViewServiceMock;
    private IRenderedComponent<StudentsComponent> _renderedStudentsComponent = null!;
    public StudentsComponentTests()
    {
        _studentViewServiceMock = Substitute.For<IStudentViewService>();
        IWebHostEnvironment envMock = Substitute.For<IWebHostEnvironment>();
        IConfiguration configMock = Substitute.For<IConfiguration>();
        Services.AddScoped(services => _studentViewServiceMock);
        Services.AddSingleton(envMock);
        Services.AddSingleton(configMock);

        Services.AddFluentUIComponents();
        JSInterop.Mode = JSRuntimeMode.Loose;
        JSInterop.SetupModule("./_content/Microsoft.FluentUI.AspNetCore.Components/Components/TextField/FluentTextField.razor.js?v=4.12.1.25197");
        JSInterop.SetupVoid("setControlAttribute", _ => true);
    }

    private static IEnumerable<StudentView> CreateRandomStudentViews() =>
          CreateStudentFiller().Create(count: GetRandomNumber());

    private static int GetRandomNumber() =>
          new IntRange(min: 2, max: 10).GetValue();

    private static Filler<StudentView> CreateStudentFiller()
    {
        var filler = new Filler<StudentView>();

        filler.Setup()
            .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

        return filler;
    }
}
