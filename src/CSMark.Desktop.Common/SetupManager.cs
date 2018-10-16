using AutoUpdaterDotNET;
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
		//Check For Updates URL for Stable Channel
        private string stableURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/win/stable.xml";
        //Check For Updates URL for Beta Channel
        private string betaURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/win/beta.xml";

        //Supported Versions of Windows for Non-Windows Store release
        private Version win10v1607 = new Version(10, 0, 14393, 0);
        private Version win10v1703 = new Version(10, 0, 15063, 0);
        //Supported Versions of Windows for Windows Store release
        private Version win10v1709 = new Version(10, 0, 16299, 0);
        private Version win10v1803 = new Version(10, 0, 17134, 0);    
        //Windows version(s) we will soon support
        private Version win10v1809 = new Version(10, 0, 17763, 0);

        public bool OSCompatibilityCheck(){
            if (IsWindowsStoreApp){
                return OSCompatibilityCheck_Store();
            }
            return (Environment.OSVersion.Version.Equals(win10v1607) ||  Environment.OSVersion.Version.Equals(win10v1703) || Environment.OSVersion.Version.Equals(win10v1709) || Environment.OSVersion.Version.Equals(win10v1803) || Environment.OSVersion.Version.Equals(win10v1809));
        }
        private bool OSCompatibilityCheck_Store(){
            return (Environment.OSVersion.Version.Equals(win10v1709) || Environment.OSVersion.Version.Equals(win10v1803) || Environment.OSVersion.Version.Equals(win10v1809));
        }

        /// <summary>
        /// 
        /// </summary>
        public void CheckForUpdate(bool UseBetaChannel){
            if (!IsWindowsStoreApp && !UseBetaChannel){
                AutoUpdater.Start(stableURL);
            }
            else if (!IsWindowsStoreApp && UseBetaChannel){
                AutoUpdater.Start(betaURL);
            }
            else if(IsWindowsStoreApp && !UseBetaChannel){
                //TODO: Add Windows Store Check For Updates
            }
            else{

            }
        }
        /// <summary>
        /// Determine what distribution platform CSMark has come from.
        /// </summary>
        public DistributionPlatform DetermineDistributionPlatform(){
            DistributionPlatform distribution = DistributionPlatform.GitRepository;
            string currentDirectory = Environment.CurrentDirectory;

            if (currentDirectory.Contains("16188AluminiumTech.CSMark_20gejd9zdp9ny")){
                return DistributionPlatform.WinStore;
            }
            else{
                distribution = DistributionPlatform.GitRepository;
            }

            return distribution;
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