using AluminiumCoreLib.Utilities;
using AutoUpdaterDotNET;
using CSMarkLib;
using CSMarkLib.BenchmarkManagement;
using CSMarkWinForms.Forms;
using CSMarkWinForms.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

/// Using Namespaces to enable Subscriptions
using Windows.Services.Store;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CSMarkWinForms{
    public partial class Main : Form{

        [ComImport]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IInitializeWithWindow
        {
            void Initialize(IntPtr hwnd);
        }

        BenchmarkController btc;
        StressTestController stc = new StressTestController();

        //private string betaURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/beta.xml";
        private string stableURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/stable.xml";
        //Supported Versions of Windows
        private Version win10v1703 = new Version(10, 0, 15063, 0);
        private Version win10v1709 = new Version(10, 0, 16299, 0);
        private Version win10v1803 = new Version(10, 0, 17134, 0);

        private bool runningStressTest = false;
        private DateTime start;
        private Timer t;
        private Platform platform;

        private enum DistributionPlatform{
            SteamStore,
            WinStore,
            GitRepository
        }
        private enum ContributorLevel{
            Free,
            PatronPrime,
            StorePrime,
            PatronPro,
            PatronSponsor,
        }

        ContributorLevel level = ContributorLevel.Free;

        public Main(){
            InitializeComponent();
            Assembly assembly = Assembly.GetEntryAssembly();
            platform = new Platform();
            btc = new BenchmarkController();

            if (DetermineDistributionPlatform().Equals(DistributionPlatform.GitRepository)){
                AutoUpdater.Start(stableURL);
            }
            //Check for WinStore sub
            CheckIfUserHasSubscriptionAsync();
            DetermineContributorLevel();           
        }

        #region Handle Stress Test and Benchmarks
        private void HandleStressTest(){
            if (runningStressTest != true){
                startBenchmarkBtn.Enabled = false;
                runningStressTest = true;
                //Start the Stress Test as a new Task to ensure good UI performance.
                var startStressTest = new Task(() => stc.StartMultiStressTest());
                startStressTest.Start();
                //Create the dispatcher timer so we can see how long a stress test runs for.
                t = new Timer();
                t.Tick += TimerTickEvent;
                start = DateTime.Now;
                //Reset the stressTimer label.
                //    stressTimer.Content = "";
            }
            else{
                runningStressTest = false;
                startBenchmarkBtn.Enabled = true;
                stc.StopStressTest();
                //  var stopStressTest = new Task(() => stc.StopStressTest());
                //    stopStressTest.Start();
                t.Stop();
            }

            ApplyStresTestButtonColors();
        }
        private void ApplyStresTestButtonColors(){
            if (runningStressTest == false)
            {
                startStressTestBtn.BackColor = Color.Green;
                startStressTestBtn.Text = "Start Stress Test";
            }
            else{
                startStressTestBtn.BackColor = Color.Red;
                startStressTestBtn.Text = "Stop Stress Test";
            }
        }
        private void StartBenchmark(){
            var benchmarkWorkTask = new Task(() => BenchmarkWork());
            benchmarkWorkTask.Start();
            benchmarkWorkTask.Wait();

            try{
                startBenchmarkBtn.Invoke(new Action(() => { startBenchmarkBtn.Enabled = true; }));
                startBenchmarkBtn.Invoke(new Action(() => { startBenchmarkBtn.Text = "Start Benchmark"; }));
                startBenchmarkBtn.Invoke(new Action(() => { startStressTestBtn.Enabled = true; }));
                startBenchmarkBtn.Invoke(new Action(() => { new ResultsOverview(version.Text, contributionStatus.Text).ShowDialog(); }));
            }
            catch (Exception ex){
                MessageBox.Show(ex.ToString());
            }
        }
        private async void BenchmarkWork(){
       //     var warmupTask = Task.Factory.StartNew(() => btc.DoWarmup());
         //   warmupTask.Wait((60) * 1000);
            var task1 = Task.Factory.StartNew(() => btc.StartSingleBenchmarkTests());
            task1.Wait((120 * 5) * 1000);
            var task2 = Task.Factory.StartNew(() => btc.StartMultiBenchmarkTests());
            task2.Wait((120 * 5) * 1000);

            /*    HashMap<BenchmarkType, CSMarkLib.BenchmarkLib.Benchmark> hash = btc.ReturnBenchmarkObjects();
                var resultSaver = new ResultSaver();
                var result = resultSaver.SaveResult(true, hash);
                */

            Results.Default.BenchmarkResult = btc.SaveResult(true, true);
        //    Results.Default.BenchmarkResult = result;
            Results.Default.Save();
        }
        private void TimerTickEvent(object sender, EventArgs e){
            if (runningStressTest){
                //stressTimer.Content = Convert.ToString(DateTime.Now - start);
            }
        }
        #endregion
        #region Handle WinStore Subs
        private StoreContext context = StoreContext.GetDefault();
        StoreProduct subscriptionStoreProduct;

        // Assign this variable to the Store ID of your subscription add-on.
        private string primeSubscriptionStoreId = "9NN39G8WFZCJ";

        // This is the entry point method for the example.
        public async Task SetupSubscriptionInfoAsync()
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            bool userOwnsSubscription = await CheckIfUserHasSubscriptionAsync();
            if (userOwnsSubscription)
            {
                // Unlock all the subscription add-on features here.
                return;
            }

            // Get the StoreProduct that represents the subscription add-on.
            subscriptionStoreProduct = await GetSubscriptionProductAsync();
            if (subscriptionStoreProduct == null)
            {
                return;
            }

            // Prompt the customer to purchase the subscription.
            PromptUserToPurchaseAsync();
            // Check if the first SKU is a trial and notify the customer that a trial is available.
            // If a trial is available, the Skus array will always have 2 purchasable SKUs and the
            // first one is the trial. Otherwise, this array will only have one SKU.
            //  StoreSku sku = subscriptionStoreProduct.Skus[0];
            //     if (sku.SubscriptionInfo.HasTrialPeriod)
            //    {
            // You can display the subscription trial info to the customer here. You can use 
            // sku.SubscriptionInfo.TrialPeriod and sku.SubscriptionInfo.TrialPeriodUnit 
            // to get the trial details
            //   }
            //   else
            //    {
            //        // You can display the subscription purchase info to the customer here. You can use 
            // sku.SubscriptionInfo.BillingPeriod and sku.SubscriptionInfo.BillingPeriodUnit
            // to provide the renewal details.
            //    }

            
        }

        private async Task<bool> CheckIfUserHasSubscriptionAsync()
        {
            StoreAppLicense appLicense = await context.GetAppLicenseAsync();

            // Check if the customer has the rights to the subscription.
            foreach (var addOnLicense in appLicense.AddOnLicenses)
            {
                StoreLicense license = addOnLicense.Value;
                if (license.SkuStoreId.StartsWith(primeSubscriptionStoreId))
                {
                    if (license.IsActive)
                    {
                        // The expiration date is available in the license.ExpirationDate property.
                        level = ContributorLevel.StorePrime;
                        getPrimeBtn.Enabled = false;
                        getPrimeBtn.Visible = false;
                        return true;
                    }
                }
            }

            // The customer does not have a license to the subscription.
            return false;
        }

        private async Task<StoreProduct> GetSubscriptionProductAsync()
        {
            // Load the sellable add-ons for this app and check if the trial is still 
            // available for this customer. If they previously acquired a trial they won't 
            // be able to get a trial again, and the StoreProduct.Skus property will 
            // only contain one SKU.
            StoreProductQueryResult result =
                await context.GetAssociatedStoreProductsAsync(new string[] { "Durable" });

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
                if (product.StoreId == primeSubscriptionStoreId)
                {
                    return product;
                }
            }

            System.Diagnostics.Debug.WriteLine("The subscription was not found.");
            return null;
        }

        private async Task PromptUserToPurchaseAsync()
        {
            // Request a purchase of the subscription product. If a trial is available it will be offered 
            // to the customer. Otherwise, the non-trial SKU will be offered.
            StorePurchaseResult result = await subscriptionStoreProduct.RequestPurchaseAsync();

            // Capture the error message for the operation, if any.
            string extendedError = string.Empty;
            if (result.ExtendedError != null)
            {
                extendedError = result.ExtendedError.Message;
            }

            switch (result.Status)
            {
                case StorePurchaseStatus.Succeeded:
                    // Show a UI to acknowledge that the customer has purchased your subscription 
                    // and unlock the features of the subscription. 
                    break;

                case StorePurchaseStatus.NotPurchased:
                    System.Diagnostics.Debug.WriteLine("The purchase did not complete. " +
                        "The customer may have cancelled the purchase. ExtendedError: " + extendedError);
                    break;

                case StorePurchaseStatus.ServerError:
                case StorePurchaseStatus.NetworkError:
                    System.Diagnostics.Debug.WriteLine("The purchase was unsuccessful due to a server or network error. " +
                        "ExtendedError: " + extendedError);
                    break;

                case StorePurchaseStatus.AlreadyPurchased:
                    System.Diagnostics.Debug.WriteLine("The customer already owns this subscription." +
                            "ExtendedError: " + extendedError);
                    break;
            }
        }
        #endregion
        #region Utility Misc stuff

        /// <summary>
        /// Determine what distribution platform CSMark has come from.
        /// </summary>
        private DistributionPlatform DetermineDistributionPlatform(){
            DistributionPlatform distribution = DistributionPlatform.GitRepository;

            string currentDirectory = Environment.CurrentDirectory;

            if (currentDirectory.Contains("16188AluminiumTech.CSMark_20gejd9zdp9ny")){
                distribution = DistributionPlatform.WinStore;
                Text += " Windows Store Version";
            }
            else{
                distribution = DistributionPlatform.GitRepository;
                getPrimeBtn.Enabled = false;
                getPrimeBtn.Visible = false;
            }
            //Store Testing: 
            getPrimeBtn.Enabled = true;
            getPrimeBtn.Visible = true;

            return distribution;
        }
        /// <summary>
        /// Determine what level of contribution the current user is at.
        /// </summary>
        private ContributorLevel DetermineContributorLevel(){ 
            if (level.Equals(ContributorLevel.Free)){
                contributionStatus.Text = "FREE";
                contributionStatus.ForeColor = Color.Lime;
                getPrimeBtn.Visible = true;
            }
            else if (level.Equals(ContributorLevel.PatronPrime)){
                contributionStatus.Text = "PRIME";
                contributionStatus.ForeColor = Color.RoyalBlue;
                //  getPrimeBtn.Text = "Get PRO";
                //getPrimeBtn.ForeColor = Color.OrangeRed;
                getPrimeBtn.Visible = false;
            }
            else if (level.Equals(ContributorLevel.PatronPro)){
                contributionStatus.Text = "PRO";
                contributionStatus.ForeColor = Color.OrangeRed;
                ///
                getPrimeBtn.Visible = false;
            }
            else if (level.Equals(ContributorLevel.PatronSponsor)){
                contributionStatus.Text = "SPONSOR";
                contributionStatus.ForeColor = Color.Goldenrod;
                ///
                getPrimeBtn.Visible = false;
            }
            return level;
        }

        private void DoScaling()
        {
            title.Font = new Font("Segoe UI", 18);
            version.Font = new Font("Segoe UI", 18);

       //     settingsButton.Font = new Font("Segoe MDL2 Assets", 20);
        //    translateButton.Font = new Font("Segoe MDL2 Assets", 20);
            helpButton.Font = new Font("Segoe MDL2 Assets", 20);
            aboutButton.Font = new Font("Segoe MDL2 Assets", 20);

            aboutButton.Text = "\xE115";
            settingsButton.Text = "\xE115";
            //closeButton.Text = "\xE10A";
            //feedbackButton.Text = "\xE170";
            helpButton.Text = "\xE897";
            aboutButton.Text = "\xE946";
            translateButton.Text = "\xE775";
        }

        private void PositionIcons()
        {
         //       settingsButton.Location = new Point(Width - 70, Height - (Height - 5));
                aboutButton.Location = new Point(Width - 70, Height - (Height - 5));
                helpButton.Location = new Point(Width - 120, Height - (Height - 5));
          //      translateButton.Location = new Point(Width - 170, Height - (Height - 5));
                PositionButtons();
            translateButton.Hide();
            helpButton.Hide();
        }

        private void PositionButtons(){
           startBenchmarkBtn.Location = new Point((Width / 2) - 100, Height / 2);
            startStressTestBtn.Location = new Point((Width / 2) - 100, (Height / 2) + 80);
            overclockWarning.Location = new Point((Width / 2) - 200, (Height / 2) + 160);
            copyrightNotice.Location = new Point((Width / 2) - 150, (Height / 2) + 240);

            //      MessageBox.Show(Width.ToString());
            //    MessageBox.Show(Height.ToString());
        }

        private string FormatDateVersion()
        {
            string major = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
            string minor = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            string build = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
            string revision = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();

            version.Text = version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString() + "." + revision.ToString();

            string existingString = "";
            string newString = "0";

            //Fix the date version format for the minor version
            if (minor.Length.Equals(1))
            {
                existingString = minor;
                newString = newString + existingString;
                minor = newString;
            }

            ///Smart auto correcting version syste,.
             if (build.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString();
            }
            else if (revision.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString();
            }
            else if (!revision.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString() + "." + revision.ToString();
            }

            if (major.Equals("0")){
                version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString();
            }

            return version.Text;
        }

        private string FormatFriendlyVersion()
        {
            string major = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
            string minor = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            string build = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
            string revision = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();

            ///Smart auto correcting version syste,.
            if (build.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString();
            }
            else if (revision.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString();
            }
            else if (!revision.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString() + "." + revision.ToString();
            }

            if (major.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString();
            }

            return version.Text;
        }
        #endregion
        private void Main_Load(object sender, EventArgs e){
            Text = ProductName;
            title.Text = ProductName;
            PositionIcons();
            DoScaling(); 
            FormatFriendlyVersion();
            DetermineContributorLevel();
            DetermineDistributionPlatform();
            settingsButton.Visible = false;
        }
        #region Button click handling
        private void aboutButton_Click(object sender, EventArgs e){
            Form about = new About(version.Text);
            about.ShowDialog();
        }
        private void helpButton_Click(object sender, EventArgs e){

        }
        private void translateButton_Click(object sender, EventArgs e){

        }
        private void Main_ResizeEnd(object sender, EventArgs e){
            PositionIcons();
        }
        private void Main_Resize(object sender, EventArgs e){
            PositionIcons();
        }
        private void startBenchmarkBtn_Click(object sender, EventArgs e){
            startStressTestBtn.Enabled = false;
            startBenchmarkBtn.Text = "Starting Benchmark...";
            startBenchmarkBtn.Enabled = false;
            var task = new Task(() => StartBenchmark());
            task.Start();
        }
        private void startStressBtn_Click(object sender, EventArgs e){
            HandleStressTest();
        }
        #endregion

        private void getPrimeBtn_Click(object sender, EventArgs e){
            try
            {
                SetupSubscriptionInfoAsync();
                PromptUserToPurchaseAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}