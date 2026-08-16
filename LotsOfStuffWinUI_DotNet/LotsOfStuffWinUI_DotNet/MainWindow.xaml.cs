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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LotsOfStuffWinUI_DotNet
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private int TestNumber = 0;
        private int TestRun = 0;

        private int SleepTime = 50; // Default sleep time in milliseconds
        private int MaxCount = 50000;
        private int CurrentCount = 0;
        private int LastProgress = 0;
        private bool CloseAfterTest = true;

        public MainWindow()
        {
            InitializeComponent();
            ProcessCommandLineArgs();

            // Keep progress text visible and respond to changes
            progressText.Text = "0%";
            progressText.Visibility = Visibility.Visible;

            // Subscribe to value changes to update overlay text
            progressBar.ValueChanged += ProgressBar_ValueChanged;

            RunTest(TestNumber, TestRun);
        }

        private void ProgressBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            // Keep UI update simple and immediate
            try
            {
                var value = (int)Math.Round(e.NewValue);
                progressText.Text = $"{value}%";
                progressText.Visibility = Visibility.Visible;
            }
            catch
            {
                // swallow any UI-formatting exceptions to avoid crashing tests
            }
        }

        private void ProcessCommandLineArgs()
        {
            string[] args = Environment.GetCommandLineArgs();
            // Process command line arguments here
            if (args.Length > 1)
            {
                // Example: Handle a specific command line argument
                switch (args[1])
                {
                    case "0":
                        TestNumber = 0;
                        break;
                    case "1":
                        TestNumber = 1;
                        break;
                    case "2":
                        TestNumber = 2;
                        break;
                    case "3":
                        TestNumber = 3;
                        break;
                    case "4":
                        TestNumber = 4;
                        break;
                    case "5":
                        TestNumber = 5;
                        break;
                    case "6":
                        TestNumber = 6;
                        break;
                    case "-help":
                    default:
                        ShowDialogAndClose("Help", "Help: This application needs a test number.");
                        break;
                }
                if (args.Length > 2)
                {
                    try
                    {
                        TestRun = int.Parse(args[2]);
                    }
                    catch { }
                }
            }
            else
            {
                //TestNumber = 6;
            }
        }
        private void RunTest(int testNumber, int testRun)
        {
            TestLabel.Text = $"Running Test {testNumber} Run {testRun}";
            progressText.Text = "0%";
            progressText.Visibility = Visibility.Visible;
            switch (testNumber)
            {
                case 0:
                    TestLabel.Text += ": Baseline with no activity.";
                    CloseAfterTest = true;
                    Test0();
                    break;
                case 1:
                    TestLabel.Text += ": Sleeps for 50ms, 100 times.";
                    CloseAfterTest = true;
                    Test1();
                    break;
                case 2:
                    TestLabel.Text += $": Read {MaxCount} registry keys/values.";
                    MaxCount = 50000;
                    CloseAfterTest = true;
                    Test2();
                    break;
                case 3:
                    TestLabel.Text += $": Write {MaxCount} registry values.";
                    MaxCount = 50000;
                    CloseAfterTest = true;
                    Test3();
                    break;
                case 4:
                    MaxCount = 50000;
                    CloseAfterTest = true;
                    TestLabel.Text += $": Write, then enumerate {MaxCount} registry values.";
                    Test4();
                    break;
                case 5:
                    MaxCount = 50000;
                    CloseAfterTest = true;
                    TestLabel.Text += $": Enumerate {MaxCount} files.";
                    Test5();
                    break;
                case 6:
                    MaxCount = 50000;
                    CloseAfterTest = true;
                    TestLabel.Text += $": Create {MaxCount} files.";
                    Test6();
                    break;
                default:
                    ShowDialog("Invalid", "Invalid test number.");
                    break;
            }
        }

        private void ShowDialog(string title, string content)
        {
            // Create dialog, set XamlRoot so it can be shown from a Window
            var dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK",
                XamlRoot = this.Content?.XamlRoot
            };

            // Fire-and-forget showing on UI thread
            _ = dialog.ShowAsync();
        }

        private void ShowDialogAndClose(string title, string content)
        {
            ShowDialog(title, content);
            Close();
        }

        

    }
}
