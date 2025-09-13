// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tnosc.Lib.Client.Web.Bases.Forms;
using Tnosc.Lib.Client.Web.Enums;
using Tnosc.OtripleS.Client.Application.Exceptions.Views.Students;
using Tnosc.OtripleS.Client.Application.Services.Views.Students;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Tnosc.OtripleS.Client.Web.Client.Exceptions.Students;

namespace Tnosc.OtripleS.Client.Web.Client.Components.Students;

#pragma warning disable CA1515
public partial class StudentRegistrationComponent : ComponentBase
{
    [Inject]
    public IStudentViewService StudentViewService { get; set; } = null!;

    public ComponentState State { get; set; }
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
            await StudentViewService.RegisterStudentViewAsync(studentView: StudentView);

            ReportStudentSubmissionSuccesseded();
        }
        catch (StudentViewValidationException studentViewValidationException)
        {
            string validationMessage =
                studentViewValidationException.InnerException!.Message;

            ReportStudentSubmissionFailed(errorMessage: validationMessage);
        }
        catch (StudentViewDependencyValidationException dependencyValidationException)
        {
            string validationMessage =
                dependencyValidationException.InnerException!.Message;

            ReportStudentSubmissionFailed(errorMessage: validationMessage);
        }
        catch (StudentViewServiceException studentViewServiceException)
        {
            string validationMessage =
                studentViewServiceException.Message;

            ReportStudentSubmissionFailed(errorMessage: validationMessage);
        }
        catch (StudentViewDependencyException dependencyException)
        {
            string validationMessage =
                dependencyException.Message;

            ReportStudentSubmissionFailed(errorMessage: validationMessage);
        }

    }

    private void ReportStudentSubmissionSuccesseded()
    {
        StatusLabel.SetColor(Color.Green);
        StatusLabel.SetValue("Submitted Successfully.");
    }

    private void ReportStudentSubmissionFailed(string errorMessage)
    {
        StatusLabel.SetColor(Color.Red);
        StatusLabel.SetValue(errorMessage);
    }
}
#pragma warning restore CA1515
