using BotCsgo.Service;
using BotCsgo.ViewModel;
using System.Windows;

namespace BotCsgo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowModel(this, new PageService());
        }
    }
}
