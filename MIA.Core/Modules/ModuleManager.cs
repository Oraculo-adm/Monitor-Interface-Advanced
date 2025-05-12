
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

namespace MIA.Core.Modules
{
    public class ModuleInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public bool IsCore { get; set; }
        public List<string> Dependencies { get; set; }
        public string EntryPoint { get; set; }
        public string Repository { get; set; }
        public JObject UI { get; set; }
    }

    public static class ModuleManager
    {
        public static List<ModuleInfo> Modules { get; private set; } = new();
        public static string ModulesPath => Path.Combine(Directory.GetCurrentDirectory(), "modules");

        public static void LoadModules(MenuItem menuModulos, MenuItem menuOpcoes)
        {
            if (!Directory.Exists(ModulesPath))
                Directory.CreateDirectory(ModulesPath);

            Modules.Clear();
            menuModulos.Items.Clear();

            foreach (var dir in Directory.GetDirectories(ModulesPath))
            {
                var jsonPath = Path.Combine(dir, "module.json");
                if (!File.Exists(jsonPath)) continue;

                try
                {
                    var json = JObject.Parse(File.ReadAllText(jsonPath));
                    var module = new ModuleInfo
                    {
                        Name = json["name"]?.ToString() ?? "Desconhecido",
                        Version = json["version"]?.ToString(),
                        IsCore = json["core"]?.ToObject<bool>() ?? false,
                        EntryPoint = json["entryPoint"]?.ToString(),
                        Repository = json["repository"]?.ToString(),
                        UI = json["ui"] as JObject,
                        Dependencies = json["dependencies"]?.ToObject<List<string>>() ?? new List<string>()
                    };

                    Modules.Add(module);

                    var menuItem = new MenuItem
                    {
                        Header = module.Name + (module.IsCore ? " (core)" : ""),
                        IsEnabled = !module.IsCore,
                        ToolTip = $"Versão: {module.Version}"
                    };

                    menuModulos.Items.Add(menuItem);

                    if (module.UI != null && module.UI.ContainsKey("submenu"))
                    {
                        var submenu = new MenuItem { Header = module.UI["submenu"]?.ToString() };
                        menuOpcoes.Items.Add(submenu);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao carregar módulo: {ex.Message}");
                }
            }

            var btnAbrir = new MenuItem { Header = "Adicionar módulo..." };
            btnAbrir.Click += (s, e) => System.Diagnostics.Process.Start("explorer", ModulesPath);
            menuModulos.Items.Add(new Separator());
            menuModulos.Items.Add(btnAbrir);
        }
    }
}
