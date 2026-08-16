using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LotsOfStuffWinUI_DotNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private async void Test5()
        {
            var progress = new Progress<int>(value =>
            {
                progressBar.Value = value;
                progressText.Text = $"Test Progress: {value}%";
            });

            await Task.Run(() =>
            {
                System.Threading.Thread.CurrentThread.Name = "Test5";

                CurrentCount = 0;
                LastProgress = 0;

                DirectoryInfo directory = new DirectoryInfo("C:\\");
                Test5_EnumerateDirectoryIterative(directory, (IProgress<int>)progress);

                ((IProgress<int>)progress).Report(100);
            });

            if (CloseAfterTest)
                Close();
        }

        private void Test5_EnumerateDirectoryIterative(DirectoryInfo root, IProgress<int> progress)
        {
            var stack = new Stack<DirectoryInfo>();
            stack.Push(root);

            while (stack.Count > 0 && CurrentCount < MaxCount)
            {
                var dir = stack.Pop();
                try
                {
                    foreach (var subdirectory in dir.EnumerateDirectories())
                    {
                        CurrentCount++;
                        double prog = (CurrentCount / (double)MaxCount) * 100;
                        if ((int)prog > LastProgress)
                        {
                            LastProgress = (int)prog;
                            progress.Report((int)prog);
                        }
                        // push for later processing
                        stack.Push(subdirectory);
                        if (CurrentCount >= MaxCount)
                            break;
                    }

                    if (CurrentCount >= MaxCount)
                        break;

                    foreach (var file in dir.EnumerateFiles())
                    {
                        CurrentCount++;
                        double prog = (CurrentCount / (double)MaxCount) * 100;
                        if ((int)prog > LastProgress)
                        {
                            LastProgress = (int)prog;
                            progress.Report((int)prog);
                        }
                        if (CurrentCount >= MaxCount)
                            break;
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // ignore directories we can't access
                }
                catch (DirectoryNotFoundException)
                {
                    // directory disappeared, ignore
                }
            }
        }

    }
}