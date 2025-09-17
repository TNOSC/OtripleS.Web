// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Tnosc.OtripleS.Client.Domain.Students;

namespace Tnosc.OtripleS.Client.Application.Services.Foundations.Students;

public interface IStudentService
{
    ValueTask<Student> RegisterStudentAsync(Student student);
    ValueTask<IEnumerable<Student>> RetrieveAllStudentsAsync();
}
