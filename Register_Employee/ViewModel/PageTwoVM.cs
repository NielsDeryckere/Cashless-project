using GalaSoft.MvvmLight.CommandWpf;
using models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Register_Employee.ViewModel
{
    class PageTwoVM: ObservableObject,IPage
    {
        public PageTwoVM()
        {
            GetProducts();
            GetLogin();
        }

        private void GetLogin()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            CurrentEmployee = appvm.LoggedIn;
        }
        public string Name
        {
            get { return "Register"; }
        }

        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged("Products"); }
        }

        private List<Product> _orderedProducts;

        public List<Product> OrderedProducts
        {
            get { return _orderedProducts; }
            set { _orderedProducts = value; OnPropertyChanged("OrderedProducts"); }
        }
        

        private Employee _currentEmployee;

        public Employee CurrentEmployee
        {
            get { return _currentEmployee; }
            set { _currentEmployee = value; OnPropertyChanged("CurrentEmployee"); }
        }
        
        
        public async void GetProducts()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:41983/api/ProductRegister");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
                }
            }
            
            
        }

        public ICommand AddProductCommand
        {
            get { return new RelayCommand(AddProduct); }
        
        }

        private void AddProduct()
        {
            MessageBox.Show("Product added");
            Console.WriteLine("product added");
        }

      

      
    }
}
