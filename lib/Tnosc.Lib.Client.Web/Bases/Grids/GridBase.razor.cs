// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Grids;

public partial class GridBase<TItem> : ComponentBase
{
    [Parameter]
    public IEnumerable<TItem>? DataSource { get; set; }

    [Parameter]
    public RenderFragment? Columns { get; set; }

    public void Load(IEnumerable<TItem>? data) 
        => DataSource = data;

    private bool IsLoading => DataSource == null || !DataSource.Any();
}
