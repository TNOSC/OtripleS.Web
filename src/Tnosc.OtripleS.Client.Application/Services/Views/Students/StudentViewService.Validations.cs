// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Tnosc.OtripleS.Client.Application.Exceptions.Views.Students;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;

namespace Tnosc.OtripleS.Client.Application.Services.Views.Students;

public partial class StudentViewService
{
    private static void ValidateStudentView(StudentView studentView)
    {
        if (studentView is null)
        {
            throw new NullStudentViewException( message: "The student is null.");
        }
    }
}
