using System.Collections.Generic;
using System.Threading.Tasks;
using Force.DeepCloner;
using NSubstitute;
using Shouldly;
using Tnosc.OtripleS.Client.Domain.Students;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Foundations.Students;

public partial class StudentServiceTests
{
    [Fact]
    public async Task ShouldRetrieveAllStudentsAsync()
    {
        // given
        IEnumerable<Student> randomStudents = CreateRandomStudents();
        IEnumerable<Student> retrievedStudents = randomStudents;
        IEnumerable<Student> expectedStudents = retrievedStudents.DeepClone();

        _apiBrokerMock
            .GetAllStudentsAsync()
                .Returns(retrievedStudents);

        // when
        IEnumerable<Student> actualStudents =
               await _studentService.RetrieveAllStudentsAsync();

        // then
        actualStudents.ShouldBeEquivalentTo(expectedStudents);
        
        await _apiBrokerMock
            .Received(requiredNumberOfCalls: 1)
            .GetAllStudentsAsync();
        
        _loggingBrokerMock
            .ReceivedCalls()
            .ShouldBeEmpty();
    }
}
