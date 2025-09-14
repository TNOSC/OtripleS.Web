// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tnosc.Lib.Client.Web.Bases.Components;
using Tnosc.Lib.Client.Web.Bases.Forms;
using Tnosc.Lib.Client.Web.Enums;
using Tnosc.OtripleS.Client.Application.Exceptions.Views.Students;
using Tnosc.OtripleS.Client.Application.Services.Views.Students;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Tnosc.OtripleS.Client.Web.Client.Exceptions.Students;

namespace Tnosc.OtripleS.Client.Web.Client.Components.Students;

#pragma warning disable CA1515
public partial class StudentRegistrationComponent : AppViewComponent
{
    [Inject]
    public IStudentViewService StudentViewService { get; set; } = null!;
   
    public StudentRegistrationComponentException? Exception { get; set; }
    public StudentView StudentView { get; set; } = null!;
    public TextBoxBase StudentIdentityTextBox { get; set; } = null!;
    public TextBoxBase StudentFirstNameTextBox { get; set; } = null!;
    public TextBoxBase StudentMiddleNameTextBox { get; set; } = null!;
    public TextBoxBase StudentLastNameTextBox { get; set; } = null!;
    public DropDownBase<StudentViewGender> StudentGenderDropDown { get; set; } = null!;
    public DatePickerBase DateOfBirthPicker { get; set; } = null!;
    public ButtonBase SubmitButton { get; set; } = null!;
    public LabelBase StatusLabel { get; set; } = null!;

    protected override void OnInitialized()
    {
        StudentView = new StudentView();
        State = ComponentState.Content;
    }

    public async Task RegisterStudentAsync()
    {
        try
        {
            ApplySubmittingStatus();
            await StudentViewService.RegisterStudentViewAsync(studentView: StudentView);
            NavigateToStudentSubmittedPage();
        }
        catch (StudentViewValidationException studentViewValidationException)
        {
            string validationMessage =
                studentViewValidationException.InnerException!.Message;

            ApplySubmissionFailed(errorMessage: validationMessage);
        }
        catch (StudentViewDependencyValidationException dependencyValidationException)
        {
            string validationMessage =
                dependencyValidationException.InnerException!.Message;

            ApplySubmissionFailed(errorMessage: validationMessage);
        }
        catch (StudentViewServiceException studentViewServiceException)
        {
            string errorMessage =
                studentViewServiceException.Message;

            ApplySubmissionFailed(errorMessage: errorMessage);
        }
        catch (StudentViewDependencyException dependencyException)
        {
            string errorMessage =
                dependencyException.Message;

            ApplySubmissionFailed(errorMessage: errorMessage);
        }
    }

    private void ApplySubmittingStatus()
    {
        StatusLabel.SetColor(Color.Black);
        StatusLabel.SetValue("Submitting ... ");
        StudentIdentityTextBox.Disable();
        StudentFirstNameTextBox.Disable();
        StudentMiddleNameTextBox.Disable();
        StudentLastNameTextBox.Disable();
        StudentGenderDropDown.Disable();
        DateOfBirthPicker.Disable();
        SubmitButton.Disable();
        SubmitButton.SetLoading(true);
    }

    private void NavigateToStudentSubmittedPage() =>
        NavigateTo("/studentsubmitted");

    private void ApplySubmissionFailed(string errorMessage)
    {
        StatusLabel.SetColor(Color.Red);
        StatusLabel.SetValue(errorMessage);
        StudentIdentityTextBox.Enable();
        StudentFirstNameTextBox.Enable();
        StudentMiddleNameTextBox.Enable();
        StudentLastNameTextBox.Enable();
        StudentGenderDropDown.Enable();
        DateOfBirthPicker.Enable();
        SubmitButton.Enable();
        SubmitButton.SetLoading(false);
    }
}
#pragma warning restore CA1515
