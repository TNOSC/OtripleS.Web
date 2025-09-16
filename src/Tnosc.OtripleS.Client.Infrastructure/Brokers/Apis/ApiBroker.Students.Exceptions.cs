// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.WebAssembly.Exceptions;
using Tnosc.OtripleS.Client.Application.Exceptions.Foundations.Students;
using Tnosc.OtripleS.Client.Domain.Students;

namespace Tnosc.OtripleS.Client.Infrastructure.Brokers.Apis;

internal partial class ApiBroker
{
    private delegate ValueTask<Student> ReturningStudentFunction();
    private delegate ValueTask<IEnumerable<Student>> ReturningStudentsFunction();

    private static async ValueTask<Student> TryCatch(ReturningStudentFunction returningStudentFunction)
    {
        try
        {
            return await returningStudentFunction();
        }
        catch (HttpRequestException httpRequestException)
        {
            var failedStudentCriticalDependencyException =
                new FailedStudentCriticalDependencyException(
                    message: "Failed student critical dependency error occurred, please contact support.",
                    innerException: httpRequestException);

            throw failedStudentCriticalDependencyException;
        }
        catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
        {
            var failedStudentCriticalDependencyException =
                new FailedStudentCriticalDependencyException(
                    message: "Failed student critical dependency error occurred, please contact support.",
                    innerException: httpResponseUrlNotFoundException);

            throw failedStudentCriticalDependencyException;
        }
        catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
        {
            var failedStudentCriticalDependencyException =
                new FailedStudentCriticalDependencyException(
                    message: "Failed student critical dependency error occurred, please contact support.",
                    innerException: httpResponseUnauthorizedException);

            throw failedStudentCriticalDependencyException;
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
        catch (HttpResponseInternalServerErrorException httpResponseInternalServerErrorException)
        {
            var failedStudentDependencyException =
                new FailedStudentDependencyException(
                    message: "Failed student dependency error occurred, please contact support.",
                    innerException: httpResponseInternalServerErrorException);

            throw failedStudentDependencyException;
        }
        catch (HttpResponseException httpResponseException)
        {
            var failedStudentDependencyException =
                new FailedStudentDependencyException(
                    message: "Failed student dependency error occurred, please contact support.",
                    innerException: httpResponseException);

            throw failedStudentDependencyException;
        }
    }


    private static async ValueTask<IEnumerable<Student>> TryCatch(ReturningStudentsFunction returningStudentsFunction)
    {
        try
        {
            return await returningStudentsFunction();
        }
        catch (HttpRequestException httpRequestException)
        {
            var failedStudentCriticalDependencyException =
                new FailedStudentCriticalDependencyException(
                    message: "Failed student critical dependency error occurred, please contact support.",
                    innerException: httpRequestException);

            throw failedStudentCriticalDependencyException;
        }
        catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
        {
            var failedStudentCriticalDependencyException =
                new FailedStudentCriticalDependencyException(
                    message: "Failed student critical dependency error occurred, please contact support.",
                    innerException: httpResponseUrlNotFoundException);

            throw failedStudentCriticalDependencyException;
        }
        catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
        {
            var failedStudentCriticalDependencyException =
                new FailedStudentCriticalDependencyException(
                    message: "Failed student critical dependency error occurred, please contact support.",
                    innerException: httpResponseUnauthorizedException);

            throw failedStudentCriticalDependencyException;
        }
        catch (HttpResponseInternalServerErrorException httpResponseInternalServerErrorException)
        {
            var failedStudentDependencyException =
                new FailedStudentDependencyException(
                    message: "Failed student dependency error occurred, please contact support.",
                    innerException: httpResponseInternalServerErrorException);

            throw failedStudentDependencyException;
        }
        catch (HttpResponseException httpResponseException)
        {
            var failedStudentDependencyException =
                new FailedStudentDependencyException(
                    message: "Failed student dependency error occurred, please contact support.",
                    innerException: httpResponseException);

            throw failedStudentDependencyException;
        }
    }
}
