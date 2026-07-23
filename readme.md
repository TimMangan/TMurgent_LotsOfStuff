**TMurgent_LotsOfStuff**

This software is associated with some performance testing done to determine if MSIX and/or the Package Support Framework cause undue performanace degredation over native applications.

It consists of a purpose build test application that takes a runtime argument for a workload to include in the test.  
Currently there is only one test application, written in C# for the .Net Framework runtime.  
Additional test applications, such as unmanaged C++ for Win32, or C# for .Net Runtime,  could be added in the future.

There is also a Windows Application Project that will create an MSIX package without the PSF.  You must edit the project file to configure your code-signing certificate.
There is an assumption that you will manually add the PSF to a copy of the package, once to add only the PsfLauncher (and PsfRuntime) and once to add the full PSF with fixups.

The PowerShell scripts are used to automate the testing.

>RunATest.ps1 is the workhorse that will run a given application test for a given scenario using a given workload multiple times, timing each run and reporting on the results to the screen.

>RunNativeTests.ps1 is called to test the native application scenario for each workload.

>RunPackagedTests.ps1 is called to test the scenario of the application running in the MSIX container without the presence of the PSF.

>RunPackagedLauncherTests.ps1 is called to test the scenarios where only the PsfLauncher is present, or when the Full PSF is present in the package.

The containerized tests assume that the appropriate package has already been installed.

>ReproduceBug.ps1 is a script that you can use to verify if your system has an OS bug discovered recently that can cause your system to run out of virtual memory and crash when running these tests.  See details in the script.

The solution was made using Visual Studio 2026 and uses no external components like Nuget packages..