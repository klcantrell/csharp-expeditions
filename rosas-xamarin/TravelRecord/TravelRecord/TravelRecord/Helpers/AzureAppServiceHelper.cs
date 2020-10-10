#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace TravelRecord.Helpers
{
    public class AzureAppServiceHelper
    {
        public static async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await App.MobileService.SyncContext.PushAsync();

                await App.postsTable.PullAsync("userPosts", "");
            }
            catch (MobileServicePushFailedException e)
            {
                syncErrors = e.PushResult?.Errors;
            }
            catch {}

            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        await error.CancelAndDiscardItemAsync();
                    }    
                }
            }
        }
    }
}
