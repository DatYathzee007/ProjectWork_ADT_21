//using Microsoft.Toolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
//using Microsoft.Toolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Input;
using IL41ML_HFT_2021221.WpfApp.Manager;
using IL41ML_HFT_2021221.WpfApp.Stock;
using System.Threading;
using System.Windows.Input;

namespace IL41ML_HFT_2021221.WpfApp
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        //commands getter & setter
        public ICommand ShowManager { get; set; }
        public ICommand ShowStock { get; set; }
        public ICommand ShowCustomer { get; set; }

        public MainWindowViewModel()
        {
            Thread.Sleep(5000);
            // COMMANDS
            ShowCustomer = new RelayCommand(() => new CustomerWindow().ShowDialog());

            ShowManager = new RelayCommand(() =>
            {
                ManagerWindow managerWindow = new ManagerWindow();
                managerWindow.ShowDialog();
            }
            );

            ShowStock = new RelayCommand(() =>
            {
                StockWindow stockWindow = new StockWindow();
                stockWindow.ShowDialog();
            }
            );
        }
    }
}