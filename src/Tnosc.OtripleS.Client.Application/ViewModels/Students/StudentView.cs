// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;

namespace Tnosc.OtripleS.Client.Application.ViewModels.Students;

public sealed class StudentView
{
    public string IdentityNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    public StudentViewGender Gender { get; set; }
}
