// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Tnosc.OtripleS.Client.Application.Exceptions.Foundations.Students;
using Tnosc.OtripleS.Client.Domain.Students;

namespace Tnosc.OtripleS.Client.Infrastructure.Brokers.Apis;

internal partial class ApiBroker
{
    private delegate ValueTask<Student> ReturningStudentFunction();

    private static async ValueTask<Student> TryCatch(ReturningStudentFunction returningStudentFunction)
    {
        try
        {
            return await returningStudentFunction();
        }
        catch (HttpResponseBadRequestException httpResponseBadRequestException)
        {
            var invalidStudentException =
                new InvalidStudentException(
                    message: "Invalid input, fix the errors and try again.",
                    innerException: httpResponseBadRequestException);
            
            throw invalidStudentException;
        }
        catch (HttpResponseConflictException httpResponseConfilictException)
        {
            var alreadyExistsStudentException =
                new AlreadyExistsStudentException(
                    message: "Student with the same id already exists.",
                    innerException: httpResponseConfilictException);

            throw alreadyExistsStudentException;
        }
    }
}
