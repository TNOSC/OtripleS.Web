// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;
using Tnosc.Lib.Client.Web.Enums;
using Tnosc.Lib.Client.Web.Navigations;

namespace Tnosc.Lib.Client.Web.Bases;

public class AppComponent : ComponentBase
{
    [Inject] 
    protected INavigationBroker NavigationBroker { get; set; } = default!;

    public ComponentState State { get; set; }

    public void NavigateTo(string route)
        => throw new NotImplementedException();
}
