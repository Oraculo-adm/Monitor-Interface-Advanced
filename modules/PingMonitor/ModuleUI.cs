
using System.Windows.Controls;
using System.Windows;
using MIA.Core.UI;

namespace PingMonitor
{
    public static class ModuleUI
    {
        public static void Inject(MenuItem opcoesMenu)
        {
            var menuItem = new MenuItem { Header = "Exibir gráfico" };
            menuItem.Click += (s, e) =>
            {
                var win = new PingGraphWindow();
                win.Show();
            };
            opcoesMenu.Items.Add(menuItem);
        }
    }
}
