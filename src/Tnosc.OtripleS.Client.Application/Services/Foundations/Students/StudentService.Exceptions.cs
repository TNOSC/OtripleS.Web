// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnosc.OtripleS.Client.Application.Exceptions.Foundations.Students;
using Tnosc.OtripleS.Client.Domain.Students;
using Xeptions;

namespace Tnosc.OtripleS.Client.Application.Services.Foundations.Students;

public partial class StudentService
{
    private delegate ValueTask<Student> ReturningStudentFunction();
    private delegate ValueTask<IEnumerable<Student>> ReturningStudentsFunction();

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
        catch (FailedStudentCriticalDependencyException failedStudentCriticalDependencyException)
        {
            throw CreateAndLogCriticalDependencyException(failedStudentCriticalDependencyException);
        }
        catch (FailedStudentDependencyException failedStudentDependencyException)
        {
            throw CreateAndLogDependencyException(failedStudentDependencyException);
        }
        catch (InvalidStudentDependencyException invalidStudentDependencyException)
        {
            throw CreateAndLogDependencyValidationException(invalidStudentDependencyException);
        }
        catch (AlreadyExistsStudentException alreadyExistsStudentException)
        {
            throw CreateAndLogDependencyValidationException(alreadyExistsStudentException);
        }
        catch (Exception exception)
        {
            var failedStudentServiceException =
                  new FailedStudentServiceException(
                      message: "Failed student service error occurred, contact support.",
                      innerException: exception);
            throw CreateAndLogServiceException(failedStudentServiceException);
        }
    }

    private async ValueTask<IEnumerable<Student>> TryCatch(ReturningStudentsFunction returningStudentsFunction)
    {
        try
        {
            return await returningStudentsFunction();
        }
        catch (FailedStudentCriticalDependencyException failedStudentCriticalDependencyException)
        {
            throw CreateAndLogCriticalDependencyException(failedStudentCriticalDependencyException);
        }
        catch (FailedStudentDependencyException failedStudentDependencyException)
        {
            throw CreateAndLogDependencyException(failedStudentDependencyException);
        }
    }

    private StudentServiceException CreateAndLogServiceException(Xeption exception)
    {
        var studentServiceException = new StudentServiceException(
           message: "Service error occurred, contact support.",
           innerException: exception);
        _loggingBroker.LogError(exception: studentServiceException);

        return studentServiceException;
    }

    private StudentDependencyException CreateAndLogDependencyException(Xeption exception)
    {
        var studentDependencyException = new StudentDependencyException(
           message: "Student dependency error occurred, please contact support.",
           innerException: exception);
        _loggingBroker.LogError(exception: studentDependencyException);

        return studentDependencyException;
    }

    private StudentDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
    {
        var studentDependencyException = new StudentDependencyException(
          message: "Student dependency error occurred, please contact support.",
          innerException: exception);
        _loggingBroker.LogCritical(exception: studentDependencyException);

        return studentDependencyException;
    }

    private StudentDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
    {
        var studentDependencyValidationException = new StudentDependencyValidationException(
            message: "Student dependency validation error occurred, try again.",
            innerException: exception);
        _loggingBroker.LogError(exception: studentDependencyValidationException);

        return studentDependencyValidationException;
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
