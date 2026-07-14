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

        private void Test5()
        {
            workerTestRun = new BackgroundWorker();
            workerTestRun.WorkerSupportsCancellation = false;
            workerTestRun.WorkerReportsProgress = true;
            workerTestRun.DoWork += Test5_DoWork;
            workerTestRun.ProgressChanged += Test5_ProgressChanged;
            workerTestRun.RunWorkerCompleted += Test5_RunWorkerCompleted;
            workerTestRun.RunWorkerAsync();
        }

        private void Test5_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            System.Threading.Thread.CurrentThread.Name = "Test5";

            DirectoryInfo directory = new DirectoryInfo("C:\\");
            Test5_EnumerateDirectory(directory);

            worker.ReportProgress(100);
        }

        private void Test5_EnumerateDirectory(DirectoryInfo directory)
        {
            try
            {
                foreach (var subdirectory in directory.GetDirectories())
                {
                    CurrentCount++;
                    double progress = (CurrentCount / (double)MaxCount) * 100;
                    if ((int)progress > LastProgress)
                    {
                        LastProgress = (int)progress;
                        workerTestRun.ReportProgress((int)progress);
                    }
                    // Process the subdirectory here
                    Test5_EnumerateDirectory(subdirectory);
                    if (CurrentCount >= MaxCount)
                    {
                        return; // Stop enumerating if the maximum count is reached
                    }
                }
                foreach (var file in directory.GetFiles())
                {
                    CurrentCount++;
                    double progress = (CurrentCount / (double)MaxCount) * 100;
                    if ((int)progress > LastProgress)
                    {
                        LastProgress = (int)progress;
                        workerTestRun.ReportProgress((int)progress);
                    }
                    // Process the file 
                    if (CurrentCount >= MaxCount)
                    {
                        return; // Stop enumerating if the maximum count is reached
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Handle the exception (e.g., log it, ignore it, etc.)
            }
        }
        private void Test5_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int Value = e.ProgressPercentage;
            progressBar.Value = Value;
            progressText.Text = $"Test Progress: {Value}%";
        }

        private void Test5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine($"Test5_RunWorkerCompleted: CurrentCount = {CurrentCount}");
            workerTestRun = null;
            if (CloseAfterTest)
                Close();
        }
    }
}