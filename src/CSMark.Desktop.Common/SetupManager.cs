using System;

namespace CSMark.Desktop.Common {
	
	public class SetupManager{
		//
		bool IsWindowsStoreApp;
       // We don't need this right now.
       // bool IsSteamStoreApp;

		public SetupManager(bool IsWindowsStoreApp){
			this.IsWindowsStoreApp = IsWindowsStoreApp;

            OSCompatibilityCheck();
		}
        //Supported Versions of Windows for Non-Windows Store release
        private Version win10v1703 = new Version(10, 0, 15063, 0);
        //Supported Versions of Windows for Windows Store release and non Windows Store release
        private Version win10v1709 = new Version(10, 0, 16299, 0);
        private Version win10v1803 = new Version(10, 0, 17134, 0);    
        private Version win10v1809 = new Version(10, 0, 17763, 0);

        public bool OSCompatibilityCheck(){
            if (IsWindowsStoreApp){
                return OSCompatibilityCheck_Store();
            }
            return (Environment.OSVersion.Version.Equals(win10v1703) || Environment.OSVersion.Version.Equals(win10v1709) || Environment.OSVersion.Version.Equals(win10v1803) || Environment.OSVersion.Version.Equals(win10v1809));
        }
        private bool OSCompatibilityCheck_Store(){
            return (Environment.OSVersion.Version.Equals(win10v1709) || Environment.OSVersion.Version.Equals(win10v1803));
        }

        /// <summary>
        /// Determine what distribution platform CSMark has come from.
        /// </summary>
        public DistributionPlatform DetermineDistributionPlatform(){
            string currentDirectory = Environment.CurrentDirectory;

            if (currentDirectory.Contains("16188AluminiumTech.CSMark_20gejd9zdp9ny")){
                IsWindowsStoreApp = true;
                return DistributionPlatform.WinStore;
            }
            else if (currentDirectory.Contains("CSMarkDesktop")){
                IsWindowsStoreApp = false;
                return DistributionPlatform.GitRepository;
            }
            else{
                IsWindowsStoreApp = true;
                return DistributionPlatform.WinStore;
            }
        }
        public ContributorLevel CheckContributionLevel(bool IsActiveIAP){
            if (IsWindowsStoreApp && IsActiveIAP){
                return ContributorLevel.StorePremium;
            }
            else if(IsWindowsStoreApp && !IsActiveIAP){
                return ContributorLevel.Free;
            }
            return ContributorLevel.Free;         
        }
	}
	public enum DistributionPlatform{
            SteamStore,
            WinStore,
            GitRepository,
            OfficialStore,
            OtherUntrusted
    }
     public enum ContributorLevel{
        Free,
        PatronPremium,
        PatronPro,
        StorePremium,
        PatronSponsor,
        OfficialStorePremium,
        OfficialStorePro,
        OfficialStoreSponsor
    }
}