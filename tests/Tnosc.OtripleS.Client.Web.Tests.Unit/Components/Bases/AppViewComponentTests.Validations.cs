// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using NSubstitute;
using Shouldly;
using Tnosc.Lib.Client.Web.Bases.Components;
using Tnosc.Lib.Client.Web.Exceptions;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Bases;

public partial class AppViewComponentTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void ShouldThrowValidationExceptionOnNavigateIfRouteIsInvalidAndLogItAsync(
        string invalidRoute)
    {
        // given
        _renderedAppComponent = RenderComponent<AppViewComponent>();

        // when
        Action navigateToAction = () =>
            _renderedAppComponent.Instance.NavigateTo(invalidRoute);

        // then
        Assert.Throws<AppViewComponentValidationException>(navigateToAction);

        _navigationBrokerMock
            .ReceivedCalls()
            .ShouldBeEmpty();
    }
}
