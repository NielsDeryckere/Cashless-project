using Aspose.BarCode;
using be.belgium.eid;
using Desktop_Management.Converter;
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

namespace Desktop_Management.ViewModel
{
    class PageTwoVM : ObservableObject, IPage
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged("Products"); }
        }

        
        private string _accountName;

        public string AccountName
        {
            get { return _accountName; }
            set { _accountName = value; OnPropertyChanged("AccountName"); }
        }

        private string _companyName;

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; OnPropertyChanged("CompanyName"); }
        }

        private bool _test;

        public bool Test
        {
            get { return _test; }
            set { _test = value; OnPropertyChanged("Test"); }
        }
        
        private Organisation _accountInfo;

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged("SelectedProduct"); }
        }
        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value; OnPropertyChanged("Customers"); }
        }
        public string Name
        {
            get { return "First page"; }
        }

        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged("Employees"); }
        }
        
        public Organisation AccountInfo
        {
            get { return _accountInfo; }
            set { _accountInfo = value; }
        }
        public PageTwoVM()
        {
            if (ApplicationVM.token != null)
            {
            GetProducts();
            GetAccountInfo();
            GetCustomers();
            GetEmployees();
            test();
            }
        }

        private async void GetAccountInfo()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:41983/api/AccountInfo");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    AccountInfo = JsonConvert.DeserializeObject<Organisation>(json);
                    AccountName = AccountInfo.Login;
                    CompanyName = AccountInfo.OrganisationName;
                }
            }
        }

        public void test()
        {
            if (SelectedCustomer!=null)
            { Test=true;}

            else {Test=false; }
        }


        #region accountinfo method and command

        public ICommand SignOutCommand
        {
            get { return new RelayCommand(SignOut); }
        }

        private void SignOut()
        {
              ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage( new LoginVM());
        }

        #endregion
        #region productcommands en methods
        private async void GetProducts()
        {

            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:41983/api/Product");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
                }
            }

        }  
        public ICommand NewProductCommand
        {
            get { return new RelayCommand(NewProduct); }
        }

        private void NewProduct()
        {
            Product p = new Product();
           
            SelectedProduct = p;
        }

        public ICommand SaveProductCommand
        {
            get { return new RelayCommand(SaveProduct); }
        }

        private async void SaveProduct()
        {
            string input = JsonConvert.SerializeObject(SelectedProduct);

           
            if (SelectedProduct.Id == 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.PostAsync("http://localhost:41983/api/product", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        string output = await response.Content.ReadAsStringAsync();
                        SelectedProduct.Id = Int32.Parse(output);
                        GetProducts();
                    }
                    else
                    {
                        Console.WriteLine("error");
                    }
                }
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.PutAsync("http://localhost:41983/api/product", new StringContent(input, Encoding.UTF8, "application/json"));
                  
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("error");
                    }  
                    //GetProducts();
                }
            }

        }

        public ICommand DeleteProductCommand
        {
            get { return new RelayCommand(DeleteProduct); }
        }

        private async void DeleteProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync("http://localhost:41983/api/product/" + SelectedProduct.Id);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("error");
                }
                else
                {
                    GetProducts();
                }
            }
        }
        #endregion

        #region Customer commands and methods
        private async void GetCustomers()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:41983/api/Customer");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(json);
                }
            }
        }

        private Customer _selected;
        public Customer SelectedCustomer
        {
            get { return _selected; }
            set { _selected = value; OnPropertyChanged("SelectedCustomer"); }
        }

        public ICommand NewCustomerCommand
        {
            get { return new RelayCommand(NewCustomer); }
        }

        public ICommand SaveCustomerCommand
        {
            get {  return new RelayCommand(SaveCustomer); }
        }

        public ICommand DeleteCustomerCommand
        {
            get { return new RelayCommand(DeleteCustomer); }
        }

        public ICommand ReadIdentityCardCommand
        {
            get { return new RelayCommand(ReadIdentity); }
        
        }

        private void ReadIdentity()
        {
            string barcode="";
             Customer c = new Customer();
             if (SelectedCustomer.Id != 0)
             { c.Id = SelectedCustomer.Id; }

             
            

           
            BEID_ReaderSet.initSDK();
            // access the eID card here
            if (BEID_ReaderSet.instance().readerCount() > 0)
            {
                BEID_ReaderContext readerContext = readerContext = BEID_ReaderSet.instance().getReader();
                if (readerContext != null)
                {
                    if (readerContext.getCardType() == BEID_CardType.BEID_CARDTYPE_EID)
                    {
                        BEID_EIDCard card = readerContext.getEIDCard();
                      
                        BEID_Picture picture;

                        byte[] bytearray;
                        picture = card.getPicture();
                        bytearray = picture.getData().GetBytes();
                        c.Picture =bytearray;
                       
                        
                        //c.Picture = StringToImageConverter.BitmapImageFromBytes(bytearray);
                        //img.Height = 100;

                       c.CustomerName = card.getID().getFirstName() + " " + card.getID().getSurname();
                       c.Address = card.getID().getStreet() + " " + card.getID().getMunicipality();
                       barcode = card.getID().getNationalNumber();

                    }
                } 
            }

            BEID_ReaderSet.releaseSDK();

            BarCodeBuilder bb = new BarCodeBuilder();
            bb.CodeText = barcode;
            bb.SymbologyType = Symbology.Code128;
            bb.Save(c.CustomerName+".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            c.Barcode = Int64.Parse(barcode);
            SelectedCustomer = c;
        }

      
        private void NewCustomer()
        {
           
            Customer c = new Customer();

            SelectedCustomer = c;
            test();
           
        }

        private async void SaveCustomer()
        {
           
            string input = JsonConvert.SerializeObject(SelectedCustomer);

            // check insert (no ID assigned) or update (already an ID assigned)
            if (SelectedCustomer.Id == 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.PostAsync("http://localhost:41983/api/customer", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        string output = await response.Content.ReadAsStringAsync();
                        SelectedCustomer.Id = Int32.Parse(output);
                        GetCustomers();
                    }
                    else
                    {
                        MessageBox.Show("Gebruiker bestaat al");
                    }
                }
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.PutAsync("http://localhost:41983/api/customer", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("error");
                    }
                    GetCustomers();
                }
            }
        }

        private async void DeleteCustomer()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync("http://localhost:41983/api/customer/" + SelectedCustomer.Id);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("error");
                }
                else
                {
                    GetCustomers();
                }
            }
        }

        #endregion

        #region Methods and commands Employee
        private async void GetEmployees()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:41983/api/Employee");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);
                }
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged("SelectedEmployee"); }
        }

        public ICommand NewEmployeeCommand
        {
            get { return new RelayCommand(NewEmployee); }
        }

        public ICommand SaveEmployeeCommand
        {
            get { return new RelayCommand(SaveEmployee); }
        }

        public ICommand DeleteEmployeeCommand
        {
            get { return new RelayCommand(DeleteEmployee); }
        }

        public ICommand ReadIdentityCardEmployeeCommand
        {
            get { return new RelayCommand(ReadIdentityEmployee); }

        }

        private void ReadIdentityEmployee()
        {
           
            Employee c = new Employee();
            if (SelectedEmployee.Id != 0)
            { c.Id = SelectedEmployee.Id; }





            BEID_ReaderSet.initSDK();
            // access the eID card here
            if (BEID_ReaderSet.instance().readerCount() > 0)
            {
                BEID_ReaderContext readerContext = readerContext = BEID_ReaderSet.instance().getReader();
                if (readerContext != null)
                {
                    if (readerContext.getCardType() == BEID_CardType.BEID_CARDTYPE_EID)
                    {
                        BEID_EIDCard card = readerContext.getEIDCard();

                        //BEID_Picture picture;

                        //byte[] bytearray;
                        //picture = card.getPicture();
                        //bytearray = picture.getData().GetBytes();
                        //c.Picture = bytearray;


                        //c.Picture = StringToImageConverter.BitmapImageFromBytes(bytearray);
                        //img.Height = 100;

                        c.EmployeeName = card.getID().getFirstName() + " " + card.getID().getSurname();
                        c.Address = card.getID().getStreet() + " " + card.getID().getMunicipality();
                       c.Barcode =Int64.Parse(card.getID().getNationalNumber());

                    }
                }
            }

            BEID_ReaderSet.releaseSDK();

            BarCodeBuilder bb = new BarCodeBuilder();
            bb.CodeText = c.Barcode.ToString();
            bb.SymbologyType = Symbology.Code128;
            bb.Save(c.EmployeeName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
           
            SelectedEmployee = c;
        }


        private void NewEmployee()
        {
            Employee c = new Employee();

            SelectedEmployee = c;


        }

        private async void SaveEmployee()
        {
            string input = JsonConvert.SerializeObject(SelectedEmployee);

            // check insert (no ID assigned) or update (already an ID assigned)
            if (SelectedEmployee.Id == 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.PostAsync("http://localhost:41983/api/Employee", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        string output = await response.Content.ReadAsStringAsync();
                        SelectedEmployee.Id = Int32.Parse(output);
                        GetEmployees();
                    }
                    else
                    {
                        Console.WriteLine("error: could not save customer");
                    }
                }
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.PutAsync("http://localhost:41983/api/Employee", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("error");
                    }
                    GetEmployees();
                }
            }
        }

        private async void DeleteEmployee()
        {
            Employee c = SelectedEmployee;
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync("http://localhost:41983/api/Employee/" + SelectedEmployee.Id);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("error");
                }
                else
                {
                    GetEmployees();
                }
            }
        }


        #endregion

    }
}
