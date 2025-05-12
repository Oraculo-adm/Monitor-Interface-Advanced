namespace MIA.Core.Interfaces
{
    public interface IModule
    {
        void Initialize();
    }

    public interface IModuleUI
    {
        void InjectUI(System.Windows.Controls.Menu menu, System.Windows.Window window);
    }
}
