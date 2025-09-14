// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using Tnosc.Lib.Client.Web.Exceptions;

namespace Tnosc.Lib.Client.Web.Bases.Components;

public partial class AppViewComponent
{
    private delegate void ReturningNothingFunction();

    private static void TryCatch(ReturningNothingFunction returningNothingFunction)
    {
        try
        {
            returningNothingFunction();
        }
        catch (InvalidAppViewComponentException invalidAppViewComponentException)
        {
            throw new AppViewComponentValidationException(
                message: "App View validation error occurred, try again.",
                innerException: invalidAppViewComponentException);
        }
        catch (Exception serviceException)
        {
            throw new AppViewComponentServiceException(
                message: "App View service error occured, contact support",
                innerException: serviceException);
        }
    }
}
