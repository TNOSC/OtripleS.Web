// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Tnosc.Lib.Client.Web.Enums;

namespace Tnosc.Lib.Client.Web.Containers;

public partial class ContainerStatesComponent :ComponentBase
{
    [Parameter]
    public ComponentState State { get; set; }

    [Parameter]
    public RenderFragment LoadingFragment { get; set; } = null!;

    [Parameter]
    public RenderFragment ContentFragment { get; set; } = null!;

    [Parameter]
    public RenderFragment ErrorFragment { get; set; } = null!;

    private RenderFragment GetComponentStateFragment() =>
    State switch
    {
        ComponentState.Loading => LoadingFragment,
        ComponentState.Content => ContentFragment,
        ComponentState.Error => ErrorFragment,
        _ => ErrorFragment
    };
}
