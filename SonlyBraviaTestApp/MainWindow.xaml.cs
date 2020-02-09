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
using SonlyControlTestApp.Properties;
using SonyBraviaControl;

namespace SonlyControlTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IpTextbox.Text = Settings.Default.IpAddress;
            PskTextBox.Text = Settings.Default.Psk;
        }
        
        private void GetPower_OnClick(object sender, RoutedEventArgs e)
        {
            string ip = IpTextbox.Text;
            string psk = PskTextBox.Text;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(psk))
            {
                MessageBox.Show("Must enter an IP and/or PSK");
                return;
            }

            var result = BraviaCommands.GetPowerStatus(ip, psk);
            if (result != null && result.Result != null)
            {
                UpdateOutputText(result.Result[0].Status);
            }
            else
            {
                UpdateOutputText("Command Failed");
            }
        }

        private void TurnOn_Click(object sender, RoutedEventArgs e)
        {
            string ip = IpTextbox.Text;
            string psk = PskTextBox.Text;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(psk))
            {
                MessageBox.Show("Must enter an IP and/or PSK");
                return;
            }
            UpdateOutputText(BraviaCommands.SetPowerStatus(ip, psk, BraviaPowerStatus.Active) ? "Turned On" : "Failed");
        }

        private void TurnOff_Click(object sender, RoutedEventArgs e)
        {
            string ip = IpTextbox.Text;
            string psk = PskTextBox.Text;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(psk))
            {
                MessageBox.Show("Must enter an IP and/or PSK");
                return;
            }
            UpdateOutputText(BraviaCommands.SetPowerStatus(ip, psk, BraviaPowerStatus.Standby) ? "Turned Off" : "Command Failed");
            
        }

        private void GetVolumeInfo_Click(object sender, RoutedEventArgs e)
        {
            string ip = IpTextbox.Text;
            string psk = PskTextBox.Text;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(psk))
            {
                MessageBox.Show("Must enter an IP and/or PSK");
                return;
            }
            var result = BraviaCommands.GetVolumeInformation(ip, psk);
            if (result != null && result.Result != null)
            {
                UpdateOutputText("Speaker Volume: " + result.Result[0].First(x => x.Target == "speaker").Volume.ToString());
                UpdateOutputText("Headphone Volume: " + result.Result[0].First(x => x.Target == "headphone").Volume.ToString());
            }
            else
            {
                UpdateOutputText("Command Failed");
            }
        }

        private void TurnVolumeUpOne_Click(object sender, RoutedEventArgs e)
        {
            string ip = IpTextbox.Text;
            string psk = PskTextBox.Text;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(psk))
            {
                MessageBox.Show("Must enter an IP and/or PSK");
                return;
            }
            UpdateOutputText(BraviaCommands.ChangeVolume(ip, psk, 1) ? "Volume changed up 1" : "Command Failed");
        }

        private void TurnVolumeDownOne_Click(object sender, RoutedEventArgs e)
        {
            string ip = IpTextbox.Text;
            string psk = PskTextBox.Text;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(psk))
            {
                MessageBox.Show("Must enter an IP and/or PSK");
                return;
            }
            UpdateOutputText(BraviaCommands.ChangeVolume(ip, psk, -1) ? "Volume changed down 1" : "Command Failed");
        }

        private void SetVolumeToTen_Click(object sender, RoutedEventArgs e)
        {
            string ip = IpTextbox.Text;
            string psk = PskTextBox.Text;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(psk))
            {
                MessageBox.Show("Must enter an IP and/or PSK");
                return;
            }
            UpdateOutputText(BraviaCommands.SetVolume(ip, psk, 10) ? "Volume changed to 10" : "Command Failed");
        }

        private void GetInput_Click(object sender, RoutedEventArgs e)
        {
            string ip = IpTextbox.Text;
            string psk = PskTextBox.Text;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(psk))
            {
                MessageBox.Show("Must enter an IP and/or PSK");
                return;
            }
            var result = BraviaCommands.GetPlayingContentInformation(ip, psk);
            if (result != null && result.Result != null)
            {
                UpdateOutputText(result.Result[0].Title);
            }
            else
            {
                UpdateOutputText("Command Failed");
            }
        }

        private void SetToHDMIOne_Click(object sender, RoutedEventArgs e)
        {
            string ip = IpTextbox.Text;
            string psk = PskTextBox.Text;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(psk))
            {
                MessageBox.Show("Must enter an IP and/or PSK");
                return;
            }
            UpdateOutputText(BraviaCommands.SetPlayingContent(ip, psk, BraviaPlayContent.HDMI1) ? "Set to HDMI 1" : "Command Failed");
        }

        private void SetToHDMITwo_Click(object sender, RoutedEventArgs e)
        {
            string ip = IpTextbox.Text;
            string psk = PskTextBox.Text;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(psk))
            {
                MessageBox.Show("Must enter an IP and/or PSK");
                return;
            }
            UpdateOutputText(BraviaCommands.SetPlayingContent(ip, psk, BraviaPlayContent.HDMI2) ? "Set to HDMI 2" : "Command Failed");
        }

        private void SetToHDMIThree_Click(object sender, RoutedEventArgs e)
        {
            string ip = IpTextbox.Text;
            string psk = PskTextBox.Text;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(psk))
            {
                MessageBox.Show("Must enter an IP and/or PSK");
                return;
            }
            UpdateOutputText(BraviaCommands.SetPlayingContent(ip, psk, BraviaPlayContent.HDMI3) ? "Set to HDMI 3" : "Command Failed");
        }

        private void UpdateOutputText(string text)
        {
            OutputText.Text += text;
            OutputText.Text += Environment.NewLine;
            OutputText.ScrollToEnd();
        }

        private void SetToTuner_Click(object sender, RoutedEventArgs e)
        {
            string ip = IpTextbox.Text;
            string psk = PskTextBox.Text;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(psk))
            {
                MessageBox.Show("Must enter an IP and/or PSK");
                return;
            }
            var result = BraviaCommands.GetPlayContentsList(ip, psk);
            UpdateOutputText(BraviaCommands.SetPlayingContent(ip, psk, BraviaPlayContent.Component1) ? "Set to Tuner" : "Command Failed");
        }

        private void IpTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Default.IpAddress = IpTextbox.Text;
            Settings.Default.Save();
        }

        private void PskTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Default.Psk = PskTextBox.Text;
            Settings.Default.Save();
        }
    }
}
