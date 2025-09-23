// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Tnosc.Lib.Client.Web.Bases.Grids;

public partial class GridBase<TItem> : ComponentBase
{
    [Parameter]
    public IEnumerable<TItem>? DataSource { get; set; }

    [Parameter]
    public RenderFragment? Columns { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (!ReferenceEquals(DataSource, _lastDataSource))
        {
            _lastDataSource = DataSource;
            await InvokeAsync(StateHasChanged);
        }
    }

    private readonly PaginationState pagination = new() { ItemsPerPage = 10 };

    private IQueryable<TItem> PagedData =>
        (DataSource ?? Enumerable.Empty<TItem>())
            .Skip(pagination.CurrentPageIndex * pagination.ItemsPerPage)
            .Take(pagination.ItemsPerPage)
            .AsQueryable();

    private bool IsLoading => DataSource == null || !DataSource.Any();

    private IEnumerable<TItem>? _lastDataSource;
}
