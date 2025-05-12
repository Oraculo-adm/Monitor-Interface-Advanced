using System.Windows;
using MIA.Core.Modules;

namespace MIA.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Carrega os módulos e injeta no Menu e Label
            ModuleManager.LoadModules(MenuPrincipal, this, StatusLabel, MenuModulos, MenuTeste);
        }

        private void Sobre_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Monitor Interface Advanced - MIA\nVersão 1.0.0\nDesenvolvido por Leone Martins com assistência da IA.", "Sobre", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
