// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Forms;

public partial class DropDownBase<TEnum> : ComponentBase
    where TEnum : struct, Enum
{
    [Parameter]
    public required TEnum Value { get; set; }

    [Parameter]
    public EventCallback<TEnum> ValueChanged { get; set; }

    [Parameter]
    public bool IsDisabled { get; set; }

    public void SetValue(TEnum value) =>
        Value = value;

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

    private static IEnumerable<TEnum> EnumValues => Enum.GetValues<TEnum>();

    private string SelectedValue
    {
        get => Value.ToString();
        set 
        {
            if (Enum.TryParse(value, out TEnum enumValue))
            {
                Value = enumValue;
                ValueChanged.InvokeAsync(enumValue);
            }
        }
    }
}
