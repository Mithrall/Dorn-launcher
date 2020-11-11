using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Engine;
using System.Threading;
using System.Net;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace WPFUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        
        public MainWindow() {
            InitializeComponent();

            IsGameInstalled();

            DataContext = this;
        }

        private void IsGameInstalled() {
            if(File.Exists("2grads.exe")) {
                InstallGrid.Visibility = Visibility.Collapsed;
                Playbtn.Visibility = Visibility.Visible;
            }
        }

        private void WindowDrag(object sender, MouseButtonEventArgs e) {
            try {
                DragMove();
            } catch(InvalidOperationException) {
            }
        }

        private void WindowClose(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void WindowMin(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void WindowDown(object sender, RoutedEventArgs e) {
            Button btn = sender as Button;
            Button btn2 = (Button)this.FindName("windowMaxi");
            btn.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Visible;
            WindowState = WindowState.Normal;
        }

        private void WindowMax(object sender, RoutedEventArgs e) {
            Button btn = sender as Button;
            Button btn2 = (Button)this.FindName("windowRDown");
            btn.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Visible;
            WindowState = WindowState.Maximized;
        }
        
        private void Install(object sender, RoutedEventArgs e) {

            //temp hardcoded (2grads spil)
            string url = "https://dl.dropboxusercontent.com/s/6vuie7ng2s0e3rq/2grads.exe?dl=0";

            using(WebClient wc = new WebClient()) {
                wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new Uri(url),
                    // Param2 = Path to save
                    "2grads.exe"
                );
            }
        }

        // Event to track the progress
        void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            progressBar.Value = e.ProgressPercentage;
            progressPercent.Content = e.ProgressPercentage + "%";
            if(e.ProgressPercentage >= 100) {
                IsGameInstalled();
            }
        }

        private void Playbtn_Click(object sender, RoutedEventArgs e) {
            Process.Start("2grads.exe");
        }
    }
}
