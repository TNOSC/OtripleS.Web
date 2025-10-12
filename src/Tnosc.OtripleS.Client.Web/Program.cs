// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Tnosc.OtripleS.Client.Application;
using Tnosc.OtripleS.Client.Infrastructure;
using Tnosc.OtripleS.Client.Web.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.ConfigureOpenTelemetry();
builder.AddServiceDefaults();
builder.Services.AddComponents();
builder.Services.AddApplicationServices();
builder.Services.AddBrokers();
builder.Services.AddShared();

WebApplication app = builder.Build();

app.MapDefaultEndpoints();

await app.RunAsync();
