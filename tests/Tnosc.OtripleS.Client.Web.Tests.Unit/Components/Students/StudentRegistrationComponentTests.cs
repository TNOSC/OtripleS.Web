// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------


using Bunit;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tnosc.OtripleS.Client.Application.Services.Views.Students;
using Tnosc.OtripleS.Client.Web.Client.Components.Students;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Students;

public partial class StudentRegistrationComponentTests : TestContext
{
    private readonly IStudentViewService _studentViewServiceMock;

    public StudentRegistrationComponentTests()
    {
        _studentViewServiceMock = Substitute.For<IStudentViewService>();
        Services.AddScoped(services => _studentViewServiceMock);
        Services.AddServerSideBlazor();
    }
}
