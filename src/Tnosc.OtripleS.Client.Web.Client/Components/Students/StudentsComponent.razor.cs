// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Tnosc.Lib.Client.Web.Bases.Breadcrumbs;
using Tnosc.Lib.Client.Web.Bases.Components;
using Tnosc.Lib.Client.Web.Bases.Forms;
using Tnosc.Lib.Client.Web.Bases.Grids;
using Tnosc.Lib.Client.Web.Enums;
using Tnosc.OtripleS.Client.Application.Services.Views.Students;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;

namespace Tnosc.OtripleS.Client.Web.Client.Components.Students;

#pragma warning disable CA1515
public partial class StudentsComponent : AppViewComponent
{
    [Inject]
    public IStudentViewService StudentViewService { get; set; } = null!;

    public IEnumerable<StudentView> StudentViews { get; set; } = null!;
    public FluentDataGrid<StudentView> StudentGrid { get; set; } = null!;
    public string? ErrorMessage { get; set; }
    public LabelBase? ErrorLabel { get; set; }

    private readonly IEnumerable<BreadcrumbItem> BreadcrumbItems =
    [
        new BreadcrumbItem
        {
            Text = "Home",
            Href = "/",
            Icon = new BreadcrumbIcon { Value = new Microsoft.FluentUI.AspNetCore.Components.Icons.Regular.Size20.Home() }
        },
        new BreadcrumbItem
        {
            Text = "Students",
            Icon = new BreadcrumbIcon { Value = new Microsoft.FluentUI.AspNetCore.Components.Icons.Regular.Size20.Person() }
        }
    ];

    private bool IsLoading => StudentViews == null || !StudentViews.Any();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await LoadStudentViewsAsync();
            State = ComponentState.Content;
        }
        catch (Exception exception)
        {
            ErrorMessage = exception.Message;
            State = ComponentState.Error;
        }
    }

    private async Task OnRegisterStudentClick()
    {
        DialogResult result = await ShowPanel<StudentRegistrationComponent>();
        if (result.Data != null)
        {
            await LoadStudentViewsAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task LoadStudentViewsAsync() 
        => StudentViews = await StudentViewService.RetrieveAllStudentViewsAsync();
}
#pragma warning restore CA1515
