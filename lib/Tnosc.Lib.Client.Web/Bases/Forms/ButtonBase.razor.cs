// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Forms;

public partial class ButtonBase : ComponentBase
{
    [Parameter]
    public string Label { get; set; } = string.Empty;

    [Parameter]
    public Action? OnClick { get; set; }

    [Parameter]
    public bool IsDisabled { get; set; }

    public void Click() =>
        OnClick?.Invoke();

    public void Disable()
    {
        IsDisabled = true;
        InvokeAsync(StateHasChanged);
    }

    public void Enable()
    {
        IsDisabled = false;
        InvokeAsync(StateHasChanged);
    }
}
