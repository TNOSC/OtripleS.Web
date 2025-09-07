// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Tnosc.OtripleS.Client.Application.Exceptions.Views.Students;

public class StudentViewDependencyValidationException : Xeption
{
    public StudentViewDependencyValidationException(
         string message,
         Xeption innerException)
         : base(
            message: message,
            innerException: innerException)
    { }
}
