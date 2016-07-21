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

namespace VisionA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RandomLatter();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            PlusFont();
        }

        private void PlusFont()
        {
            var fs = VisionLetter.FontSize;
            fs = Math.Round(fs + (fs/100*10));
            VisionLetter.FontSize = fs;
            LatterFonetSize.Text = fs.ToString();
        }

        private void RundomButton_Click(object sender, RoutedEventArgs e)
        {
            RandomLatter();
        }

        private void RandomLatter()
        {
            var str = "QWERTYUIOPASDFGHJKLZXCVBNM";
            Random rnd = new Random();
            var r = rnd.Next(0, str.Length - 1);
            VisionLetter.Text = str[r].ToString();
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            MinusFont();
        }

        private void MinusFont()
        {
            var fs = VisionLetter.FontSize;
            fs = Math.Round(fs - (fs/100*10));
            VisionLetter.FontSize = fs;
            LatterFonetSize.Text = fs.ToString();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D1:
                    PlusFont();
                    break;
                case Key.D2:
                    MinusFont();
                    break;
                case Key.D3:
                    RandomLatter();
                    break;
                default:
                    var k = e.Key.ToString();
                    if (k == VisionLetter.Text)
                    {
                        LatterFonetSize.Background = Brushes.Green;
                        RandomLatter();
                    }
                    else
                    {
                        LatterFonetSize.Background = Brushes.Red;
                    }
                    break;

            }
        }
    }
}
