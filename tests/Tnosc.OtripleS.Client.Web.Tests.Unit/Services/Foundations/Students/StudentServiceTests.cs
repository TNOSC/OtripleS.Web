// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using NSubstitute;
using RESTFulSense.WebAssembly.Exceptions;
using Tnosc.OtripleS.Client.Application.Brokers.Apis;
using Tnosc.OtripleS.Client.Application.Brokers.Loggings;
using Tnosc.OtripleS.Client.Application.Services.Foundations.Students;
using Tnosc.OtripleS.Client.Domain.Students;
using Tynamix.ObjectFiller;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Foundations.Students;

public partial class StudentServiceTests
{
    private readonly IApiBroker _apiBrokerMock;
    private readonly ILoggingBroker _loggingBrokerMock;
    private readonly StudentService _studentService;

    public StudentServiceTests()
    {
        _apiBrokerMock = Substitute.For<IApiBroker>();
        _loggingBrokerMock = Substitute.For<ILoggingBroker>();

        _studentService = new StudentService(
            apiBroker: _apiBrokerMock,
            loggingBroker: _loggingBrokerMock);
    }

    public static TheoryData CriticalApiException()
    {
        string exceptionMessage = GetRandomString();
        var responseMessage = new HttpResponseMessage();

        var httpRequestException =
            new HttpRequestException();

        var httpResponseUrlNotFoundException =
            new HttpResponseUrlNotFoundException(
                responseMessage: responseMessage,
                message: exceptionMessage);

        var httpResponseUnAuthorizedException =
            new HttpResponseUnauthorizedException(
                responseMessage: responseMessage,
                message: exceptionMessage);

        return new TheoryData<Exception>
            {
                httpRequestException,
                httpResponseUrlNotFoundException,
                httpResponseUnAuthorizedException
            };
    }

    public static TheoryData DependencyApiException()
    {
        string exceptionMessage = GetRandomString();
        var responseMessage = new HttpResponseMessage();

        var httpResponseException =
            new HttpResponseException(
                httpResponseMessage: responseMessage,
                message: exceptionMessage);

        var httpResponseInternalServerErrorException =
            new HttpResponseInternalServerErrorException(
                responseMessage: responseMessage,
                message: exceptionMessage);

        return new TheoryData<Exception>
            {
                httpResponseException,
                httpResponseInternalServerErrorException
            };
    }

   

    private static Student CreateRandomStudent() =>
         CreateStudentFiller().Create();

    private static IEnumerable<Student> CreateRandomStudents() =>
           CreateStudentFiller().Create(count: GetRandomNumber());
   
    private static Filler<Student> CreateStudentFiller()
    {
        var filler = new Filler<Student>();

        filler.Setup()
            .OnType<DateTimeOffset>().Use(valueToUse: DateTimeOffset.UtcNow);

        return filler;
    }

    private static int GetRandomNumber() =>
       new IntRange(min: 2, max: 10).GetValue();

    private static string GetRandomString() =>
       new MnemonicString().GetValue();
}
