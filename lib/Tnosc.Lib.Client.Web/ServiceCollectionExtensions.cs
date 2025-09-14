// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Tnosc.Lib.Client.Web.Navigations;

namespace Tnosc.OtripleS.Client.Application;

public static class ServiceCollectionExtensions
{
    public static void AddShared(this IServiceCollection services) =>
        services.AddScoped<INavigationBroker, NavigationBroker>();
}
