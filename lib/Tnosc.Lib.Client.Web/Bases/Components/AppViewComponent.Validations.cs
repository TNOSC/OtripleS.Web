// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using Tnosc.Lib.Client.Web.Exceptions;

namespace Tnosc.Lib.Client.Web.Bases.Components;

public partial class AppViewComponent
{
    private static void ValidateRoute(string route)
    {
        if (IsInvalid(route))
        {
            throw new InvalidAppViewComponentException(
                parameterName: "Route",
                parameterValue: route);
        }
    }

    private static bool IsInvalid(string text) =>
        string.IsNullOrWhiteSpace(text);
}
