// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Tnosc.OtripleS.Client.Application.Brokers.Apis;
using Tnosc.OtripleS.Client.Application.Brokers.Loggings;
using Tnosc.OtripleS.Client.Domain.Students;

namespace Tnosc.OtripleS.Client.Application.Services.Foundations.Students;

public partial class StudentService : IStudentService
{
#pragma warning disable S4487 // Unread "private" fields should be removed
    private readonly IApiBroker _apiBroker;
    private readonly ILoggingBroker _loggerBroker;
#pragma warning restore S4487 // Unread "private" fields should be removed

    public StudentService(
        IApiBroker apiBroker,
        ILoggingBroker loggerBroker)
    {
        _apiBroker = apiBroker;
        _loggerBroker = loggerBroker;
    }

    public ValueTask<Student> RegisterStudentAsync(Student student) =>
        throw new System.NotImplementedException();
}
