// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Cards;

public partial class CardBase : ComponentBase
{
    [Parameter]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public string SubTitle { get; set; } = string.Empty;

    [Parameter]
    public required RenderFragment Content { get; set; }

    [Parameter]
    public RenderFragment? Footer { get; set; }
}
