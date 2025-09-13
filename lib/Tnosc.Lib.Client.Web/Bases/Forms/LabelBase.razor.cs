// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Tnosc.Lib.Client.Web.Enums;

namespace Tnosc.Lib.Client.Web.Bases.Forms;

public partial class LabelBase : ComponentBase
{
    [Parameter]
    public string Value { get; set; } = string.Empty;

    [Parameter]
    public Color Color { get; set; }

    public void SetValue(string value)
    {
        Value = value;
        InvokeAsync(StateHasChanged);
    }
}
