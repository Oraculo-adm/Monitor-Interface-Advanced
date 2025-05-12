using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MIA.Core.Interfaces;
using MIA.Core.Models;

namespace MIA.Core.UI
{
    public static class ModuleUIManager
    {
        public static Dictionary<string, ModuleMetadata> Modulos = new();

        public static void PopularMenus(Menu menuModulos, Menu menuTeste)
        {
            menuModulos.Items.Clear();
            menuTeste.Items.Clear();

            foreach (var modulo in Modulos.Values)
            {
                MenuItem item = new() { Header = modulo.Name };

                if (modulo.IsLib || modulo.Dependencies?.Exists(d => d.Required && !Modulos.ContainsKey(d.Name)) == true)
                {
                    item.IsEnabled = false;
                    item.Opacity = 0.5;
                }
                else
                {
                    item.IsCheckable = true;
                    item.IsChecked = modulo.Enabled;
                    item.Click += (_, _) => ToggleModulo(modulo);
                }

                menuModulos.Items.Add(item);

                if (!modulo.IsLib && modulo.Enabled)
                {
                    MenuItem testeItem = new() { Header = modulo.Name };
                    testeItem.Click += (_, _) => modulo.Launch();
                    menuTeste.Items.Add(testeItem);
                }
            }
        }

        private static void ToggleModulo(ModuleMetadata modulo)
        {
            modulo.Enabled = !modulo.Enabled;
            // Aqui você pode salvar o estado no disco e recarregar a interface
        }
    }
}
