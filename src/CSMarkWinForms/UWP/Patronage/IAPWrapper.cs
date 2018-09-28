using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Windows.Services.Store;
using System.Runtime.InteropServices;
using System.Windows;

namespace CSMarkWinForms.Patronage{
   public class IAPWrapper{
        //Declare the IInitializeWithWindow interface in your app's code with the ComImport attribute
        [ComImport]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IInitializeWithWindow
        {
            void Initialize(IntPtr hwnd);
        }

        //Get a StoreContext object by using the GetDefault method 
        private static StoreContext storeContext = StoreContext.GetDefault();

        private string resultString = "";

        public string Purchase(string storeID){
            try{
                PurchaseAsync(storeID);
                return resultString;
            }
            catch (Exception ex){
                return "Exception:" + ex.ToString() + "exMessage:" + ex.Message;
            }
        }

        async Task PurchaseAsync(string storeID){
            IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)storeContext;
            var ptr = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

            //Call the IInitializeWithWindow.Initialize method, and pass the handle of the window 
            //to be the owner for any modal dialogs that are shown by StoreContext methods.
            initWindow.Initialize(ptr);

            var result = await storeContext.RequestPurchaseAsync(storeID);

            switch (result.Status)
            {
                case StorePurchaseStatus.AlreadyPurchased:
                    resultString = "You already bought this AddOn.";
                    break;

                case StorePurchaseStatus.Succeeded:
                    resultString = "Product was purchased. Transaction Successful.";
                    break;

                case StorePurchaseStatus.NotPurchased:
                    resultString = "Product was not purchased, it may have been canceled or your card may have been declined.";
                    break;

                case StorePurchaseStatus.NetworkError:
                    resultString = "Product was not purchased due to a network error.";
                    break;

                case StorePurchaseStatus.ServerError:
                    resultString = "Product was not purchased due to a server error.";
                    break;

                default:
                    resultString = "Product was not purchased due to an unknown error. Your card may have been declined.";
                    break;
            }

            if (result.ExtendedError != null)
            {
                MessageBox.Show(result.ExtendedError.Message, result.ExtendedError.HResult.ToString());
            }

            MessageBox.Show("Purchase Finished with status " + result.Status);
        }
    }
}