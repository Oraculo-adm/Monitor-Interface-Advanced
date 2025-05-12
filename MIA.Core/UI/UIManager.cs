
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MIA.Core.UI
{
    public static class UIManager
    {
        public static Menu MainMenu { get; private set; }

        public static void Initialize(Menu menu)
        {
            MainMenu = menu;
        }

        public static void AddMenuItem(string header, MenuItem item)
        {
            foreach (var menuItem in MainMenu.Items)
            {
                if (menuItem is MenuItem m && m.Header.ToString() == header)
                {
                    m.Items.Add(item);
                    return;
                }
            }
        }

        public static MenuItem CreateBaseMenu(string header)
        {
            var menuItem = new MenuItem { Header = header };
            MainMenu.Items.Add(menuItem);
            return menuItem;
        }
    }
}
