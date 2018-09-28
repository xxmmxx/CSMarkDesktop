using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Services.Store;
using System.Runtime.InteropServices;
using System.Windows;
using CSMarkWinForms.Patronage;

namespace CSMarkWinForms.UWP.Patronage{
    class IAPManagement{
        //Declare the IInitializeWithWindow interface in your app's code with the ComImport attribute
        [ComImport]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IInitializeWithWindow
        {
            void Initialize(IntPtr hwnd);
        }

       private string premium3months_id = "9NNRJNZS5SS9";
       private string premium6months_id = "9NFTTGL0LTL2";
       private string premium12months_id = "9PL25LNX6403";

        private DateTimeOffset expirationdate;
        private bool isActive;
        private string productPurchased = "";

        public IAPManagement(){

        }

        //Get a StoreContext object by using the GetDefault method 
        private static StoreContext storeContext = StoreContext.GetDefault();

        public DateTimeOffset GetIAPExpirationDate(){
            try{
                return expirationdate;
            }
            catch{
                IsAPremiumUser();
                return expirationdate;
            }
        } 

        public bool IsActiveIAP(){
            try{
                return isActive;
            }
            catch{
                IsAPremiumUser();
                return isActive;
            }
        }

        public string GetProductPurchased(){
            try{
                return productPurchased;
            }
            catch{
                IsAPremiumUser();
                return productPurchased;
            }
        }

        public async Task IsAPremiumUser(){
            if (storeContext == null){
                storeContext = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            StoreAppLicense appLicense = await storeContext.GetAppLicenseAsync();

            // Access the valid licenses for durable add-ons for this app.
            foreach (KeyValuePair<string, StoreLicense> item in appLicense.AddOnLicenses)
            {
                StoreLicense addOnLicense = item.Value;
                // Use members of the addOnLicense object to access license info
                // for the add-on.

                //Check to see if owns 3 month premium
                if (addOnLicense.SkuStoreId == premium3months_id){
                    if (addOnLicense.IsActive){
                        productPurchased = "premium3months";
                        expirationdate = addOnLicense.ExpirationDate;
                        isActive = true;
                    }
                }
                //Check to see if owns 6 month premium
                else if (addOnLicense.SkuStoreId == premium6months_id){
                    if (addOnLicense.IsActive){
                        productPurchased = "premium6months";
                        expirationdate = addOnLicense.ExpirationDate;
                        isActive = true;
                    }
                }
                //Check to see if owns 12 month premium
                else if (addOnLicense.SkuStoreId == premium12months_id){
                    if (addOnLicense.IsActive){
                        productPurchased = "premium12months";
                        expirationdate = addOnLicense.ExpirationDate;
                        isActive = true;
                    }
                }
            }

            isActive = false;
        }

        public string Purchase3MonthPremium(){
            if (!IsActiveIAP()) {
                IAPWrapper wrapper = new IAPWrapper();
                return wrapper.Purchase(premium3months_id);
            }
            return "Product already purchased"; 
        }
        public string Purchase6MonthPremium(){
            if (!IsActiveIAP())
            {
                IAPWrapper wrapper = new IAPWrapper();
                return wrapper.Purchase(premium6months_id);
            }
            return "Product already purchased";
        }
        public string Purchase12MonthPremium(){
            if (!IsActiveIAP())
            {
                IAPWrapper wrapper = new IAPWrapper();
                return wrapper.Purchase(premium12months_id);
            }
            return "Product already purchased";
        }

        public string Get3MonthPremiumStoreID(){
            return premium3months_id;
        }
        public string Get6MonthPremiumStoreID(){
            return premium6months_id;
        }
        public string Get12MonthPremiumStoreID(){
            return premium12months_id;
        }
    }
}