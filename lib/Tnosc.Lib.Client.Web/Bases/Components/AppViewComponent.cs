// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Tnosc.Lib.Client.Web.Brokers.Navigations;
using Tnosc.Lib.Client.Web.Enums;

namespace Tnosc.Lib.Client.Web.Bases.Components;

public partial class AppViewComponent : ComponentBase
{
    [Inject]
    protected INavigationBroker NavigationBroker { get; set; } = default!;

    public ComponentState State { get; set; }

    public void NavigateTo(string route) =>
    TryCatch(() =>
    {
        ValidateRoute(route);
        NavigationBroker.NavigateTo(route: route);
    });
}
