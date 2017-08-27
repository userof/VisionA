using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using NLog;

namespace VisionA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

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
            var str = "WERTYUIOPASDFHJKLZXCVBNM";
            Random rnd = new Random();
            var r = rnd.Next(0, str.Length - 1);
            VisionLetter.Text = str[r].ToString();
            LatterFonetSize.Text = VisionLetter.FontSize.ToString();
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
                case Key.OemPlus:
                    PlusFont();
                    break;
                case Key.OemMinus:
                    MinusFont();
                    break;
                case Key.Space:
                    RandomLatter();
                    break;
                case Key.D0:
                    TimeFlashLetter();
                    break;
                default:
                    var k = e.Key.ToString();
                    if (k == VisionLetter.Text)
                    {
                        ContolPanel.Background = Brushes.Green;
                        _log.Info($"ok: {k}, fontsize: {VisionLetter.FontSize}");

                        SucessTxt.Text = (int.Parse(SucessTxt.Text) + 1).ToString();

                        RandomLatter();
                    }
                    else
                    {
                        ContolPanel.Background = Brushes.Red;
                        _log.Info($"failed: {k} real:{VisionLetter.Text}, fontsize: {VisionLetter.FontSize}");

                        FaildTxt.Text = (int.Parse(FaildTxt.Text) + 1).ToString();
                    }
                    break;

            }
        }

        private void TimeFlashLetter()
        {
            var oldFs = VisionLetter.FontSize;
            VisionLetter.FontSize = 500;


            Task task = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
            });

            Task UITask = task.ContinueWith((x) =>
            {
                VisionLetter.FontSize = oldFs;
                //this.TextBlock1.Text = "Complete";
            }, TaskScheduler.FromCurrentSynchronizationContext());

            // Task.Run(() =>
            //{
            //    Thread.Sleep(3000);
            //    VisionLetter.FontSize = oldFs;
            //});
        }
    }
}
