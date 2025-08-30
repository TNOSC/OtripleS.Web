// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using Tnosc.OtripleS.Client.Application.Brokers.DateTimes;

namespace Tnosc.OtripleS.Client.Infrastructure.Brokers.DateTimes;

internal sealed class DateTimeBroker : IDateTimeBroker
{
    public DateTimeOffset GetCurrentDateTime() =>
        DateTimeOffset.UtcNow;
}
