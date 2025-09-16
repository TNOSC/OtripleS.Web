// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Grids;

public partial class GridBase<T> : ComponentBase
{
    [Parameter]
    public IEnumerable<T> DataSource { get; set; } = [];
}
