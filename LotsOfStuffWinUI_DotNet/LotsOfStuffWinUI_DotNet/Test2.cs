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
using Microsoft.Win32;
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

        private async void Test2()
        {
            var progress = new Progress<int>(value =>
            {
                progressBar.Value = value;
                progressText.Text = $"Test Progress: {value}%";
            });

            await Task.Run(() =>
            {
                System.Threading.Thread.CurrentThread.Name = "Test2";

                CurrentCount = 0;
                LastProgress = 0;

                using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default))
                {
                    Test2_EnumerateRegistryStuff(baseKey, (IProgress<int>)progress);
                }

                ((IProgress<int>)progress).Report(100);
            });

            if (CloseAfterTest)
                Close();
        }
        private void Test2_EnumerateRegistryStuff(RegistryKey key, IProgress<int> progress)
        {
            try
            {
                if (CurrentCount >= MaxCount)
                    return;

                foreach (string subkeyName in key.GetSubKeyNames())
                {
                    try
                    {
                        using (var subkey = key.OpenSubKey(subkeyName))
                        {
                            if (subkey != null)
                            {
                                // Process the subkey here
                                CurrentCount++;
                                double prog = (CurrentCount / (double)MaxCount) * 100;
                                if ((int)prog > LastProgress)
                                {
                                    LastProgress = (int)prog;
                                    progress.Report((int)prog);
                                }
                                // Recursively enumerate subkeys
                                Test2_EnumerateRegistryStuff(subkey, progress);
                            }
                        }
                        if (CurrentCount >= MaxCount)
                        {
                            return; // Stop enumerating if the maximum count is reached
                        }
                    }
                    catch
                    {
                        ;
                    }
                }

                foreach (string valueName in key.GetValueNames())
                {
                    CurrentCount++;
                    double prog = (CurrentCount / (double)MaxCount) * 100;
                    if ((int)prog > LastProgress)
                    {
                        LastProgress = (int)prog;
                        progress.Report((int)prog);
                    }
                    if (CurrentCount >= MaxCount)
                    {
                        return; // Stop enumerating if the maximum count is reached
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // ignore keys we can't access
            }
            catch (IOException)
            {
                // ignore IO issues and continue
            }
        }

        // Progress reporting moved to IProgress<int>; old BackgroundWorker event handlers removed
    }
}