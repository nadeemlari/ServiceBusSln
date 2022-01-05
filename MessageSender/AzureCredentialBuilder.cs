using Azure.Core;
using Azure.Identity;

namespace MessageSender;

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