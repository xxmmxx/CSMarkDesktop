# CSMarkDesktop
CSMark is a CPU benchmark written in C# .NET .

The CSMarkCore CLI applications works on Windows, macOS and Linux Distributions.
The CSMarkWPF Application works on Windows only.

## CSMarkCore
CSMarkCore is the original .NET Core CLI application with major improvements to usability and simplicity.

This was originally called CSMarkDesktop but was renamed to help avoid confusion.

The Benchmarking and Stress Testing capabilities were separated into 2 different executables sharing similar names.
The overall name for the project is CSMarkCore.

## CSMarkDesktop
CSMarkDesktop is the new GUI version of CSMark which works on Windows.

## Where is the CSMarkDesktop Cross-platform GUI version?
It is still in the works. CSMarkWPF was introduced to provide a GUI experience to Windows users in a timely fashion whilst the cross-platform GUI version is underway.

### Which version should I download?
| Processor Type | OS | CSMark Edition You Should Download | CSMark Package You Should Download |
|---------|----------|-----------|----------------|
| Any x86-64 CPU | Windows 10 | CSMarkDesktop or CSMarkCore | CSMarkWPF or Win10-x64 |
| Any 32 Bit ARM CPU E.g. Qualcomm(R) SnapdragonTM 400 | Windows 10 | CSMarkCore | Win10-arm |
| Any ARM64 CPU E.g. Qualcomm(R) SnapdragonTM 835 Mobile Computing Platform | Windows 10 | CSMarkCore | Win10-arm |
| Any x86-64 CPU | macOS | CSMarkCore | osx-x64 |
| Any x86-64 CPU | Linux | CSMarkCore | linux-x64 |
| Any 32 Bit ARM CPU | Linux | CSMarkCore | linux-arm |

## Contributing
For details on Contributing or Building the Source Code check out the [Contribution Guide](/CONTRIBUTING.md) .

## What makes up CSMark
The CSMark Project is made up of 2 primary repositories.

[CSMarkLib](https://www.gitlab.com/CSMarkBenchmark/CSMarkLib/) is one of the main ones and is used to keep the development of the benchmarks seperated from the implementation of them for a specific platform. E.g. CSMarkLib can be updated independantly of the CSMark App.

The CSMark repository is the other main repo for this project and is used to help develop CSMark and ties everything together.

## Requirements for running the Benchmarks
The .NET Core Runtime is included in CSMark Releases. There is no need to download the Runtime seperately unless explicitly specified.

__List Of Supported Platforms for CSMarkCore__
* Windows 10 (ARM, ARM64, X64) (10.0.15063 or newer) or Windows Server 2016
* macOS 10.12 "Sierra" and macOS 10.13 "High Sierra"
* Ubuntu 14.04 LTS, Ubuntu 16.04 LTS, Ubuntu 18.04 LTS
* Debian 8.7 or newer (8.7+ and 9.0+)
* CentOS 7 or newer
* RedHat Linux Enterprise 7 or newer
* SUSE Linux Enterprise 12 SP2 +
* Fedora 27 or newer
* openSUSE 42.3 or newer
* LinuxMint 18 or newer

__List Of Supported Platforms for CSMarkWPF__
* Windows 10 (X86, X64) (10.0.15063 or newer) or Windows Server 2016

[See OS Support Document for more details](/docs/OS_Support.md)

## Install CSMark
* [Running CSMark On Windows](/docs/RunningCSMarkOnWindows.md)
* [Running CSMark On Linux](/docs/RunningCSMarkOnLinux.md)
* [Running CSMark On Mac](/docs/RunningCSMarkOnMac.md)

<a href='//www.microsoft.com/store/apps/9N6S6JJFWJ6M?ocid=badge'><img src='https://assets.windowsphone.com/85864462-9c82-451e-9355-a3d5f874397a/English_get-it-from-MS_InvariantCulture_Default.png' alt='English badge' width='250' height='70'/></a>

## Notes
* Results from CSMark versions which are of a different major or minor version, in the format of [Major].[Minor].[Build].[Revision] , than another version should not be compared unless explicitly stated otherwise.  
* When comparing CPUs, the same exact version of the benchmark should be used across testing platforms.

## Supporting this Project
[![Support CSMark through Patreon](https://github.com/CSMarkBenchmark/CSMarkDesktop/blob/master/assets/patron_button.png)](https://www.patreon.com/csmark)

## FAQ
__Question:__ Why is CSMarkCore a CLI program?
__Answer:__ To allow it to run on Windows, Mac and Linux. .NET Core currently doesn't contain a cross-platform GUI component however if one does come in the future, an attempt to migrate to a GUI based program may be made.

__Question:__ Where can I hang out with the CSMark community?
__Answer:__ There's the [CSMark Discord](https://discord.gg/M3DMgcY).

__Question:__ Can I (or an organization I work for) review CSMark?
__Answer:__ Of course! I'd like to get in contact with you prior to you or your company publishing your review. You can email me or Discord Direct Message a maintainer. I will try to respond and if I'm busy, another maintainer or contributor could respond.

__Question:__ Can I make videos or content around CSMark?
__Answer:__ Of course! Anybody can make videos and/or content around CSMark. I don't currently sponsor videos but I will be happy to answer questions for you if you send me an email or Discord DM to me.

__Question:__ Is the version of CSMark I'm using supported?
__Answer:__ You can check the status of CSMark support at the [CSMark Support Documentation](/Support.md).

## Notes
* Qualcomm Snapdragon is a trademark of Qualcomm Inc. 
* All Trademarks and Copyrights belong to their respective owners.
