// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Tnosc.OtripleS.Client.Application.Exceptions.Foundations.Students;
using Tnosc.OtripleS.Client.Domain.Students;

namespace Tnosc.OtripleS.Client.Application.Services.Foundations.Students;

public partial class StudentService
{
    private static void ValidateStudentOnRegister(Student student) => 
        ValidateStudentIsNotNull(student);

    private static void ValidateStudentIsNotNull(Student student)
    {
        if (student is null)
        {
            throw new NullStudentException(message: "The student is null.");
        }
    }
}
