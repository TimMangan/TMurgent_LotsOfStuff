**TMurgent_LotsOfStuff**

This software is associated with some performance testing done to determine if MSIX and/or the Package Support Framework cause undue performanace degredation over native applications.

It consists of a purpose build test application that takes a runtime argument for a workload to include in the test, along with installer or packaging projects.

All software is freely provided as is, with no warranty, and under the MIT license, 
should you wish to verify the results or add additional workload tests or scenarios.


*1) Workloads*

At this time, there are 7 workloads, numbered as follows:

> **0** - a simple workload that does nothing, just to measure the overhead of the test harness.

> **1** - a simple timer to delay for 5 seconds, to validate the overhead of the test harness.


> **2** - Enumerate 50,000 existing registry keys and values, to test the performance of registry access.

> **3** - Create 50,000 new registry keys and values, to test the performance of registry writing.

> **4** - Create 15,000 new resitry keys and values, and then go back and enumerate them. Under MSIX this is to verify virtual registry performance.

> **5** - Enumerate 50,000 existing files and directories, to test the performance of file system access.

> **6** - Create 50,000 new files and directories, to test the performance of file system writing.


*2) Solution Projects*

The Visual Studio Solution contains the following projects:

> **LotsOfStuffWPF_DotNetFramework** - a WPF application that will be used to test the performance of the native, MSIX, and MSIX with PSF packaging.

> **LotsOfStuffWinUI_DotNet** - a WinUI 3 application that will be used to test the performance of the native, MSIX, and MSIX with PSF  packaging.

Currently there are two test applications, written in C#, one for the .Net Framework runtime and one for the .Net 10 runtime.  
Additional test applications, such as unmanaged C++ for Win32, could be added in the future.

The Native/Unpackaged installer projects are:

> **LotsOfStuffWPF_InnoInstaller** - an InnoSetup installer project that will create a native unpackaged installer for the LotsOfStuffWPF_DotNetFramework application.

> **LotsOfSfuggWinUI_InnoInstaller** - an InnoSetup installer project that will create a native unpackaged installer for the LotsOfStuffWinUI_DotNet application.

The project files for the InnoInstaller projects contain a path to the InnoSetup compiler, which you will need to edit to match your system.  The InnoSetup compiler is installed with the InnoSetup IDE, which you can download from https://jrsoftware.org/isinfo.php.
These projects are also configured to "publish" on completion of the build, creating the final installer exe.
These projects are configured to install into the Program Files folder by default, and they will generate a shortcut (that you don't need).

The MSIX packaging projects are:

> **LotsOfStuffWPF_Package** - a Windows Application Packaging Project that will create an MSIX package for the LotsOfStuffWPF_DotnetFramework application.

> **LotsOfStuffWinUI_Package** - a Windows Application Packaging Project that will create an MSIX package for the LotsOfStuffWinUI_DotNet application.

The two Windows Application Projects that will each create an MSIX package with InstallLocationVirtualization , but without the PSF.  You must edit the project files to configure your own code-signing certificate.
You will need to change the package.appxmanifest file of each project to configure the publisher field to match your code-signing certificate Subject.  
These projects are also configured to "publish" on completion of the build, creating the final MSIX package (without PSF).

There is an assumption that for each MSIX package you will also create a copy version of the package (possibly editing the MSIX package with the TMEditX or other tool):

>  Manually add the PSF to a copy of the package to add only the PsfLauncher and PsfRuntime.

>  Manually add the full PSF to a copy of the package, while taking out InstalledLocationVirtualization from the AppXManifest.

The solution was made using Visual Studio 2026 and uses no external co
mponents like Nuget packages, except for the SDK which is used in the MSIX package.

The InnoInstaller projects use InnoSetup, which is a free installer you must add to your build system.  
You can download it from https://jrsoftware.org/isinfo.php.  
The InnoSetup 6 projects are configured to use the InnoSetup compiler that is installed with the InnoSetup IDE.

The WPF projects are AnyCPU, and the WinUI 3 projects are x64. 


*3) Building the Projects*

The solution and projects were made using Visual Studio 2026, plus the addition of the InnoSetup IDE 6 for the InnoSetup installer projects.  

> You will need to install Visual Studio to build the solution, although VSCode and/or MSBuild might work.

> You will need InnoSetup IDE 6 to build the solution to create the native scenario installers.

> Support for building C# and WinUI 3 applications are needed in Visual Studio or other build tools.  

> .Net Framework 4.8.1 and DotNet 10 support are needed on the system.

> VC++ redist is recommended to be installed on the build system, but probably not required.

> THe Windows SDK is needed. You can download it from https://developer.microsoft.com/en-us/windows/downloads/windows-sdk/.

> The Windows App SDK is needed to build the MSIX packages.  You can find it in the Visual Studio Marketplace at https://marketplace.visualstudio.com/items?itemName=WindowsAppSDKTeam.WindowsAppSDKExtensionforVisualStudio.

> The Windows Application Packaging Project is part of the Visual Studio installation, but you will need to install the MSIX Packaging Tools from the Microsoft Store to be able to build the MSIX packages. 

> There may be additional requirements that I failed to mention.

You build the solution by using the BatchBuild feature of Visual Studio, which will build all the projects in the solution.
You should set the active configuration to "Release" and the active platform to "x64" before building.  
Note that all WPF projects are built for AnyCPU, and all WinUI 3 projects are built for x64. 

The InnoSetup projects will build the native installers, and the MSIX packaging projects will build the MSIX packages for the non-PSF scenario.
You will need to manually add portions of the PSF (and modify the AppXManifest) to a copy of the MSIX package to create the PsfLauncher and Full PSF scenarios as noted in the Scenario details later on.

All final build outputs needed for the test system will appear in a "Releases" folder of the solution.


*4) PowerShell Scripts*

The PowerShell scripts are used to automate the testing on your test system.

> **RunATest.ps1** is the workhorse that will run a given application test for a given scenario using a given workload multiple times, timing each run and reporting on the results to the screen.

The following scripts (replacing ### with WPF or WinUI) are the primary scripts that you would execute to test a scenario.  Each will call RunATest to conduct mupliple repeated tests against each workload in the scenario.

> **Run###NativeTests.ps1** is called to test the native application scenario for each workload.

> **Run###PackagedTests.ps1** is called to test the scenario of the application running in the MSIX container without the presence of the PSF.

> **Run###PackagedLauncherTests.ps1** is called to test the scenarios where only the PsfLauncher is present, without the PSF fixups in the package.

> **Run###PackagedFullPsfTests.ps1** is called to test the scenarios where the Full PSF is present in the package.

The scenario scripts display summary results to the screen, 
but also record individual test run results plus summary of each 
scenario-workload to a CSV file placed in a Results folder of the folder containing the script.

There is an additional script that you don't need, but can be useful to quickly determine if the committed memory leak exists on your test system.
This leak in committed memory by the OS can crash your test system if not managed.  See the details in the script for more information.

> **ReproduceBug.ps1** is a script that you can use to verify if your system has an OS bug discovered recently that can cause your system to run out of virtual memory and crash when running these tests.  See details in the script.


*5) Running the Tests*

The Test system should be Windows 11 (or later) and should be prepared with:

> The .Net Framework 4.8.1 desktop runtime is needed to run the WPF application.

> The .Net 10 Runtime x64 (or later) installed to run the WinUI 3 packaged application.  

> The .Net 10 Desktop Runtime (or later) installed to run the WinUI 3 packaged application.  

> The Windows App Runtime 2.4.0 is needed to run the WinUI 3 applications.

Copy the two native installers, and the MSIX packages, along with the above powershell script files to your test machine.
You are responsible for installing the native installer or MSIX package prior to running a scenario script (although some scripts might attempt to install for you if missing).

After running a scenario script, you are responsible for uninstalling also (or better, use a snapshot).
Due to application aliases, it is best to only have one test application installed at a time.

The native installer for the unpackaged WinUI 3 application will install the WindowsAppRuntime installer dependency for version 2.4.0.
MSIX package will install into the appropriate WindowsApps folder.

A scenario run should be done with a clean system, freshly rebooted, and with no other applications running.
Unnecessary background processes should be stopped or disabled.
It is recommended to disable the antivirus software with an exclusion for the named processes, as some systems treat unsigned exe's differently when in an MSIX container, 
affecting result times for comparison purposes.

You should also be sure to have a snapshot of the system so you can restore it to a clean state after each scenario run.
After logging in, a delay of several minutes is advised before starting a scenario run to calm the system down.