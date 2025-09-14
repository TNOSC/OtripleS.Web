// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Forms;

public partial class ValidationSummaryBase
{
    [Parameter]
    public IDictionary? ValidationData { get; set; }

    [Parameter]
    public required string Key { get; set; }

    public IEnumerable<string>? Errors
    {
        get => ValidationData?[Key] as IEnumerable<string>;
        set => Errors = value;
    }
}
