using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using MIA.Core.Interfaces;
using MIA.Core.Models;
using MIA.Core.UI;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MIA.Core.Modules
{
    public static class ModuleManager
    {
        public static string StatusMessage { get; private set; } = "";

        public static void LoadModules(Menu menuPrincipal, Window janelaPrincipal, Label statusLabel, Menu menuModulos, Menu menuTeste)
        {
            string modulesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "modules");
            int carregados = 0, corrompidos = 0, incompatíveis = 0;
            bool temModulos = false;

            ModuleUIManager.Modulos.Clear();

            if (!Directory.Exists(modulesPath))
            {
                Directory.CreateDirectory(modulesPath);
                StatusMessage = "Nenhum Modulo Reconhecido";
                statusLabel.Content = StatusMessage;
                return;
            }

            string[] moduleFolders = Directory.GetDirectories(modulesPath);

            foreach (string moduleFolder in moduleFolders)
            {
                string jsonPath = Directory.GetFiles(moduleFolder, "*.json").FirstOrDefault();
                if (jsonPath == null)
                {
                    corrompidos++;
                    continue;
                }

                temModulos = true;

                try
                {
                    string json = File.ReadAllText(jsonPath);
                    dynamic metadata = JsonConvert.DeserializeObject(json);

                    if (metadata == null || metadata.entry == null || metadata.version == null || metadata.name == null)
                    {
                        corrompidos++;
                        continue;
                    }

                    string entry = metadata.entry;
                    string[] parts = entry.Split('.');
                    if (parts.Length != 2)
                    {
                        corrompidos++;
                        continue;
                    }

                    string modulo = parts[0];
                    string classe = parts[1];

                    string dllPath = Path.Combine(moduleFolder, $"{modulo}.dll");
                    if (!File.Exists(dllPath))
                    {
                        incompatíveis++;
                        continue;
                    }

                    Assembly assembly = Assembly.LoadFrom(dllPath);
                    Type type = assembly.GetType($"{modulo}.{classe}");
                    if (type == null)
                    {
                        incompatíveis++;
                        continue;
                    }

                    object instance = Activator.CreateInstance(type);
                    var isLib = metadata.isLib != null && metadata.isLib == true;

                    var modMeta = new ModuleMetadata
                    {
                        Name = metadata.name,
                        Version = metadata.version,
                        Author = metadata.author ?? "Desconhecido",
                        IsLib = isLib,
                        Enabled = true,
                        EntryClass = entry,
                        Launch = instance is IModule m ? new Action(() => m.Initialize()) : null
                    };

                    if (metadata.dependencies != null)
                    {
                        foreach (var dep in metadata.dependencies)
                        {
                            modMeta.Dependencies.Add(new ModuleDependency
                            {
                                Name = dep.name,
                                Required = dep.required == null || dep.required == true
                            });
                        }
                    }

                    ModuleUIManager.Modulos[modMeta.Name] = modMeta;

                    if (instance is IModule moduleInstance)
                    {
                        moduleInstance.Initialize();
                        carregados++;
                    }

                    if (instance is IModuleUI ui)
                    {
                        ui.InjectUI(menuPrincipal, janelaPrincipal);
                    }
                }
                catch
                {
                    corrompidos++;
                }
            }

            if (!temModulos)
                StatusMessage = "Nenhum Modulo Reconhecido";
            else if (carregados == 0 && corrompidos == 0 && incompatíveis == 0)
                StatusMessage = "Nenhum Modulo Carregado";
            else if (carregados > 0)
                StatusMessage = $"{carregados} Módulo(s) Carregado(s)";
            else if (incompatíveis > 0)
                StatusMessage = $"{incompatíveis} Módulo(s) Incompatíveis";
            else if (corrompidos > 0)
                StatusMessage = $"{corrompidos} Módulo(s) Corrompidos/Desconhecidos";

            statusLabel.Content = StatusMessage;
            ModuleUIManager.PopularMenus(menuModulos, menuTeste);
        }
    }
}
