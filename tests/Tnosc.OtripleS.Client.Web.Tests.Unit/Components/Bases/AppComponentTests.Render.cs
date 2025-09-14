// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Linq;
using NSubstitute;
using Shouldly;
using Tnosc.Lib.Client.Web.Bases;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Bases;

public partial class AppComponentTests
{
    [Fact]
    public void ShouldNavigateToRoute()
    {
        // given
        string randomRoute = GetRandomRoute();
        string inputRoute = randomRoute;

        // when
        _renderedAppComponent =
            RenderComponent<AppComponent>();

        _renderedAppComponent.Instance.NavigateTo(inputRoute);

        // then
        _navigationBrokerMock.Received(requiredNumberOfCalls: 1)
             .NavigateTo(route: inputRoute);

        _navigationBrokerMock
            .ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);
    }
}
