using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Terminal_Customer.ViewModel
{
    class LoginScreenCustomerVM : ObservableObject, IPage
    {
        public LoginScreenCustomerVM()
        {
            GetCustomers();
        }
        public string Name
        {
            get { return "Login customer"; }
        }

        private long _barcode;

        public long Barcode
        {
            get { return _barcode; }
            set { _barcode = value; OnPropertyChanged("Barcode"); }
        }

        private List<Customer> _customers;

        public List<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value; OnPropertyChanged("Customers"); }
        }

        private Customer _loggedInCustomer;

        public Customer LoggedInCustomer
        {
            get { return _loggedInCustomer; }
            set { _loggedInCustomer = value; OnPropertyChanged("LoggedInCustomer"); }
        }
        

        public async void GetCustomers()
        {
            using (HttpClient client = new HttpClient())
            {

                var url = "http://localhost:41983/api/CustomerRegister";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<List<Customer>>(json);

                }
            }
        }
        
        public ICommand LoginCommand
        { get { return new RelayCommand(Login); } }

        private void Login()
        {
            try
            {
                LoggedInCustomer = (from p in Customers where p.Barcode == Barcode select p).First();
                if (LoggedInCustomer != null)
                {
                    ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                    appvm.LoggedInCustomer = LoggedInCustomer;
                    appvm.ChangePage(new PageThreeVM());

                }

            }
            catch (Exception ex)
            {

               Console.WriteLine( "Could not scan customer, is the customer registered?");
            }
           

        }

        public ICommand RegisterCommand
        {
            get { return new RelayCommand(RegisterCustomer); }
        }

        private void RegisterCustomer()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new PageTwoVM());
        }

        
    }
}
