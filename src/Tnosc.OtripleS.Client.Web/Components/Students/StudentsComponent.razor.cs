// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tnosc.Lib.Client.Web.Bases.Components;
using Tnosc.Lib.Client.Web.Bases.Forms;
using Tnosc.Lib.Client.Web.Bases.Grids;
using Tnosc.Lib.Client.Web.Enums;
using Tnosc.OtripleS.Client.Application.Services.Views.Students;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;

namespace Tnosc.OtripleS.Client.Web.Components.Students;

public partial class StudentsComponent : AppViewComponent
{
    [Inject]
    public IStudentViewService StudentViewService { get; set; } = null!;

    public IEnumerable<StudentView> StudentViews { get; set; } = null!;
    public GridBase<StudentView> StudentGrid { get; set; } = null!;
    public string? ErrorMessage { get; set; }
    public LabelBase? ErrorLabel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            StudentViews =
            await StudentViewService.RetrieveAllStudentViewsAsync();

            State = ComponentState.Content;
        }
        catch (Exception exception)
        {
            ErrorMessage = exception.Message;
            State = ComponentState.Error;
        }
       
    }
}


