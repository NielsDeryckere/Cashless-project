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

        private Organisation _accountInfo;

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged("SelectedProduct"); }
        }
        
        public string Name
        {
            get { return "First page"; }
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

        public ICommand SignOutCommand
        {
            get { return new RelayCommand(SignOut); }
        }

        private void SignOut()
        {
              ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage( new LoginVM());
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
       

        


    }
}
