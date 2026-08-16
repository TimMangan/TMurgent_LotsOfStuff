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

        private async void Test3()
        {
            var progress = new Progress<int>(value =>
            {
                progressBar.Value = value;
                progressText.Text = $"Test Progress: {value}%";
            });

            await Task.Run(() =>
            {
                System.Threading.Thread.CurrentThread.Name = "Test3";

                CurrentCount = 0;
                LastProgress = 0;

                using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default))
                using (var swKey = baseKey.OpenSubKey("Software", writable: true))
                {
                    if (swKey != null)
                    {
                        using (var testKey = swKey.CreateSubKey("Test3_BaseKey"))
                        {
                            if (testKey != null)
                            {
                                Test3_WriteRegistryStuff(testKey, (IProgress<int>)progress);
                            }
                        }
                    }
                }

                ((IProgress<int>)progress).Report(100);
            });

            if (CloseAfterTest)
                Close();
        }

        private void Test3_WriteRegistryStuff(RegistryKey key, IProgress<int> progress)
        {
            for (int keyindex = 0; keyindex < MaxCount / 1000; keyindex++)
            {
                string subkeyName = $"Test3_Subkey_{keyindex}";
                using (var subkey = key.CreateSubKey(subkeyName))
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
                            int prog = (int)(CurrentCount * 100.0 / MaxCount);
                            if (prog > LastProgress)
                            {
                                LastProgress = prog;
                                progress.Report(prog);
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

        // Progress reporting moved to IProgress<int>; old BackgroundWorker event handlers removed
    }
}