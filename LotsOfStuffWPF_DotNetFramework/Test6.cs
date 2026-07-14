using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel; // for Backgroundworker
using System.IO; // for Backgroundworker
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace LotsOfStuffWPF_DotNetFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private void Test6()
        {
            workerTestRun = new BackgroundWorker();
            workerTestRun.WorkerSupportsCancellation = false;
            workerTestRun.WorkerReportsProgress = true;
            workerTestRun.DoWork += Test6_DoWork;
            workerTestRun.ProgressChanged += Test6_ProgressChanged;
            workerTestRun.RunWorkerCompleted += Test6_RunWorkerCompleted;
            workerTestRun.RunWorkerAsync();
        }

        private void Test6_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            System.Threading.Thread.CurrentThread.Name = "Test6";

            DirectoryInfo directory = new DirectoryInfo("Test6Folder");
            if (!directory.Exists)
            {
                directory.Create();
            }
            Test6_CreateFiles(directory);

            worker.ReportProgress(100);
        }

        private void Test6_CreateFiles(DirectoryInfo directory)
        {
            try
            {
                // Don't put more than 100 files per subdirectory to avoid too many files in one directory
                CurrentCount = 0;
                int MaxPerDirectory = 100;
                int OnSubDirectory = 0;
                bool needNewSubDirectory = true;
                DirectoryInfo SubDirectory = null;
                while (CurrentCount < MaxCount)
                {
                    if (needNewSubDirectory)
                    {
                        OnSubDirectory++;
                        SubDirectory = new DirectoryInfo(System.IO.Path.Combine(directory.FullName, $"SubDir_{OnSubDirectory}"));
                        SubDirectory.Create();
                        needNewSubDirectory = false;
                    }
                    for (int i = 0; i < MaxPerDirectory && CurrentCount < MaxCount; i++)
                    {
                        string fileName = System.IO.Path.Combine(SubDirectory.FullName, $"TestFile_{CurrentCount}.txt");
                        using (StreamWriter writer = new StreamWriter(fileName))
                        {
                            writer.WriteLine($"This is test file number {CurrentCount}");
                        }
                        CurrentCount++;
                        double progress = (CurrentCount / (double)MaxCount) * 100;
                        if ((int)progress > LastProgress)
                        {
                            LastProgress = (int)progress;
                            workerTestRun.ReportProgress((int)progress);
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
        private void Test6_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int Value = e.ProgressPercentage;
            progressBar.Value = Value;
            progressText.Text = $"Test Progress: {Value}%";
        }

        private void Test6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine($"Test6_RunWorkerCompleted: CurrentCount = {CurrentCount}");
            workerTestRun = null;
            if (CloseAfterTest)
                Close();
        }
    }
}