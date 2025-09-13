// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using Bunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tnosc.OtripleS.Client.Application.Exceptions.Views.Students;
using Tnosc.OtripleS.Client.Application.Services.Views.Students;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;
using Tnosc.OtripleS.Client.Web.Client.Components.Students;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace Tnosc.OtripleS.Client.Web.Tests.Unit.Components.Students;

public partial class StudentRegistrationComponentTests : TestContext
{
    private readonly IStudentViewService _studentViewServiceMock;
    private IRenderedComponent<StudentRegistrationComponent> _renderedStudentRegistrationComponent = null!;

    public StudentRegistrationComponentTests()
    {
        _studentViewServiceMock = Substitute.For<IStudentViewService>();
        IWebHostEnvironment envMock = Substitute.For<IWebHostEnvironment>();
        IConfiguration configMock = Substitute.For<IConfiguration>();
        Services.AddScoped(services => _studentViewServiceMock);
        Services.AddSingleton(envMock);
        Services.AddSingleton(configMock);
        Services.AddServerSideBlazor();
    }

    public static TheoryData StudentViewValidationExceptions()
    {
        string randomMessage = GetRandomString();
        string validationMessage = randomMessage;
        var innerValidationException = new Xeption(message: validationMessage);

        return new TheoryData<Exception>
        {
            new StudentViewValidationException(
                message: "Invalid input, fix the errors and try again.",
                innerException: innerValidationException),
            new StudentViewDependencyValidationException(
                message : "Student view dependency validation error occurred, try again.",
                innerException: innerValidationException)
        };
    }

    private static string GetRandomString() =>
        new MnemonicString().GetValue();

    private static StudentView CreateRandomStudentView() =>
          CreateStudentFiller().Create();

    private static Filler<StudentView> CreateStudentFiller()
    {
        var filler = new Filler<StudentView>();

        filler.Setup()
            .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

        return filler;
    }

}
