
using System.Windows;
using System.Windows.Threading;
using Graphic;

namespace PingMonitor
{
    public partial class PingGraphWindow : Window
    {
        private readonly DispatcherTimer timer;

        public PingGraphWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer
            {
                Interval = System.TimeSpan.FromSeconds(3)
            };
            timer.Tick += (s, e) => GraphicService.Draw(PingCanvas);
            timer.Start();

            this.Closed += (s, e) => timer.Stop();
        }
    }
}
