// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Breadcrumbs;

public partial class BreadcrumbBase : ComponentBase
{
    [Parameter]
    public IEnumerable<BreadcrumbItem> Items { get; set; } = [];
}
