// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using NSubstitute;
using Tnosc.Lib.Client.Web.Bases.Components;
using Tnosc.Lib.Client.Web.Brokers.Navigations;
using Tynamix.ObjectFiller;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Bases;

public partial class AppViewComponentTests : TestContext
{
    private readonly INavigationBroker _navigationBrokerMock;
    private IRenderedComponent<AppViewComponent> _renderedAppComponent = null!;

    public AppViewComponentTests()
    {
        _navigationBrokerMock = Substitute.For<INavigationBroker>();
        IWebHostEnvironment envMock = Substitute.For<IWebHostEnvironment>();
        IConfiguration configMock = Substitute.For<IConfiguration>();
        Services.AddScoped(services => _navigationBrokerMock);
        Services.AddSingleton(envMock);
        Services.AddSingleton(configMock);
        Services.AddServerSideBlazor();
        Services.AddFluentUIComponents();
    }

    private static string GetRandomRoute() =>
           new RandomUrl().GetValue();
}
