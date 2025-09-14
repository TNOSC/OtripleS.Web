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
using Tnosc.Lib.Client.Web.Bases;
using Tnosc.Lib.Client.Web.Navigations;
using Tynamix.ObjectFiller;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Bases;

public partial class AppComponentTests : TestContext
{
    private readonly INavigationBroker _navigationBrokerMock;
    private IRenderedComponent<AppComponent> _renderedAppComponent = null!;

    public AppComponentTests()
    {
        _navigationBrokerMock = Substitute.For<INavigationBroker>();
        IWebHostEnvironment envMock = Substitute.For<IWebHostEnvironment>();
        IConfiguration configMock = Substitute.For<IConfiguration>();
        Services.AddScoped(services => _navigationBrokerMock);
        Services.AddSingleton(envMock);
        Services.AddSingleton(configMock);
        Services.AddServerSideBlazor();
    }

    private static string GetRandomRoute() =>
           new RandomUrl().GetValue();
}
