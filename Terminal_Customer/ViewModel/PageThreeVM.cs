using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace Terminal_Customer.ViewModel
{
    class PageThreeVM:ObservableObject,IPage
    {
        public PageThreeVM()
        {
            FillInInformation();
        }
        public string Name
        {
            get { return "Terminal"; }
        }
        private Customer _loggedInCustomer;

        public Customer LoggedInCustomer
        {
            get { return _loggedInCustomer; }
            set { _loggedInCustomer = value; }
        }
        

        public void FillInInformation()
        {   ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                   LoggedInCustomer =appvm.LoggedInCustomer;


        }

        public ICommand LogoutCommand
        { get { return new RelayCommand(Logout); } }

        private void Logout()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.LoggedInCustomer=new Customer();
            appvm.ChangePage(new LoginScreenCustomerVM());
        }

      
    }
}
