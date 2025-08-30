// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tnosc.OtripleS.Client.Application.Brokers.Apis;
using Tnosc.OtripleS.Client.Application.Brokers.DateTimes;
using Tnosc.OtripleS.Client.Application.Brokers.Loggings;
using Tnosc.OtripleS.Client.Infrastructure.Brokers.Apis;
using Tnosc.OtripleS.Client.Infrastructure.Brokers.DateTimes;
using Tnosc.OtripleS.Client.Infrastructure.Brokers.Loggings;

namespace Tnosc.OtripleS.Client.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddBrokers(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddScoped<ILogger, Logger<LoggingBroker>>();
        services.AddScoped<ILoggingBroker, LoggingBroker>();
        services.AddScoped<IDateTimeBroker, DateTimeBroker>();
        services.AddScoped<IApiBroker, ApiBroker>();
    }
}
