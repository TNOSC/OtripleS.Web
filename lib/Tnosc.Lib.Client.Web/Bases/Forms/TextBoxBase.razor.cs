// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Forms;

public partial class TextBoxBase : ComponentBase
{
    [Parameter]
    public string Value { get; set; } = string.Empty;

    [Parameter]
    public string PlaceHolder { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    public bool IsDisabled { get; set; }

    public void SetValue(string value) =>
        Value = value;

    public void SetPlaceHolder(string value) =>
        PlaceHolder = value;

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

    private async Task OnValueChanged(ChangeEventArgs args) =>
        await ValueChanged.InvokeAsync(args.Value?.ToString() ?? string.Empty);
}
