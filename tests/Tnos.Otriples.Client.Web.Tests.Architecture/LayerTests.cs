// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Reflection;

namespace Tnos.Otriples.Client.Web.Tests.Architecture;

public partial class LayerTests
{
    private readonly Assembly _domainAssembly = typeof(Tnosc.OtripleS.Client.Domain.AssemblyReference).Assembly;
    private readonly Assembly _applicationAssembly = typeof(Tnosc.OtripleS.Client.Application.AssemblyReference).Assembly;
    private readonly Assembly _infrastructureAssembly = typeof(Tnosc.OtripleS.Client.Infrastructure.AssemblyReference).Assembly;
    private readonly Assembly _presentationAssembly = typeof(Tnosc.OtripleS.Client.Web.AssemblyReference).Assembly;
}
