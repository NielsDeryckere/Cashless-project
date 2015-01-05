using GalaSoft.MvvmLight.CommandWpf;
using models;
using modelsProject;
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
            GetCustomers();
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

        private Product _clickedProduct;

        public Product ClickedProduct
        {
            get { return _clickedProduct; }
            set { _clickedProduct = value; OnPropertyChanged("ClickedProduct"); }
        }
        
        private ObservableCollection<Product> _orderedProducts=new ObservableCollection<Product>();

        public ObservableCollection<Product> OrderedProducts
        {
            get { return _orderedProducts; }
            set { _orderedProducts = value; OnPropertyChanged("OrderedProducts"); }
        }


        private double _totalPrice;

        public double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; OnPropertyChanged("TotalPrice"); }
        }

        private Customer _scannedCustomer;

        public Customer ScannedCustomer
        {
            get { return _scannedCustomer; }
            set { _scannedCustomer = value; OnPropertyChanged("ScannedCustomer"); }
        }

        private long _barcodeCustomer;

        public long BarcodeCustomer
        {
            get { return _barcodeCustomer; }
            set { _barcodeCustomer = value; OnPropertyChanged("BarcodeCustomer"); }
        }



        private bool _customerScanned;

        public bool CustomerScanned
        {
            get { return _customerScanned; }
            set { _customerScanned = value; OnPropertyChanged("CustomerScanned"); }
        }
        

        private Employee _currentEmployee;

        public Employee CurrentEmployee
        {
            get { return _currentEmployee; }
            set { _currentEmployee = value; OnPropertyChanged("CurrentEmployee"); }
        }

        private List<Customer> _customers;

        public List<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value; OnPropertyChanged("Customers"); }
        }

        private string _error;

        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
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

        public ICommand FinalizeSaleCommand
        {

            get { return new RelayCommand(FinalizeSale); }
        }

        private async void FinalizeSale()
        {
            
                if (TotalPrice <= ScannedCustomer.Balance)
                {
                    foreach (Product p in OrderedProducts)
                    {
                        Transfer t = new Transfer();
                        Sales s = new Sales();
                        s.Amount = 1;
                        s.CustomerId = ScannedCustomer.Barcode;
                        s.ProductId = p.Id;
                        s.RegisterId = 2;
                        s.Timestamp = DateTime.Now;
                        s.TotalPrice = p.Price;

                        t.Sender = ScannedCustomer;
                        t.Receiver = s;

                        try
                        {  string input = JsonConvert.SerializeObject(t);
                        using (HttpClient client = new HttpClient())
                        {
                            HttpResponseMessage response = await client.PutAsync("http://localhost:41983/api/ProductRegister/", new StringContent(input, Encoding.UTF8, "application/json"));
                            if (response.IsSuccessStatusCode)
                            {

                            }
                        }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            
                            throw;
                        }
                       
                      
                    } ScannedCustomer = null;
                    OrderedProducts.Clear();
                    Customers.Clear();
                    GetCustomers();
                    TotalPrice = 0;
                    }
                else
                    {
                        Error = "The customer his balance is not sufficient";
                        
                        
                    }

            }
           
        
           

        public ICommand AddProductCommand
        {
            get { return new RelayCommand <int>(AddProduct); }
        
        }

        private void AddProduct(int i)
        {
           
            
                ClickedProduct= (from p in Products where p.Id == i select p).First() ;
            try
            {
                
                OrderedProducts.Add(ClickedProduct);
             
                 TotalPrice += ClickedProduct.Price; 
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
           
                
            }

        }

        public ICommand ScanCustomerCommand
        {

            get { return new RelayCommand(ScanCustomer); }
        }

        private void ScanCustomer()
        {
            try
            {
                ScannedCustomer = (from p in Customers where p.Barcode == BarcodeCustomer select p).First();
                if (ScannedCustomer != null)
                { CustomerScanned = true; }

            }
            catch (Exception ex)
            {

                Error = "Could not scan customer, is the customer registered?";
            }
          
            
        }
      
        public ICommand LoggOffCommand
        { get { return new RelayCommand(LogOff); } }

        private async void LogOff()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            using (HttpClient client = new HttpClient())
            {
                EmployeeRegister er = new EmployeeRegister() {RegisterID= 2,EmployeeID= CurrentEmployee.Barcode,From= appvm.From,Untill=DateTime.Now };
                
                string input = JsonConvert.SerializeObject(er);
             
                HttpResponseMessage response = await client.PostAsync("http://localhost:41983/api/EmployeeRegister", new StringContent(input, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string output = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(Int32.Parse(output));
                 
                }
                else
                {
                    Console.WriteLine("error: could not save employeeregister");
                }
            }

            
            appvm.ChangePage(new LoginVM());

        }

        public ICommand RemoveProductCommand
        { get { return new RelayCommand<Product>(RemoveProduct); } }

        private void RemoveProduct(Product obj)
        {
            OrderedProducts.Remove(obj);
            TotalPrice -= obj.Price;
        }
      
    }
}
