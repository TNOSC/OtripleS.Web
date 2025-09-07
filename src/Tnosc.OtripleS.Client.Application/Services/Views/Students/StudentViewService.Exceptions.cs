// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Tnosc.OtripleS.Client.Application.Exceptions.Foundations.Students;
using Tnosc.OtripleS.Client.Application.Exceptions.Views.Students;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Xeptions;

namespace Tnosc.OtripleS.Client.Application.Services.Views.Students;

public partial class StudentViewService
{
    private delegate ValueTask<StudentView> ReturningStudentViewFunction();

    private async ValueTask<StudentView> TryCatch(ReturningStudentViewFunction returningStudentViewFunction)
    {
        try
        {
            return await returningStudentViewFunction();
        }
        catch (NullStudentViewException nullStudentViewException)
        {
            throw CreateAndLogValidationException(nullStudentViewException);
        }
        catch (StudentValidationException studentValidationException)
        {
            throw CreateAndLogDependencyValidationException(studentValidationException);
        }
        catch (StudentDependencyValidationException studentDependencyValidationException)
        {
            throw CreateAndLogDependencyValidationException(studentDependencyValidationException);
        }
    }

    private StudentViewValidationException CreateAndLogValidationException(Xeption exception)
    {
        var studentViewValidationException = new StudentViewValidationException(
            message: "Invalid input, fix the errors and try again.",
            innerException: exception);
        _loggingBroker.LogError(studentViewValidationException);

        return studentViewValidationException;
    }

    private StudentViewDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
    {
        var studentViewDependencyValidationException =
            new StudentViewDependencyValidationException(
                message: "Student view dependency validation error occurred, try again.",
                innerException: (exception.InnerException as Xeption)!);

        _loggingBroker.LogError(studentViewDependencyValidationException);

        return studentViewDependencyValidationException;
    }
}
