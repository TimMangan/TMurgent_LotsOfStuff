using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Diagnostics;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LotsOfStuffWinUI_DotNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private async void Test6()
        {
            var progress = new Progress<int>(value =>
            {
                progressBar.Value = value;
                progressText.Text = $"Test Progress: {value}%";
            });


            await Task.Run(() =>
            {
                System.Threading.Thread.CurrentThread.Name = "Test6";

                CurrentCount = 0;
                LastProgress = 0;

                // Ensure the root folder exists (use Directory.CreateDirectory which is important)
                //string rootPath = System.IO.Path.GetFullPath("Test6Folder");
                string rootPath = System.IO.Path.Combine("C:", "Test6Folder");
                DirectoryInfo directory = System.IO.Directory.CreateDirectory(rootPath);
                if (directory == null || !directory.Exists)
                {
                    Debug.WriteLine($"Test6 Unable to create directory " + rootPath);
                }
                else
                {
                    Debug.WriteLine($"Test6 Created directory " + rootPath);
                    Test6_CreateFiles(directory, (IProgress<int>)progress);
                }

                ((IProgress<int>)progress).Report(100);
            });

            Debug.WriteLine($"Test6_RunCompleted");
            if (CloseAfterTest)
                Close();
        }

        private void Test6_CreateFiles(DirectoryInfo directory, IProgress<int> progress)
        {
            try
            {
                // Don't put more than 100 files per subdirectory to avoid too many files in one directory
                CurrentCount = 0;
                int MaxPerDirectory = 100;
                int OnSubDirectory = 0;
                bool needNewSubDirectory = true;
                DirectoryInfo? SubDirectory = null;
                while (CurrentCount < MaxCount)
                {
                    if (needNewSubDirectory)
                    {
                        OnSubDirectory++;
                        string subPath = System.IO.Path.Combine(directory.FullName, $"SubDir_{OnSubDirectory}");
                        SubDirectory = System.IO.Directory.CreateDirectory(subPath);
                        needNewSubDirectory = false;
                    }
                    for (int i = 0; i < MaxPerDirectory && CurrentCount < MaxCount; i++)
                    {
                        if (SubDirectory != null)
                        {
                            string fileName = System.IO.Path.Combine(SubDirectory.FullName, $"TestFile_{CurrentCount}.txt");
                            try
                            {
                                using (StreamWriter writer = new StreamWriter(fileName))
                                {
                                    writer.WriteLine($"This is test file number {CurrentCount}");
                                }
                            }
                            catch (IOException)
                            {
                                // Skip files we can't create and continue
                                continue;
                            }
                            CurrentCount++;
                            double prog = (CurrentCount / (double)MaxCount) * 100;
                            if ((int)prog > LastProgress)
                            {
                                LastProgress = (int)prog;
                                progress.Report((int)prog);
                            }
                        }
                    }
                    needNewSubDirectory = true;                    
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Handle the exception (e.g., log it, ignore it, etc.)
            }
        }

    }
}