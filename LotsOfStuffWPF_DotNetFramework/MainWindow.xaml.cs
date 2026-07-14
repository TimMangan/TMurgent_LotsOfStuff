using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private int TestNumber = 0;
        private int TestRun = 0;

        BackgroundWorker workerTestRun = null;

        private int SleepTime = 50; // Default sleep time in milliseconds
        private int MaxCount = 50000;
        private int CurrentCount = 0;
        double Delta;
        private int LastProgress = 0;
        private bool CloseAfterTest = true;



        public MainWindow()
        {
            InitializeComponent();

            ProcessCommandLineArgs();

            RunTest(TestNumber, TestRun);

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
                        MessageBox.Show("Help: This application needs a test number.");
                        Close();
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
                    TestLabel.Text += $": Write, then read {MaxCount} registry values.";
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
                    MessageBox.Show("Invalid test number.");
                    break;
            }
        }

       
    }
}
