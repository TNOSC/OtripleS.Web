// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Tnosc.OtripleS.Client.Domain.Students;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Services.Views.Students;

public partial class StudentViewServiceTests
{
    [Fact]
    public async Task ShouldRetrieveAllStudentViewAsync()
    {
        // given
        IEnumerable<dynamic> dynamicStudentViewPropertiesCollection =
            [.. CreateRandomStudentViewCollections()];

        IEnumerable<Student> randomStudents =
            dynamicStudentViewPropertiesCollection.Select(property =>
                new Student
                {
                    Id = property.Id,
                    UserId = property.UserId,
                    IdentityNumber = property.IdentityNumber,
                    FirstName = property.FirstName,
                    MiddleName = property.MiddleName,
                    LastName = property.LastName,
                    Gender = property.Gender,
                    BirthDate = property.BirthDate,
                    CreatedBy = property.CreatedBy,
                    CreatedDate = property.CreatedDate,
                    UpdatedBy = property.UpdatedBy,
                    UpdatedDate = property.UpdatedDate,
                }
            );

        IEnumerable<Student> retrievedStudents = randomStudents;

        IEnumerable<StudentView> randomStudentViews =
            dynamicStudentViewPropertiesCollection.Select(property =>
                new StudentView
                {
                    IdentityNumber = property.IdentityNumber,
                    FirstName = property.FirstName,
                    MiddleName = property.MiddleName,
                    LastName = property.LastName,
                    Gender = property.GenderView,
                    BirthDate = property.BirthDate,
                }
            );

        IEnumerable<StudentView> expectedStudentViews = randomStudentViews;

        _studentServiceMock.RetrieveAllStudentsAsync()
            .Returns(returnThis: retrievedStudents);

        // when
        IEnumerable<StudentView> actualStudentViews =
            await _studentViewService.RetrieveAllStudentViewsAsync();

        // then
        actualStudentViews.ShouldBeEquivalentTo(expected: expectedStudentViews);

        await _studentServiceMock.Received(1).RetrieveAllStudentsAsync();
        _studentServiceMock.ReceivedCalls().Count().ShouldBe(expected: 1);

        _loggingBrokerMock.ReceivedCalls().ShouldBeEmpty();
        _dateTimeBrokerMock.ReceivedCalls().ShouldBeEmpty();
        _userServiceMock.ReceivedCalls().ShouldBeEmpty();
    }
}
