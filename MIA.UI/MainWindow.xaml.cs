
using System.Windows;
using System.Windows.Controls;
using MIA.Core.UI;

namespace MIA.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Inicializa o sistema de UI dinâmica
            UIManager.Initialize(MainMenu);

            // Cria menus principais
            UIManager.CreateBaseMenu("Temas");
            UIManager.CreateBaseMenu("Módulos");
            UIManager.CreateBaseMenu("Opções");

            var ajuda = UIManager.CreateBaseMenu("Ajuda");
            var sobre = new MenuItem { Header = "Sobre" };
            sobre.Click += (s, e) =>
            {
                MessageBox.Show(
                    "Monitor Interface Advanced (MIA)\nVersão: 1.0.0",
                    "Sobre",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            };
            ajuda.Items.Add(sobre);
        }
    }
}
