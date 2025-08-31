// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Tnosc.OtripleS.Client.Application.Brokers.Loggings;
using Tnosc.OtripleS.Client.Application.Exceptions.Foundations.Students;
using Tnosc.OtripleS.Client.Domain.Students;
using Xeptions;

namespace Tnosc.OtripleS.Client.Application.Services.Foundations.Students;

public partial class StudentService
{
    private delegate ValueTask<Student> ReturningStudentFunction();

    private async ValueTask<Student> TryCatch(ReturningStudentFunction returningStudentFunction)
    {
        try
        {
            return await returningStudentFunction();
        }
        catch (NullStudentException nullStudentException)
        {
            throw CreateAndLogValidationException(nullStudentException);
        }
        catch (InvalidStudentException invalidStudentException)
        {
            throw CreateAndLogValidationException(invalidStudentException);
        }
    }

    private StudentValidationException CreateAndLogValidationException(Xeption exception)
    {
        var studentValidationException = new StudentValidationException(
            message: "Invalid input, fix the errors and try again.",
            innerException: exception);
        _loggingBroker.LogError(exception: studentValidationException);

        return studentValidationException;
    }
}
