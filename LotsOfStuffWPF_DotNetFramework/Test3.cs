using Microsoft.Win32; // for Backgroundworker
using System;
using System.Collections.Generic;
using System.ComponentModel; // for Backgroundworker
using System.Diagnostics;
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

namespace LotsOfStuffWPF_DotNetFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private void Test3()
        {
            workerTestRun = new BackgroundWorker();
            workerTestRun.WorkerSupportsCancellation = false;
            workerTestRun.WorkerReportsProgress = true;
            workerTestRun.DoWork += Test3_DoWork;
            workerTestRun.ProgressChanged += Test3_ProgressChanged;
            workerTestRun.RunWorkerCompleted += Test3_RunWorkerCompleted;
            workerTestRun.RunWorkerAsync();
        }

        private void Test3_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            System.Threading.Thread.CurrentThread.Name = "Test3";

            Delta = 100.0 / MaxCount;
            CurrentCount = 0;
            LastProgress = 0;

            RegistryKey key = Registry.CurrentUser;
            RegistryKey testKey = key.CreateSubKey("Test3_BaseKey");
            Test3_WriteRegistryStuff(testKey);

            worker.ReportProgress(100);
        }

        private void Test3_WriteRegistryStuff(RegistryKey key)
        {
            for (int keyindex=0; keyindex < MaxCount/1000; keyindex++)
            {
                string subkeyName = $"Test3_Subkey_{keyindex}";
                using (RegistryKey subkey = key.CreateSubKey(subkeyName))
                {
                    if (subkey != null)
                    {
                        CurrentCount++;
                        for (int valueindex = 0; valueindex < 1000; valueindex++)
                        {
                            string valueName = $"Value_{valueindex}";
                            string valueData = $"Data_{valueindex}";
                            subkey.SetValue(valueName, valueData);
                            CurrentCount++;
                            int progress = (int)(CurrentCount * 100.0 / MaxCount);
                            if ((int)progress > LastProgress)
                            {
                                LastProgress = (int)progress;
                                workerTestRun.ReportProgress((int)progress);
                                ///((BackgroundWorker)Dispatcher.Invoke(() => workerTestRun)).ReportProgress(progress);
                            }
                            if (CurrentCount >= MaxCount)
                            {
                                return; // Stop enumerating if the maximum count is reached
                            }
                        }

                    }
                }
                if (CurrentCount >= MaxCount)
                {
                    return; // Stop enumerating if the maximum count is reached
                }
            }
        }

        private void Test3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int Value = e.ProgressPercentage;
            progressBar.Value = Value;
            progressText.Text = $"Test Progress: {Value}%";
        }

        private void Test3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine($"Test3_RunWorkerCompleted: CurrentCount = {CurrentCount}");

            workerTestRun = null;
            if (CloseAfterTest)
                Close();
        }
    }
}