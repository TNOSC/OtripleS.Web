// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Forms;

public partial class TextBoxBase : ComponentBase
{
    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public required string Value { get; set; }

    [Parameter]
    public required string PlaceHolder { get; set; }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    public bool IsDisabled { get; set; }

    [Parameter]
    public bool Required { get; set; }

    [Parameter]
    public IDictionary? ValidationData { get; set; }

    [Parameter]
    public string? Key { get; set; }

    private string CssClass
     => !string.IsNullOrEmpty(Key)
        && (ValidationData?[Key] as IEnumerable<string>)?.Any() == true
            ? "invalid"
            : string.Empty;

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
