using BotCsgo.Service;
using BotCsgo.ViewModel;
using System.Windows.Controls;

namespace BotCsgo
{
   public class BasePage<VM> : Page where VM : BaseViewModel, new()
    {
        private VM viewmodel;
        public VM ViewModel
        {
            get { return viewmodel; }
            set
            {
                viewmodel = value;
                this.DataContext = viewmodel;
            }
        }
        public BasePage()
        {
            this.ViewModel = new VM();
        }
    }
}
