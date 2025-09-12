// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Forms;

public partial class DatePickerBase : ComponentBase
{
    [Parameter]
    public DateTimeOffset Value { get; set; }

    public void SetValue(DateTimeOffset value) =>
        Value = value;
}
