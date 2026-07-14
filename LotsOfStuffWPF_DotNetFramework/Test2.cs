using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using Microsoft.Win32;
using System.Diagnostics; // for Backgroundworker

namespace LotsOfStuffWPF_DotNetFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private void Test2()
        {
            workerTestRun = new BackgroundWorker();
            workerTestRun.WorkerSupportsCancellation = false;
            workerTestRun.WorkerReportsProgress = true;
            workerTestRun.DoWork += Test2_DoWork;
            workerTestRun.ProgressChanged += Test2_ProgressChanged;
            workerTestRun.RunWorkerCompleted += Test2_RunWorkerCompleted;
            workerTestRun.RunWorkerAsync();
        }

        private void Test2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            System.Threading.Thread.CurrentThread.Name = "Test2";

            Delta = 100.0 / MaxCount;
            CurrentCount = 0;
            LastProgress = 0;

            RegistryKey key = Registry.CurrentUser;
            Test2_EnumerateRegistryStuff(key);
            

            worker.ReportProgress(100);
        }
        private void Test2_EnumerateRegistryStuff(RegistryKey key)
        {
            foreach (string subkeyName in key.GetSubKeyNames())
            {
                using (RegistryKey subkey = key.OpenSubKey(subkeyName))
                {
                    if (subkey != null)
                    {
                        // Process the subkey here
                        CurrentCount++;
                        double progress = (CurrentCount / (double)MaxCount) * 100;
                        if ((int)progress > LastProgress)
                        {
                            LastProgress = (int)progress;
                            workerTestRun.ReportProgress((int)progress);
                        }
                        // Recursively enumerate subkeys
                        Test2_EnumerateRegistryStuff(subkey);
                    }
                    if (CurrentCount >= MaxCount)
                    {
                        return; // Stop enumerating if the maximum count is reached
                    }
                }                
            }
            foreach (string valueName in key.GetValueNames())
            {
                CurrentCount++;
                double progress = (CurrentCount / (double)MaxCount) * 100;
                if ((int)progress > LastProgress)
                {
                    LastProgress = (int)progress;
                    workerTestRun.ReportProgress((int)progress);
                }
                if (CurrentCount >= MaxCount)
                {
                    return; // Stop enumerating if the maximum count is reached
                }
            }
        }

        private void Test2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int Value = e.ProgressPercentage;
            progressBar.Value = Value;
            progressText.Text = $"Test Progress: {Value}%";
        }

        private void Test2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int something = CurrentCount;  //100000 = x18680
            Debug.WriteLine($"Test2_RunWorkerCompleted: CurrentCount = {something}");
            workerTestRun = null;
            if (CloseAfterTest)
                Close();
        }
    }
}