# Contribution Guide

**Table Of Contents**

[What should I know before I get started?](#what-should-i-know-before-i-get-started)

[Code Of Conduct](#code-of-conduct)

[Setting up your environment](#setting-up-your-environment)

[How to submit changes](#how-to-submit-changes)

[How to suggest enhancements](#suggesting-enhancements)

[How to report a bug](#how-to-report-a-bug)

[Building Release Binaries](#building-release-binaries)

[How do I get recognized for my contributions?](#contribution-recognition)

## What should I know before I get started
CSMark takes advantage of OOP to try to modularize components and that means that making changes is usually pretty easy.

All the calculations logic and benchmark classes are in the [CSMarkLib project](https://www.github.com/CSMarkBenchmark/CSMarkLib/) to allow the 2 projects to be developed at their own pace.

CSMark is built using C# .NET Core. The library with all the benchmarks is maintained seperately. Due to the nature of our benchmarks, we don't want to support 32 Bit PC Devices as they would have substantially worse performance. In some cases we cannot avoid this such as with ARM on Linux and Windows 10.

It is also important to acknowledge that CSMark is cross-platform and works on Windows, Mac and Linux. This is important to note because each release which is published should contain binaries for as many platforms as is reasonable. Any new code that is added should be able to run on Windows, Mac or Linux. 

## Setting up your environment
Setting up your environment for developing CSMark is pretty straight forward.

Below I've listed the different recommendations for different OS users.

### Setting up your environment on Windows
Developing CSMark on Windows is pretty straight forward. 
* I recommend downloading [Visual Studio 2017](https://www.visualstudio.com) - Ensure that you have selected the .NET Core package for installation.
* You should download [GitHub Desktop](https://desktop.github.com/) to be able to easily [submit changes](#how-to-submit-changes)

You don't need to download anything else to get setup.  If you'd like to try out CSMark on a system without the DotNetCore SDKs (the SDK is included in the Visual Studio DotNetCore package), you'll want to download the [.NET Core Runtime](https://www.microsoft.com/net/download/core#/runtime).

## How to submit changes
You can submit changes by:
1. Forking this repository - It's as simple as pressing that fork button.
2. Cloning your fork of the repository onto your PC - This can be through GitHub Desktop's easy cloning functionality or through Git clone on any computer.
3. Make whatever changes you want to make
4. Test the changes and debug the app to make sure you haven't introduced any new bugs.
5. Create a Pull request
6. Fill in the required details specified in the Pull Request Template - Make sure that your title does not include an issue number.
7. Include screenshots if possible
8. Ensure that your code is compatible with and/or works on Windows, Mac, and Linux.
9. Wait for a Maintainer to check your Pull Request. If it's up to provides 1 or more meaningful improvements such as new features or bug fixes, then Maintainers can accept the Pull Request and Merge it into the main branch. - In general, try to seperate your pull requests to only focus on the minimum required to achieve its goal so that each individual feature or fix can be reviewed individually.

## Building Release Binaries
So, you want to build release binaries of CSMark?

You'll need all the SDKs and Tools mentioned above in [Setting up your environment](#setting-up-your-environment).

Creating binaries currently requires using the Visual Studio's Publish tool.

The license file has been set to be copied every time you create a new binary so you don't need to worry about that.

You may wish to ZIP the files for publishing or distributing them.

That doesn't mean that if you make or distribute your own releases that you have to. You can use [NSIS](http://nsis.sourceforge.net/Main%5FPage) if you're more familiar with that.

## Code Of Conduct
Everybody participating in the CSMark project must abide by the [CSMark Code of Conduct](https://github.com/AluminiumTech/CSMark/blob/master/CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code.
Please report unacceptable behavior to aluminiumdev@gmail.com.

## How to report a bug
This section guides you through submitting a bug report for CSMark.

### Before submitting a bug report
* Check to see if there an existing bug report for the bug you've experienced.
* If you find a Closed issue that seems like it is the same thing that you're experiencing, open a new issue and include a link to the original issue in the body of your new one.

### How do I submit a good bug report?
Bugs in CSMark are tracked as [GitHub issues](https://guides.github.com/features/issues/). 
Create an issue following [the template](https://github.com/CSMarkBenchmark/CSMark/blob/master/ISSUE_TEMPLATE.md).

Explain the problem & include various information to help maintainers reproduce the problem:
* Use a clear and descriptive title - Maintainers should be able to have a good idea about what issue you're experiencing before they click on your issue.
* Describe exactly how to reproduce the problem - State what you did and how you did them.
* Describe the behavior you observed after following the steps and point out what exactly is the problem with that behavior.
* Explain which behavior you expected to see instead and why.
* If the problem wasn't triggered by a specific action, describe what you were doing before the problem happened and share more information using the guidelines below
* Can you reliably reproduce the issue? If not, provide details about how often the problem happens and under which conditions it normally happens.

## Suggesting Enhancements
* Use a clear and descriptive title for the issue to identify the suggestion - It should be obvious to maintainers exactly what you want. Try to keep it under 150 characters.
* Provide a step-by-step description of the suggested enhancement in as many details as possible.
* Describe the current behavior and explain which behavior you expected to see instead and why.
* State what version of CSMark you're using
* State the name and version of the OS you're using.

## Contribution Recognition
Thank you for contributing to this project! You're awesome :) .
