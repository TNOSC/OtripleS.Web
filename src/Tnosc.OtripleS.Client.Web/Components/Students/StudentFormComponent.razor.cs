// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Tnosc.Lib.Client.Web.Bases;
using Tnosc.Lib.Client.Web.Enums;

namespace Tnosc.OtripleS.Client.Web.Components.Students;

#pragma warning disable CA1515
public partial class StudentFormComponent : ComponentBase
{
    public TextBoxBase StudentNameTextBox { get; set; } = null!;
    public ComponentState State { get; set; }

    protected override void OnInitialized() =>
        State = ComponentState.Content;
}

#pragma warning restore CA1515
