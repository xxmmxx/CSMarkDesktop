using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Services.Store;
using System.Runtime.InteropServices;
using System.Windows;

namespace CSMarkWinForms.Patronage{
   public class SubscriptionWrapper{

        StorePrice subscriptionPrice;
        ContributorLevel _level;
        public ContributorLevel GetContributorLevel(){
            return _level;
        }
        public StorePrice GetSubscriptionPrice(){
            return subscriptionPrice;
        }

        //Declare the IInitializeWithWindow interface in your app's code with the ComImport attribute
        [ComImport]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IInitializeWithWindow
        {
            void Initialize(IntPtr hwnd);
        }

        private StoreContext storeContext = StoreContext.GetDefault();
        StoreProduct subscriptionStoreProduct;

        // Assign this variable to the Store ID of your subscription add-on.
        private string primeSubscriptionStoreId = "9NN39G8WFZCJ";

        // This is the entry point method for the example.
        public async Task SetupSubscriptionInfoAsync()
        {
            IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)storeContext;
            var ptr = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

            //Call the IInitializeWithWindow.Initialize method, and pass the handle of the window 
            //to be the owner for any modal dialogs that are shown by StoreContext methods.
            initWindow.Initialize(ptr);

            bool userOwnsSubscription =  await CheckIfUserHasSubscriptionAsync();
            if (userOwnsSubscription){
                // Unlock all the subscription add-on features here.
                return;
            }

            // Get the StoreProduct that represents the subscription add-on.
            subscriptionStoreProduct = await GetSubscriptionProductAsync();
            if (subscriptionStoreProduct == null){
                return;
            }

            // Prompt the customer to purchase the subscription.
            PurchaseAsync();
        }

        public async Task<bool> CheckIfUserHasSubscriptionAsync(){
            StoreAppLicense appLicense = await storeContext.GetAppLicenseAsync();

          //  System.Threading.Thread.Sleep(5000);

            // Check if the customer has the rights to the subscription.
            foreach (var addOnLicense in appLicense.AddOnLicenses)
            {
                StoreLicense license = addOnLicense.Value;
                if (license.SkuStoreId.StartsWith(primeSubscriptionStoreId))
                {
                    if (license.IsActive)
                    {
                        // The expiration date is available in the license.ExpirationDate property.
                        _level = ContributorLevel.StorePremium;
                        return true;
                    }
                }
            }

            // The customer does not have a license to the subscription.
            return false;
        }

        public async Task<StoreProduct> GetSubscriptionProductAsync()
        {
            // Load the sellable add-ons for this app and check if the trial is still 
            // available for this customer. If they previously acquired a trial they won't 
            // be able to get a trial again, and the StoreProduct.Skus property will 
            // only contain one SKU.
            StoreProductQueryResult result =
                await storeContext.GetAssociatedStoreProductsAsync(new string[] { "Durable" });

            if (result.ExtendedError != null)
            {
                System.Diagnostics.Debug.WriteLine("Something went wrong while getting the add-ons. " +
                    "ExtendedError:" + result.ExtendedError);
                return null;
            }

            // Look for the product that represents the subscription.
            foreach (var item in result.Products)
            {
                StoreProduct product = item.Value;
                subscriptionPrice = product.Price;
                if (product.StoreId == primeSubscriptionStoreId)
                {
                    return product;
                }
            }

            System.Diagnostics.Debug.WriteLine("The subscription was not found.");
            return null;
        }

        public async Task PurchaseAsync()
        {
            IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)storeContext;
            var ptr = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

            //Call the IInitializeWithWindow.Initialize method, and pass the handle of the window 
            //to be the owner for any modal dialogs that are shown by StoreContext methods.
            initWindow.Initialize(ptr);

            var result = await storeContext.RequestPurchaseAsync(primeSubscriptionStoreId);

            if (result.ExtendedError != null)
            {
                MessageBox.Show(result.ExtendedError.Message, result.ExtendedError.HResult.ToString());
            }

            MessageBox.Show("Purchase Finished with status " + result.Status);
        }

        public async Task GetSubscriptionsInfo()
        {
            if (storeContext == null)
            {
                storeContext = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            // Subscription add-ons are Durable products.
            string[] productKinds = { "Durable" };
            List<String> filterList = new List<string>(productKinds);

            StoreProductQueryResult queryResult =
                await storeContext.GetAssociatedStoreProductsAsync(productKinds);

            if (queryResult.ExtendedError != null)
            {
                // The user may be offline or there might be some other server failure.
                System.Diagnostics.Debug.WriteLine($"ExtendedError: {queryResult.ExtendedError.Message}");
                return;
            }

            foreach (KeyValuePair<string, StoreProduct> item in queryResult.Products)
            {
                // Access the Store product info for the add-on.
                StoreProduct product = item.Value;
                subscriptionPrice = product.Price;

                // For each add-on, the subscription info is available in the SKU objects in the add-on. 
                foreach (StoreSku sku in product.Skus)
                {
                    if (sku.IsSubscription)
                    {
                        // Use the sku.SubscriptionInfo property to get info about the subscription. 
                        // For example, the following code gets the units and duration of the 
                        // subscription billing period.
                        StoreDurationUnit billingPeriodUnit = sku.SubscriptionInfo.BillingPeriodUnit;
                        uint billingPeriod = sku.SubscriptionInfo.BillingPeriod;
                    }
                }
            }
        }
    }
}