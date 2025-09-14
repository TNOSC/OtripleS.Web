// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using NSubstitute;
using Shouldly;
using Tnosc.Lib.Client.Web.Bases.Components;
using Tnosc.Lib.Client.Web.Exceptions;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Bases;

public partial class AppViewComponentTests
{
    [Fact]
    public void ShouldThrowServiceExceptionOnNavigateIfServiceErrorOccurs()
    {
        // given
        string someRoute = GetRandomRoute();
        var serviceException = new Exception();

        _renderedAppComponent = RenderComponent<AppViewComponent>();

        _navigationBrokerMock.When(l => l.NavigateTo(route: someRoute))
            .Do(x => throw serviceException);

        // when
        Action navigateToAction = () =>
             _renderedAppComponent.Instance.NavigateTo(someRoute);

        // then
        Assert.Throws<AppViewComponentServiceException>(navigateToAction);

        _navigationBrokerMock
           .ReceivedCalls()
           .Count()
           .ShouldBe(expected: 1);
    }
}
