// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Tnosc.Lib.Client.Web.Enums;

namespace Tnosc.Lib.Client.Web.Containers;

public partial class ContainerComponent : ComponentBase
{
    [Parameter]
    public ComponentState State { get; set; }

    [Parameter]
    public RenderFragment Content { get; set; } = null!;

    [Parameter]
    public string Error { get; set; } = string.Empty;
}
