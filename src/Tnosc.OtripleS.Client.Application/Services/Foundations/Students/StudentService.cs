// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Tnosc.OtripleS.Client.Application.Brokers.Apis;
using Tnosc.OtripleS.Client.Application.Brokers.Loggings;
using Tnosc.OtripleS.Client.Domain.Students;

namespace Tnosc.OtripleS.Client.Application.Services.Foundations.Students;

public partial class StudentService : IStudentService
{
    private readonly IApiBroker _apiBroker;
    private readonly ILoggingBroker _loggingBroker;

    public StudentService(
        IApiBroker apiBroker,
        ILoggingBroker loggingBroker)
    {
        _apiBroker = apiBroker;
        _loggingBroker = loggingBroker;
    }

    public async ValueTask<Student> RegisterStudentAsync(Student student) =>
    await TryCatch(async () =>
    {
        ValidateStudentOnRegister(student);
        return await _apiBroker.PostStudentAsync(student: student);
    });

    public async ValueTask<IEnumerable<Student>> RetrieveAllStudentsAsync() =>
        await TryCatch(async () =>
            await _apiBroker.GetAllStudentsAsync());
}
