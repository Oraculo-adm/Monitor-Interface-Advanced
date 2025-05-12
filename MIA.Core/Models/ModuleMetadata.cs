using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MIA.Core.Models
{
    public class ModuleMetadata
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
        public bool IsLib { get; set; }
        public bool Enabled { get; set; } = false;
        public string EntryClass { get; set; }
        public List<ModuleDependency> Dependencies { get; set; } = new();

        public Action? Launch { get; set; } // Delegate para ativar o módulo não-lib
    }

    public class ModuleDependency
    {
        public string Name { get; set; }
        public bool Required { get; set; }
    }
}
