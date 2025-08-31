// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Tnosc.OtripleS.Client.Domain.Students;

namespace Tnosc.OtripleS.Client.Infrastructure.Brokers.Apis;

internal partial class ApiBroker
{
    private const string StudentsRelativeUrl = "api/students";

    public async ValueTask<Student> PostStudentAsync(Student student) =>
        await PostAsync(StudentsRelativeUrl, student);
}
