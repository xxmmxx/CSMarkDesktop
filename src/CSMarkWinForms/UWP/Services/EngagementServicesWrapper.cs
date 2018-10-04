using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Windows.Services.Store;
using System.Runtime.InteropServices;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace CSMarkWinForms.UWP.Services{
    public class EngagementServicesWrapper{
        //Declare the IInitializeWithWindow interface in your app's code with the ComImport attribute
        [ComImport]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IInitializeWithWindow
        {
            void Initialize(IntPtr hwnd);
        }

        /// <summary>
        /// Ask the customer for a rating and review.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ShowRatingReviewDialog(){
            StoreSendRequestResult result = await StoreRequestHelper.SendRequestAsync(
                StoreContext.GetDefault(), 16, String.Empty);

            if (result.ExtendedError == null){
                JObject jsonObject = JObject.Parse(result.Response);
                if (jsonObject.SelectToken("status").ToString() == "success"){
                    // The customer rated or reviewed the app.
                    return true;
                }
            }

            // There was an error with the request, or the customer chose not to
            // rate or review the app.
            return false;
        }
        /// <summary>
        /// Launch the Store Ratings and Review page
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public async Task<bool> LaunchStoreReviewPage(string ProductID){
            bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=" + ProductID));
            return result;
        }
    }
}
