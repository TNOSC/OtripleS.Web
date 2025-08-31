// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using NSubstitute;
using Tnosc.OtripleS.Client.Application.Brokers.Apis;
using Tnosc.OtripleS.Client.Application.Brokers.Loggings;
using Tnosc.OtripleS.Client.Application.Services.Foundations.Students;
using Tnosc.OtripleS.Client.Domain.Students;
using Tynamix.ObjectFiller;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Foundations;

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
            loggerBroker: _loggingBrokerMock);
    }

    private static Student CreateRandomStudent() =>
         CreateStudentFiller().Create();

    private static Filler<Student> CreateStudentFiller()
    {
        var filler = new Filler<Student>();

        filler.Setup()
            .OnType<DateTimeOffset>().Use(valueToUse: DateTimeOffset.UtcNow);

        return filler;
    }
}
