using Microsoft.Win32; // for Backgroundworker
using System;
using System.Collections.Generic;
using System.ComponentModel; // for Backgroundworker
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

        private void Test4()
        {
            workerTestRun = new BackgroundWorker();
            workerTestRun.WorkerSupportsCancellation = false;
            workerTestRun.WorkerReportsProgress = true;
            workerTestRun.DoWork += Test4_DoWork;
            workerTestRun.ProgressChanged += Test4_ProgressChanged;
            workerTestRun.RunWorkerCompleted += Test4_RunWorkerCompleted;
            workerTestRun.RunWorkerAsync();
        }

        private void Test4_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            System.Threading.Thread.CurrentThread.Name = "Test4"; 

            Delta = 100.0 / MaxCount;
            CurrentCount = 0;
            LastProgress = 0;

            RegistryKey key = Registry.CurrentUser;
            RegistryKey testKey = key.CreateSubKey("Test3_BaseKey");
            Test3_WriteRegistryStuff(testKey);

            CurrentCount = 0;
            LastProgress = 0;
            Test2_EnumerateRegistryStuff(testKey);

            worker.ReportProgress(100);
        }

        private void   Test4_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int Value = e.ProgressPercentage;
            progressBar.Value = Value;
            progressText.Text = $"Test Progress: {Value}%";
        }

        private void Test4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine($"Test4_RunWorkerCompleted: CurrentCount = {CurrentCount}");
            workerTestRun = null;
            if (CloseAfterTest)
                Close();
        }
    }
}