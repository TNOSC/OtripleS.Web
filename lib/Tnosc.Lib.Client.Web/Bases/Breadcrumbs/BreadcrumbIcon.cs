// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.FluentUI.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Breadcrumbs;

public class BreadcrumbIcon
{
    public Icon Value { get; set; } = null!;
    public Color Color { get; set; } = Color.Neutral;
    public string Slot { get; set; } = "start";
}
