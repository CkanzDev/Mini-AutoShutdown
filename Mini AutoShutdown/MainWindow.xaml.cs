using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Mini_AutoShutdown
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer;
        private int _remainingSeconds;
        private bool _isRunning = false;

        public MainWindow()
        {
            InitializeComponent();

            Window1();

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;
            TimerReset();
        }

        private void TenMinutesButton_Click(object sender, RoutedEventArgs e)
        {
            _remainingSeconds = 600; //10 Minutes
            TimerStart();
            Window2();
        }
        private void FifteenMinutesButton_Click(object sender, RoutedEventArgs e)
        {
            _remainingSeconds = 900; //15 Minutes
            TimerStart();
            Window2();
        }
        private void ThirtyMinutesButton_Click(object sender, RoutedEventArgs e)
        {
            _remainingSeconds = 1800; //30 Minutes
            TimerStart();
            Window2();
        }
        private void OneHourButton_Click(object sender, RoutedEventArgs e)
        {
            _remainingSeconds = 3600; //1 Hour
            TimerStart();
            Window2();
        }

        private void TwoHourButton_Click(object sender, RoutedEventArgs e)
        {
            _remainingSeconds = 7200; //2 Hour
            TimerStart();
            Window2();
        }

        private void ThreeHourButton_Click(object sender, RoutedEventArgs e)
        {
            _remainingSeconds = 10800; //3 Hour
            TimerStart();
            Window2();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            TimerReset();
            Window1();
        }

        private void Window1()
        {
            
            TenMinutesButton.IsEnabled = true;
            TenMinutesButton.Visibility = Visibility.Visible;
            FifteenMinutesButton.Visibility = Visibility.Visible;
            ThirtyMinutesButton.Visibility = Visibility.Visible;
            OneHourButton.Visibility = Visibility.Visible;
            TwoHourButton.Visibility = Visibility.Visible;
            ThreeHourButton.Visibility = Visibility.Visible;
            TimerLabel.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
        }

        private void Window2()
        {
            TenMinutesButton.IsEnabled = false;
            TenMinutesButton.Visibility = Visibility.Collapsed;
            FifteenMinutesButton.Visibility = Visibility.Collapsed;
            ThirtyMinutesButton.Visibility = Visibility.Collapsed;
            OneHourButton.Visibility = Visibility.Collapsed;
            TwoHourButton.Visibility = Visibility.Collapsed;
            ThreeHourButton.Visibility = Visibility.Collapsed;
            TimerLabel.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
            UpdateDisplay();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_remainingSeconds > 0)
            {
                _remainingSeconds--;
                UpdateDisplay();
            }
            else
            {
                TimerStop();
                Shutdown();
            }
        }

        private void TimerStart()
        {
            _timer.Start();
            _isRunning = true;
        }

        private void TimerStop()
        {
            _timer.Stop();
            _isRunning = false;
        }

        public void TimerReset()
        {
            TimerStop();
            _remainingSeconds = 0;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            TimerLabel.Content = TimeSpan.FromSeconds(_remainingSeconds).ToString(@"hh\:mm\:ss");
            TimerLabel.Foreground = _remainingSeconds <= 5 ? Brushes.Red : Brushes.Black;
        }

        private void Shutdown()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "shutdown",
                    Arguments = $"/s /t 0",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                Process.Start(psi);
                System.Diagnostics.Debug.WriteLine("This is a log");
            }
            catch(Exception e)
            {
                TimerReset();
                MessageBox.Show("Error : " + e.Message);
            }

        }
    }
}