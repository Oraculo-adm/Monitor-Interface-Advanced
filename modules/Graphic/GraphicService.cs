
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Graphic
{
    public class PingPoint
    {
        public string Interface { get; set; }
        public long TimeMs { get; set; }
    }

    public static class GraphicService
    {
        private static readonly Dictionary<string, List<long>> PingHistory = new();

        public static void AddPing(string iface, long timeMs)
        {
            if (!PingHistory.ContainsKey(iface))
                PingHistory[iface] = new List<long>();

            PingHistory[iface].Add(timeMs);
            if (PingHistory[iface].Count > 30)
                PingHistory[iface].RemoveAt(0);
        }

        public static void Draw(Canvas canvas)
        {
            canvas.Children.Clear();
            double width = canvas.ActualWidth;
            double height = canvas.ActualHeight;
            double stepX = width / 30;

            foreach (var iface in PingHistory.Keys)
            {
                var list = PingHistory[iface];
                for (int i = 1; i < list.Count; i++)
                {
                    var prev = list[i - 1];
                    var curr = list[i];

                    Line line = new()
                    {
                        X1 = (i - 1) * stepX,
                        Y1 = height - Math.Min(prev, 300),
                        X2 = i * stepX,
                        Y2 = height - Math.Min(curr, 300),
                        Stroke = GetColor(curr),
                        StrokeThickness = 2
                    };

                    canvas.Children.Add(line);
                }
            }
        }

        private static Brush GetColor(long ping)
        {
            if (ping == -1) return Brushes.Red;
            if (ping > 150) return Brushes.Orange;
            return Brushes.Lime;
        }
    }
}
