// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Net.Http;
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
        catch (HttpRequestException httpRequestException)
        {
            var failedStudentDependencyException =
                new FailedStudentDependencyException(
                    message: "Failed student dependency error occurred, please contact support.",
                    innerException: httpRequestException);

            throw failedStudentDependencyException;
        }
        catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
        {
            var failedStudentDependencyException =
                new FailedStudentDependencyException(
                    message: "Failed student dependency error occurred, please contact support.",
                    innerException: httpResponseUrlNotFoundException);

            throw failedStudentDependencyException;
        }
        catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
        {
            var failedStudentDependencyException =
                new FailedStudentDependencyException(
                    message: "Failed student dependency error occurred, please contact support.",
                    innerException: httpResponseUnauthorizedException);

            throw failedStudentDependencyException;
        }
        catch (HttpResponseBadRequestException httpResponseBadRequestException)
        {
            var invalidStudentDependencyException =
                new InvalidStudentDependencyException(
                    message: "Invalid input, fix the errors and try again.",
                    innerException: httpResponseBadRequestException);
            
            throw invalidStudentDependencyException;
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
