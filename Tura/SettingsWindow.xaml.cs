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
using System.Windows.Shapes;

namespace Tura
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            RetrieveSettings();
        }

        void RetrieveSettings()
        {
            if (Properties.Settings.Default.ForeColor == "White")
                LightThemeRadioButton.IsChecked = true;
            else
                DarkThemeRadioButton.IsChecked = true;
        }

        private void DarkThemeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SetDarkTheme();
        }

        void SetDarkTheme()
        {
            Properties.Settings.Default.ForeColor = "White";
            Properties.Settings.Default.BackColor = "Black";
            Properties.Settings.Default.Accent = "Gray";
        }

        private void LightThemeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SetLightTheme();
        }

        void SetLightTheme()
        {
            Properties.Settings.Default.ForeColor = "Black";
            Properties.Settings.Default.BackColor = "White";
            Properties.Settings.Default.Accent = "#f0f0f0";
        }
    }
}
