// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;

namespace Tnosc.OtripleS.Client.Web.Client.Exceptions.Students;

public class StudentRegistrationComponentException : Exception
{
    public StudentRegistrationComponentException(Exception innerException)
        : base(
            message: "Error occurred, contact support",
            innerException : innerException)
    { }
}
