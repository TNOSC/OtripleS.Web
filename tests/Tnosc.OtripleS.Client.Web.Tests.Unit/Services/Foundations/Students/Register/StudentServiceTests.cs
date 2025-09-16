// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Tnosc.OtripleS.Client.Domain.Students;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Foundations.Students;

public partial class StudentServiceTests
{
    [Fact]
    public async Task ShouldRegisterStudentAsync()
    {
        // given
        Student randomStudent = CreateRandomStudent();
        Student inputStudent = randomStudent;
        Student retrievedStudent = inputStudent;
        Student expectedStudent = retrievedStudent;

        _apiBrokerMock
            .PostStudentAsync(student: inputStudent)
                .Returns(returnThis: retrievedStudent);

        // when
        Student actualStudent =
            await _studentService.
                RegisterStudentAsync(student: inputStudent);

        // then
        actualStudent
            .ShouldBeEquivalentTo(expected: expectedStudent);

        await _apiBrokerMock
           .Received(requiredNumberOfCalls: 1)
           .PostStudentAsync(student: inputStudent);

        _apiBrokerMock
            .ReceivedCalls()
            .Count()
            .ShouldBe(expected: 1);

        _loggingBrokerMock
            .ReceivedCalls()
            .ShouldBeEmpty();
    }
}
