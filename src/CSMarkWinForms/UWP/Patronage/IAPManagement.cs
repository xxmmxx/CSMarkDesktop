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

        private string expirationdate;
        private bool isActive;
        private string productPurchased = "";

        public IAPManagement(){
            IsAPremiumUser();
        }

        //Get a StoreContext object by using the GetDefault method 
        private static StoreContext storeContext = StoreContext.GetDefault();
        private StorePrice premium3monthsprice;

        public StorePrice GetPremium3monthsprice()
        {
            return premium3monthsprice;
        }

        public void SetPremium3monthsprice(StorePrice value)
        {
            premium3monthsprice = value;
        }

        private string premium3monthspriceString;

        public string GetPremium3monthspriceString()
        {
            return premium3monthspriceString;
        }

        public void SetPremium3monthspriceString(string value)
        {
            premium3monthspriceString = value;
        }

        private StorePrice premium6monthsprice;

        public StorePrice GetPremium6monthsprice()
        {
            return premium6monthsprice;
        }

        public void SetPremium6monthsprice(StorePrice value)
        {
            premium6monthsprice = value;
        }

        private string premium6monthspriceString;

        public string GetPremium6monthspriceString()
        {
            return premium6monthspriceString;
        }

        public void SetPremium6monthspriceString(string value)
        {
            premium6monthspriceString = value;
        }

        private StorePrice premium12monthsprice;

        public StorePrice GetPremium12monthsprice()
        {
            return premium12monthsprice;
        }

        public void SetPremium12monthsprice(StorePrice value)
        {
            premium12monthsprice = value;
        }

        private string premium12monthspriceString;

        public string GetPremium12monthspriceString()
        {
            return premium12monthspriceString;
        }

        public void SetPremium12monthspriceString(string value)
        {
            premium12monthspriceString = value;
        }

        public string GetIAPExpirationDate(){
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
            isActive = appLicense.IsActive;
            expirationdate = appLicense.ExpirationDate.Day.ToString() + "/" + appLicense.ExpirationDate.Month.ToString();

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
                        isActive = true;
                    }
                }
                //Check to see if owns 6 month premium
                else if (addOnLicense.SkuStoreId == premium6months_id){
                    if (addOnLicense.IsActive){
                        productPurchased = "premium6months";
                        isActive = true;
                    }
                }
                //Check to see if owns 12 month premium
                else if (addOnLicense.SkuStoreId == premium12months_id){
                    if (addOnLicense.IsActive){
                        productPurchased = "premium12months";
                        isActive = true;
                    }
                }
            }

            string[] productKinds = { "Durable" };
            List<string> filterList = new List<string>(productKinds);
            StoreProductQueryResult queryResult = await storeContext.GetAssociatedStoreProductsAsync(filterList);

            foreach (KeyValuePair<string, StoreProduct> item in queryResult.Products){
                // Access the Store product info for the add-on.
                StoreProduct product = item.Value;
                // Use members of the product object to access listing info for the add-on...

                if (product.StoreId == premium3months_id){
                    SetPremium3monthsprice(product.Price);
                    SetPremium3monthspriceString(product.Price.FormattedBasePrice);                   
                }
                else if (product.StoreId == premium6months_id){
                    SetPremium6monthsprice(product.Price);
                    SetPremium6monthspriceString(product.Price.FormattedBasePrice);
                }
                else if (product.StoreId == premium12months_id){
                    SetPremium12monthsprice(product.Price);
                    SetPremium12monthspriceString(product.Price.FormattedBasePrice);
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