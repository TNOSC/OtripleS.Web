// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Tnosc.Lib.Client.Web.Exceptions;

public class InvalidAppViewComponentException : Xeption
{
    public InvalidAppViewComponentException(string parameterName, object parameterValue)
        : base($"Invalid App View error occured. " +
             $"parameter name: {parameterName}, " +
             $"parameter value: {parameterValue}")
    { }
}

