using System;

namespace CSMark.Desktop.Common {
	
	public class SetupManager{
		//
		bool IsWindowsStoreApp;

		public SetupManager(bool IsWindowsStoreApp){
			this.IsWindowsStoreApp = IsWindowsStoreApp;
		}
		//Check For Updates URL for Stable Channel
        private string stableURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/stable.xml";
        //

		//Supported Versions of Windows
        private Version win10v1709 = new Version(10, 0, 16299, 0);
        private Version win10v1803 = new Version(10, 0, 17134, 0);    
        //Windows version(s) we will soon support
        private Version win10v1809 = new Version(10, 0, 17763, 0); 

        public void CheckForUpdate(){
        	if(!IsWindowsStoreApp){
        		
        	}
        	else{
        		
        	}
        }
        public ContributorLevel CheckContributionLevel(){

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