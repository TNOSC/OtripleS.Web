// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------


using Bunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tnosc.OtripleS.Client.Application.Services.Views.Students;
using Tnosc.OtripleS.Client.Web.Client.Components.Students;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Students;

public partial class StudentRegistrationComponentTests : TestContext
{
    private readonly IStudentViewService _studentViewServiceMock;
    private IRenderedComponent<StudentRegistrationComponent> _renderedStudentRegistrationComponent = null!;

    public StudentRegistrationComponentTests()
    {
        _studentViewServiceMock = Substitute.For<IStudentViewService>();
        IWebHostEnvironment envMock = Substitute.For<IWebHostEnvironment>();
        IConfiguration configMock = Substitute.For<IConfiguration>();
        Services.AddScoped(services => _studentViewServiceMock);
        Services.AddSingleton(envMock);
        Services.AddSingleton(configMock);
        Services.AddServerSideBlazor();
    }
}
