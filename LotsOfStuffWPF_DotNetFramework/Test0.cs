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
using System.ComponentModel; // for Backgroundworker
using System.Diagnostics;

namespace LotsOfStuffWPF_DotNetFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private void Test0()
        {
            workerTestRun = new BackgroundWorker();
            workerTestRun.WorkerSupportsCancellation = false;
            workerTestRun.WorkerReportsProgress = true;
            workerTestRun.DoWork += Test0_DoWork;
            workerTestRun.ProgressChanged += Test0_ProgressChanged;
            workerTestRun.RunWorkerCompleted += Test0_RunWorkerCompleted;
            workerTestRun.RunWorkerAsync();
        }

        private void Test0_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            System.Threading.Thread.CurrentThread.Name = "Test0";

            for (int i = 0; i <= 100; i++)
            {
            //    System.Threading.Thread.Sleep(SleepTime); // Simulate work
                worker.ReportProgress(i);
            }

            worker.ReportProgress(100);
        }

        private void Test0_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int Value = e.ProgressPercentage;
            progressBar.Value = Value;
            progressText.Text = $"Test Progress: {Value}%";
        }

        private void Test0_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            Debug.WriteLine($"Test0_RunWorkerCompleted");
            workerTestRun = null;
            if (CloseAfterTest)
                Close();
        }
    }
}