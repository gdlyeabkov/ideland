using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Speech.Synthesis;

namespace Ideland
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public SpeechSynthesizer debugger;

        public MainWindow()
        {
            InitializeComponent();

            debugger = new SpeechSynthesizer();

        }

        private void ExecuteCommandHandler(object sender, KeyEventArgs e)
        {
            TextBox developerCmdInput = ((TextBox)(sender));
            if (Key.Enter == e.Key)
            {
                try
                {
                    Process.Start(developerCmdInput.Text);
                } catch (System.ComponentModel.Win32Exception error)
                {
                    debugger.Speak(error.ToString());
                    developerCmdInput.Text = "";
                }

            }
        }
    }
}
