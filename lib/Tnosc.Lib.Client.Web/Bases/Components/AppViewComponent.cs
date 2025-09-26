// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Tnosc.Lib.Client.Web.Brokers.Navigations;
using Tnosc.Lib.Client.Web.Enums;

namespace Tnosc.Lib.Client.Web.Bases.Components;

public partial class AppViewComponent : ComponentBase
{
    [Inject]
    protected INavigationBroker NavigationBroker { get; set; } = default!;

    [Inject]
    public required IDialogService DialogService { get; set; }

    [Inject]
    public required IToastService ToastService { get; set; }

    public ComponentState State { get; set; }

    protected enum Operation
    {
        Add,
        Update,
        Delete
    };

    public void NavigateTo(string route) =>
    TryCatch(() =>
    {
        ValidateRoute(route);
        NavigationBroker.NavigateTo(route: route);
    });

    protected IDialogReference? Dialog { get; set; }

    protected async Task<DialogResult> ShowPanel<TComponent>()
        where TComponent : IDialogContentComponent
    {
        var dialogParameter = new DialogParameters()
        {
            Alignment = HorizontalAlignment.Right,
            PrimaryAction = string.Empty,
            SecondaryAction = string.Empty,
            PreventDismissOnOverlayClick = false,
        };
        Dialog = await DialogService.ShowPanelAsync<TComponent>(dialogParameter);
        return await Dialog.Result;
    }

    protected void ShowProgressToast(string id, string entityType, string entityName, Operation operation = Operation.Add)
    {
        string sentenceCasedEntityTypeName = ToSentenceCase(entityType);
        ToastService.ShowProgressToast(new ToastParameters<ProgressToastContent>
        {
            Id = id,
            Intent = ToastIntent.Progress,
            Title = PrepareProgressToastTitle(operation, sentenceCasedEntityTypeName),
            Content = new ProgressToastContent
            {
                Details = PrepareProgressToastMessage(entityName, operation),
            }
        });
    }

    protected void CloseProgressToast(string id) 
        => ToastService.CloseToast(id);

    protected void ShowSuccessToast(string entityType, string entityName, Operation operation = Operation.Add)
    {
        string sentenceCasedEntityTypeName = ToSentenceCase(entityType);
        string title = PrepareSuccessToastTitle(operation, sentenceCasedEntityTypeName);
        string message = PrepareSuccessToastMessage(entityName, operation, sentenceCasedEntityTypeName);
        ToastService.ShowCommunicationToast(new ToastParameters<CommunicationToastContent>
        {
            Intent = ToastIntent.Success,
            Title = title,
            Timeout = 5000,
            Content = new CommunicationToastContent
            {
                Details = message
            }
        });
    }

    protected void ShowFailureToast(string entityType, string entityName, Operation operation = Operation.Add, string failureMessage = "")
    {
        string sentenceCasedEntityTypeName = ToSentenceCase(entityType);
        string title = PrepareFailureToastTitle(operation, sentenceCasedEntityTypeName);
        string message = string.IsNullOrEmpty(failureMessage) ? PrepareFailureToastMessage(entityName, operation, sentenceCasedEntityTypeName)
                                                     : failureMessage;
        ToastService.ShowCommunicationToast(new ToastParameters<CommunicationToastContent>
        {
            Intent = ToastIntent.Error,
            Title = title,
            Timeout = 5000,
            Content = new CommunicationToastContent
            {
                Details = message
            }
        });
    }

    private static string PrepareFailureToastTitle(Operation operation, string entityName) 
        => $"{operation switch
    {
        Operation.Add => "Adding",
        Operation.Update => "Updating",
        Operation.Delete => "Deleting",
        _ => "Add/Update/Delete action on "
    }} {entityName} failed";

    private static string PrepareFailureToastMessage(string entityName, Operation operation, string entityType) 
        => $"Error {entityType}: {entityName} was {operation switch
    {
        Operation.Add => "created",
        Operation.Update => "updated",
        Operation.Delete => "deleted",
        _ => "created/updated/deleted"
    }} successfully";

    private static string PrepareSuccessToastTitle(Operation operation, string entityType) 
        => $"{entityType} {operation switch
    {
        Operation.Add => "created",
        Operation.Update => "updated",
        Operation.Delete => "deleted",
        _ => "created/updated/deleted"
    }} successfully";

    private static string PrepareSuccessToastMessage(string entityName, Operation operation, string entityType) 
        => $"{entityType}: {entityName} was {operation switch
    {
        Operation.Add => "created",
        Operation.Update => "updated",
        Operation.Delete => "deleted",
        _ => throw new InvalidOperationException("Invalid operation")
    }} successfully";

    private static string PrepareProgressToastTitle(Operation operation, string sentenceCasedEntityTypeName) 
        => $"{operation switch
    {
        Operation.Add => "Creating",
        Operation.Update => "Updating",
        Operation.Delete => "Deleting",
        _ => "Creating/Updating/Deleting"
    }} {sentenceCasedEntityTypeName}";

    private static string PrepareProgressToastMessage(string entityName, Operation operation) 
        => $"{operation switch
    {
        Operation.Add => "Creating",
        Operation.Update => "Updating",
        Operation.Delete => "Deleting",
        _ => "Creating/Updating/Deleting"
    }} {entityName}. Please wait...";

    private static string ToSentenceCase(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        string lowerCase = str.ToLower(System.Globalization.CultureInfo.CurrentCulture);
        return char.ToUpper(lowerCase[0], System.Globalization.CultureInfo.CurrentCulture) + lowerCase.Substring(1);
    }
}
