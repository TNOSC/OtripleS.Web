// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Loaders;

public partial class ProgressLoaderBase : ComponentBase
{
    [Parameter]
    public string ProgressText { get; set; } = "Loading...";

    [Parameter]
    public int Columns { get; set; } = 5;
}
