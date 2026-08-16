using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
// (BackgroundWorker removed)
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
using Microsoft.Win32;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LotsOfStuffWinUI_DotNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private async void Test4()
        {
            var progress = new Progress<int>(value =>
            {
                progressBar.Value = value;
                progressText.Text = $"Test Progress: {value}%";
            });

            await Task.Run(() =>
            {
                System.Threading.Thread.CurrentThread.Name = "Test4";

                CurrentCount = 0;
                LastProgress = 0;

                using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default))
                using (var swKey = baseKey.OpenSubKey("Software", writable: true))
                {
                    if (swKey != null)
                    {
                        using (var testKey = swKey.CreateSubKey("Test4_BaseKey"))
                        {
                            if (testKey != null)
                            {
                                Test3_WriteRegistryStuff(testKey, (IProgress<int>)progress);

                                CurrentCount = 0;
                                LastProgress = 0;
                                Test2_EnumerateRegistryStuff(testKey, (IProgress<int>)progress);
                            }
                        }
                    }
                }

                ((IProgress<int>)progress).Report(100);
            });

            if (CloseAfterTest)
                Close();
        }

        // Progress reporting moved to IProgress<int>; old BackgroundWorker event handlers removed
    }
}