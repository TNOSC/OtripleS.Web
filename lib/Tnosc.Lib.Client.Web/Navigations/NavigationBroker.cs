// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Navigations;

internal sealed class NavigationBroker : INavigationBroker
{
    private readonly NavigationManager _navigationManager;

    public NavigationBroker(NavigationManager navigationManager) =>
        _navigationManager = navigationManager;

    public void NavigateTo(string route) =>
        _navigationManager.NavigateTo(route);
}
