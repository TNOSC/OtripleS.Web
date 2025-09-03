// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Tnosc.OtripleS.Client.Application.Services.Foundations.Students;
using Tnosc.OtripleS.Client.Application.Services.Foundations.Users;
using Tnosc.OtripleS.Client.Application.Services.Views.Students;

namespace Tnosc.OtripleS.Client.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStudentViewService, StudentViewService>();
    }
}
