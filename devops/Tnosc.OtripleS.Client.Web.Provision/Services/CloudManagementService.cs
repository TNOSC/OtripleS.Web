// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Azure.ResourceManager.AppService;
using Azure.ResourceManager.Resources;
using Tnosc.OtripleS.Server.Provision.Brokers.Clouds;
using Tnosc.OtripleS.Server.Provision.Brokers.Loggings;

namespace Tnosc.OtripleS.Server.Provision.Services;

internal sealed class CloudManagementService : ICloudManagementService
{
    private readonly ICloudBroker _cloudBroker;
    private readonly ILoggingBroker _loggingBroker;

    public CloudManagementService()
    {
        _cloudBroker = new CloudBroker();
        _loggingBroker = new LoggingBroker();
    }

    public async ValueTask<ResourceGroupResource> ProvisionResourceGroupAsync(
        string projectName,
        string environment)
    {
        string resourceGroupName = $"{projectName}-RESOURCES-{environment}".ToUpperInvariant();
        _loggingBroker.LogActivity(message: $"Provisioning {resourceGroupName}...");

        ResourceGroupResource resourceGroup =
            await _cloudBroker.CreateResourceGroupAsync(resourceGroupName: resourceGroupName);

        _loggingBroker.LogActivity(message: $"{resourceGroupName} Provisioned.");

        return resourceGroup;
    }

    public async ValueTask<AppServicePlanResource> ProvisionPlanAsync(
        string projectName,
        string environment,
        ResourceGroupResource resourceGroup)
    {
        string planName = $"{projectName}-PLAN-{environment}".ToUpperInvariant();
        _loggingBroker.LogActivity(message: $"Provisioning {planName}...");

        AppServicePlanResource plan =
            await _cloudBroker.CreatePlanAsync(
                planName: planName,
                resourceGroup: resourceGroup);

        _loggingBroker.LogActivity(message: $"{plan} Provisioned");

        return plan;
    }

    public async ValueTask<WebSiteResource> ProvisionWebAppAsync(
            string projectName,
            string environment,
            ResourceGroupResource resourceGroup,
            AppServicePlanResource appServicePlan)
    {
        string webAppName = $"{projectName}-WEBAPP-{environment}".ToUpperInvariant();
        _loggingBroker.LogActivity(message: $"Provisioning {webAppName}");

        WebSiteResource webApp =
            await _cloudBroker.CreateWebAppAsync(
                webAppName: webAppName,
                plan: appServicePlan,
                resourceGroup: resourceGroup);

        _loggingBroker.LogActivity(message: $"{webAppName} Provisioned");

        return webApp;
    }

    public async ValueTask DeprovisionResouceGroupAsync(
        string projectName,
        string environment)
    {
        string resourceGroupName = $"{projectName}-RESOURCES-{environment}".ToUpperInvariant();

        bool isResourceGroupExist =
            await _cloudBroker.CheckResourceGroupExistAsync(resourceGroupName: resourceGroupName);

        if (isResourceGroupExist)
        {
            _loggingBroker.LogActivity(message: $"Deprovisioning {resourceGroupName}...");
            await _cloudBroker.DeleteResourceGroupAsync(resourceGroupName: resourceGroupName);
            _loggingBroker.LogActivity(message: $"{resourceGroupName} Deprovisioned");
        }
        else
        {
            _loggingBroker.LogActivity(
                message: $"Resource group {resourceGroupName} doesn't exist. No action taken.");
        }
    }
}
