using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
// using System.ComponentModel; // BackgroundWorker removed
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
using System.Diagnostics;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LotsOfStuffWinUI_DotNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private async void Test0()
        {
            // Modernized: use Task.Run + IProgress<int> instead of BackgroundWorker
            var progress = new Progress<int>(value =>
            {
                progressBar.Value = value;
                progressText.Text = $"Test Progress: {value}%";
            });

            await Task.Run(() =>
            {
                System.Threading.Thread.CurrentThread.Name = "Test0";
                //for (int i = 0; i <= 100; i++)
                //{
                //    // Simulate work would use Task.Delay in an async loop; here this is the fast baseline
                //    ((IProgress<int>)progress).Report(i);
                //}
                ((IProgress<int>)progress).Report(100);
            });

            Debug.WriteLine($"Test0_RunCompleted");
            if (CloseAfterTest)
                Close();
        }
    }
}