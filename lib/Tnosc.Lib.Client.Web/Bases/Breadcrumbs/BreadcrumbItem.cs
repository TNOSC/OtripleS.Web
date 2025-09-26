// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

namespace Tnosc.Lib.Client.Web.Bases.Breadcrumbs;

public class BreadcrumbItem
{
    public string Text { get; set; } = string.Empty;
    public string? Href { get; set; }
    public BreadcrumbIcon? Icon { get; set; }
}
