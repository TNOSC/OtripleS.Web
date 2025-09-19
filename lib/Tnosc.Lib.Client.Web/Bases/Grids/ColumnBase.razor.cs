// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Grids;

public partial class ColumnBase<TItem, TProp> : ComponentBase
{
    [Parameter] 
    public required Expression<Func<TItem, TProp>> Property { get; set; }
    
    [Parameter] 
    public string? Title { get; set; }
    
    [Parameter] 
    public string? Format { get; set; }
}
