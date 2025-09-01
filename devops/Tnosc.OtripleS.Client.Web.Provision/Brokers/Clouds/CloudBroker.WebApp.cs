﻿// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Azure;
using Azure.ResourceManager;
using Azure.ResourceManager.AppService;
using Azure.ResourceManager.Resources;

namespace Tnosc.OtripleS.Server.Provision.Brokers.Clouds;

internal partial class CloudBroker
{
    public async ValueTask<WebSiteResource> CreateWebAppAsync(
        string webAppName,
        AppServicePlanResource plan,
        ResourceGroupResource resourceGroup)
    {
        WebSiteCollection webAppCollection = resourceGroup.GetWebSites();

        var siteData = new WebSiteData(location: resourceGroup.Data.Location)
        {
            AppServicePlanId = plan.Id,
        };

        ArmOperation<WebSiteResource> operation = await webAppCollection.CreateOrUpdateAsync(
            waitUntil: WaitUntil.Completed,
            name: webAppName,
            data: siteData);

        return operation.Value;
    }
}
