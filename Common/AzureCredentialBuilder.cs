using Azure.Core;
using Azure.Identity;

namespace Common;

public static class AzureCredentialBuilder
{
    public static TokenCredential Credential()
    {
        #if DEBUG
            return new AzureCliCredential();
        #else
            return new DefaultAzureCredential();
        #endif

    }
}