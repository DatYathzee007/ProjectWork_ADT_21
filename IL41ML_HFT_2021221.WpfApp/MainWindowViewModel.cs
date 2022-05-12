using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace IL41ML_HFT_2021221.WpfApp
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RelayCommand ShowManager { get; set; }
        public RelayCommand ShowStock { get; set; }
        public RelayCommand ShowCustomer { get; set; }
        public MainWindowViewModel()
        {
            ShowManager = new RelayCommand(() => {
                _ = new ManagerWindow.ShowDialog();
                
            });
        }
    }
}
