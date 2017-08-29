using System;
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

        private StructuredLog _structuredLog = StructuredLog.Load();

        private DateTime _dateId = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();
            RandomLatter();

            //for (int i = 0; i < 100000; i++)
            //{
            //    _structuredLog.Entries.Add(CreateStructuredLogEntry(false));
            //}
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
                        _structuredLog.Entries.Add(CreateStructuredLogEntry(true));

                        SucessTxt.Text = (int.Parse(SucessTxt.Text) + 1).ToString();

                        RandomLatter();
                    }
                    else
                    {
                        ContolPanel.Background = Brushes.Red;
                        _log.Info($"failed: {k} real:{VisionLetter.Text}, fontsize: {VisionLetter.FontSize}");
                        _structuredLog.Entries.Add(CreateStructuredLogEntry(false));

                        FaildTxt.Text = (int.Parse(FaildTxt.Text) + 1).ToString();
                    }
                    break;

            }
        }

        private StructuredLogEntry CreateStructuredLogEntry(bool isOk)
        {
            var le =new StructuredLogEntry() {Time = DateTime.Now, IsOk = isOk, FontSize = VisionLetter.FontSize, DateId = _dateId};

            if (RdEays.IsChecked == true) le.EysExt = RdEays.Content.ToString();
            if (RdLenses65.IsChecked == true) le.EysExt = RdLenses65.Content.ToString();
            if (RdLenses70.IsChecked == true) le.EysExt = RdLenses70.Content.ToString();
            if (RdGlasses65.IsChecked == true) le.EysExt = RdGlasses65.Content.ToString();
            if (RdGlasses70.IsChecked == true) le.EysExt = RdGlasses70.Content.ToString();

            le.PrimaryScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            le.PrimaryScreennWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            le.MachineName = Environment.MachineName;
            le.UserName = Environment.UserName;

            return le;
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
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            _structuredLog.Save();
        }
    }
}
