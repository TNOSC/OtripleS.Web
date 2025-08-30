// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases;

public partial class TextBoxBase
{
    [Parameter]
    public string Value { get; set; } = string.Empty;

    [Parameter]
    public string PlaceHolder { get; set; } = string.Empty;

    public void SetValue(string value) => Value = value;
    public void SetPlaceHolder(string value) => PlaceHolder = value;
}
