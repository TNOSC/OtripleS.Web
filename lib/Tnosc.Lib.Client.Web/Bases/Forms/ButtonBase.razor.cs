// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Forms;

public partial class ButtonBase : ComponentBase
{
    [Parameter] public string Label { get; set; } = string.Empty;

    [Parameter]
    public Action? OnClick { get; set; }

    [Parameter]
    public bool IsDisabled { get; set; }

    [Parameter]
    public bool Loading { get; set; }

    [Parameter] 
    public Appearance Appearance { get; set; } = Appearance.Accent;

    [Parameter] 
    public Icon? IconStart { get; set; }

    [Parameter]
    public Icon? IconEnd { get; set; }

    [Parameter] 
    public string? Title { get; set; }

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

    public void SetLoading(bool value)
    {
        Loading = value;
        InvokeAsync(StateHasChanged);
    }
}
