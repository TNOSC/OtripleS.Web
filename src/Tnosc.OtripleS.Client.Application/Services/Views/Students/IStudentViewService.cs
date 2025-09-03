// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Tnosc.OtripleS.Client.Application.ViewModels.Students;

namespace Tnosc.OtripleS.Client.Application.Services.Views.Students;

public interface IStudentViewService
{
    ValueTask<StudentView> RegisterStudentViewAsync(StudentView studentView);
}
