using System.Windows;
using MIA.Core.Modules;

namespace MIA.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (_, _) =>
            {
                // Carregamento dinâmico de módulos com referência correta aos menus nomeados no XAML
                ModuleManager.LoadModules(MenuPrincipal, this, StatusLabel, MenuModulos, MenuTeste);
            };
        }

        private void Sobre_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Monitor Interface Advanced (MIA)\nVersão: 1.0.0\nDesenvolvido por Leone Martins com suporte da IA do ChatGPT.",
                "Sobre",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }
    }
}
