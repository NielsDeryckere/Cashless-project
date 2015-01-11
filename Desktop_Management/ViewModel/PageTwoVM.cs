using Aspose.BarCode;
using be.belgium.eid;
using Desktop_Management.Converter;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
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

namespace Desktop_Management.ViewModel
{
    class PageTwoVM : ObservableObject, IPage
    {
        #region Getters/setters
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

        private string _managementpass;

        public string ManagementPassword
        {
            get { return _managementpass; }
            set { _managementpass = value; }
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

        private string _error;

        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
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

        private string _mypassword;

        public string MyPassword
        {
            get { return _mypassword; }
            set { _mypassword = value; OnPropertyChanged("MyPassword"); }
        }
        private string _newpassword;

        public string NewPassword
        {
            get { return _newpassword; }
            set { _newpassword = value; OnPropertyChanged("NewPassword"); }
        }
        private string _repeatpassword;

        public string RepeatPassword
        {
            get { return _repeatpassword; }
            set { _repeatpassword = value; OnPropertyChanged("RepeatPassword"); }
        }

        private string _newPassError;

        public string NewPassError
        {
            get { return _newPassError; }
            set { _newPassError = value; OnPropertyChanged("NewPassError"); }
        }

        private string _passError;

        public string PasswordError
        {
            get { return _passError; }
            set { _passError = value; OnPropertyChanged("Could not change password"); }
        }
        

        private ObservableCollection<Sales> _sales;

        public ObservableCollection<Sales> SalesList
        {
            get { return _sales; }
            set { _sales = value; OnPropertyChanged("SalesList"); }
        }

        private ObservableCollection<RegisterClient> _registers;

        public ObservableCollection<RegisterClient> Registers
        {
            get { return _registers; }
            set { _registers = value; OnPropertyChanged("Registers"); }
        }

        private RegisterClient selected_register;

        public RegisterClient SelectedRegister
        {
            get { return selected_register; }
            set { selected_register = value; OnPropertyChanged("SelectedRegister"); }
        }

        private List<EmployeeRegister> _employeeRegisters;

        public List<EmployeeRegister> EmployeeRegisters
        {
            get { return _employeeRegisters; }
            set { _employeeRegisters = value; OnPropertyChanged("EmployeeRegisters"); }
        }
        

        private bool _teste;

        public bool TestE
        {
            get { return _teste; }
            set { _teste = value; OnPropertyChanged("TestE"); }
        }
        private bool _testReg;

        public bool TestReg
        {
            get { return _testReg; }
            set { _testReg = value; OnPropertyChanged("TestReg"); }
        }

        private bool _testProduct;
        
        public bool TestProduct
        {
            get { return _testProduct; }
            set { _testProduct = value; OnPropertyChanged("TestProduct"); }
        }

        private string _accountError;

        public string AccountError
        {
            get { return _accountError; }
            set { _accountError = value; OnPropertyChanged("AccountError"); }
        }
        
        
          private bool CanExecuteAddClient()
        {
            if (SelectedCustomer == null)
            {
                return false;
            }
            else
            {
                return SelectedCustomer.IsValid();
            }
        
        }
        #endregion



        public PageTwoVM()
        {
            if (ApplicationVM.token != null)
            {
            GetProducts();
            GetAccountInfo();
            GetCustomers();
            GetEmployees();
            GetSales();
            GetRegisters();
            test();
            testE();
            TestProductControl();
            
            }
        }
        #region GETS

        private async void GetRegisters()
        {
            
            Registers= new ObservableCollection<RegisterClient>();
            try
            {
                using (HttpClient client = new HttpClient())
                    {
                        client.SetBearerToken(ApplicationVM.token.AccessToken);
                        HttpResponseMessage response = await client.GetAsync("http://localhost:41983/api/Register/");
                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            Registers = JsonConvert.DeserializeObject<ObservableCollection<RegisterClient>>(json);

                        }
                     }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            } 
            
        }

        private async void GetSales()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:41983/api/Statistics");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                   SalesList = JsonConvert.DeserializeObject<ObservableCollection<Sales>>(json);
                  
                }
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
                    ManagementPassword =AccountInfo.Password;


                   
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
        #endregion

        #region TESTS
        public void test()
        {
            if (SelectedCustomer!=null)
            { Test=true;}

            else {Test=false; }
        }
        public void testE()
        {
            if (SelectedEmployee != null)
            { TestE = true; }

            else { TestE = false; }
        }

        public void TestRegister()
        {
            if(selected_register!=null)
            { TestReg = true; }
            else { TestReg = false; }
        }

        public void TestProductControl()
        {
            if(SelectedProduct!=null)
            { TestProduct = true; }
            else
            {
                TestProduct = false;

            }

        }
        #endregion
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

        public ICommand ChangePasswordCommand
        {
            get { return new RelayCommand(ChangePassword); } 
        }

        private async void ChangePassword()
        {
            if (CheckFilledInPasswords())
            {

                if (MyPassword == ManagementPassword)
                {
                    if (NewPassword == RepeatPassword)
                    {
                        object[] data = { AccountInfo.ID, NewPassword };
                        string input = JsonConvert.SerializeObject(data);
                        using (HttpClient client = new HttpClient())
                        {
                            //client.SetBearerToken(ApplicationVM.token.AccessToken);
                            HttpResponseMessage response = await client.PutAsync("http://localhost:41983/api/AccountInfo", new StringContent(input, Encoding.UTF8, "application/json"));
                            if (!response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Could not change password");
                            }
                            //MessageBox.Show("Password has been changed");
                        }


                    }

                    else
                    {
                        NewPassError = "The new password and the repeated new password are not the same";

                    }


                }
                else
                {
                    Error = "You're current password is not correct";
                }



            }
            else { MessageBox.Show("you have to fill in data"); }
        }

        private bool CheckFilledInPasswords()
        {
            if (NewPassword != null && RepeatPassword != null && MyPassword!=null && NewPassword.Length > 3 && RepeatPassword.Length > 3)
            { return true; }
            else{
                return false;
            }
        }
        #endregion
        #region productcommands en methods
       
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

        public ICommand ExportCommand
        { get { return new RelayCommand(ExportToExcel); } }

        private void ExportToExcel()
        {
            try
            {
                string s=string.Concat( DateTime.Now.ToString(),"StatisticsProducts.xlsx");
                SpreadsheetDocument doc = SpreadsheetDocument.Create(s, SpreadsheetDocumentType.Workbook);

                WorkbookPart wbp = doc.AddWorkbookPart();
                wbp.Workbook = new Workbook();

                WorksheetPart wsp = wbp.AddNewPart<WorksheetPart>();
                SheetData data = new SheetData();
                wsp.Worksheet = new Worksheet(data);

                Sheets sheets = doc.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet()
                {
                    Id = doc.WorkbookPart.GetIdOfPart(wsp),
                    SheetId = 1,
                    Name = "statistics",
                };
                sheets.Append(sheet);

                Row header = new Row() { RowIndex = 1 };
                Cell ID = new Cell() { CellReference = "A1", DataType = CellValues.String, CellValue = new CellValue("ID") };
                Cell Timestamp = new Cell() { CellReference = "B1", DataType = CellValues.String, CellValue = new CellValue("Timestamp") };
                Cell CustomerID = new Cell() { CellReference = "C1", DataType = CellValues.String, CellValue = new CellValue("CustomerID") };
                Cell RegisterID = new Cell() { CellReference = "D1", DataType = CellValues.String, CellValue = new CellValue("RegisterID") };
                Cell ProductID = new Cell() { CellReference = "E1", DataType = CellValues.String, CellValue = new CellValue("ProductID") };
                Cell Amount = new Cell() { CellReference = "F1", DataType = CellValues.String, CellValue = new CellValue("Amount") };
                Cell TotalPrice = new Cell() { CellReference = "G1", DataType = CellValues.String, CellValue = new CellValue("TotalPrice") };
                header.Append(ID, Timestamp, CustomerID, RegisterID, ProductID, Amount, TotalPrice);
                data.Append(header);
                for (int i = 2; i < SalesList.Count+2; i++)
                {
                    Sales saleitem = SalesList[i - 2];
                    Int32 rowindex = Convert.ToInt32(i);



                    Row sale = new Row() { RowIndex = Convert.ToUInt32(rowindex) };
                    Cell Value1 = new Cell() { CellReference = "A" + i, DataType = CellValues.Number, CellValue = new CellValue(saleitem.Id.ToString()) };
                    Cell Value2 = new Cell() { CellReference = "B" + i, DataType = CellValues.Date, CellValue = new CellValue(saleitem.Timestamp.ToString()) };
                    Cell Value3 = new Cell() { CellReference = "C" + i, DataType = CellValues.Number, CellValue = new CellValue(saleitem.ProductId.ToString()) };
                    Cell Value4 = new Cell() { CellReference = "D" + i, DataType = CellValues.Number, CellValue = new CellValue(saleitem.RegisterId.ToString()) };
                    Cell Value5 = new Cell() { CellReference = "E" + i, DataType = CellValues.Number, CellValue = new CellValue(saleitem.ProductId.ToString()) };
                    Cell Value6 = new Cell() { CellReference = "F" + i, DataType = CellValues.Number, CellValue = new CellValue(saleitem.Amount.ToString()) };
                    Cell Value7 = new Cell() { CellReference = "G" + i, DataType = CellValues.Number, CellValue = new CellValue(saleitem.TotalPrice.ToString()) };

                    sale.Append(Value1, Value2, Value3, Value4, Value5, Value6, Value7);
                    data.Append(sale);

                }


                wbp.Workbook.Save();
                doc.Close();
                MessageBox.Show("Export succeeded");
            }
            catch (Exception)
            {

                MessageBox.Show("Couldn't Export statistics");
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

        private Customer _selected=null;
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
            get { return new RelayCommand(SaveCustomer, CanExecuteAddClient); }
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
            try
            {string barcode="";
             Customer c = new Customer();
             if (SelectedCustomer.Barcode != 0)
             { c.Barcode = SelectedCustomer.Barcode; }

             
            

           
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
            catch (Exception)
            {

                MessageBox.Show("Could not read identitycard");
            }
            
        }

      
        private void NewCustomer()
        {
           
            Customer c = new Customer();

            SelectedCustomer = c;
            test();
           
        }

        private async void SaveCustomer()
        {

            try
            {
                string input = JsonConvert.SerializeObject(SelectedCustomer);

                // check insert (no ID assigned) or update (already an ID assigned)
                if (SelectedCustomer.Id == 0)
                {
                    bool etest = false;
                    if (Customers.Count > 0)
                    {
                        foreach (Customer e in Customers)
                        {
                            if (e.Barcode != SelectedEmployee.Barcode)
                            {
                                etest = true;
                            }
                            else { etest = false; }

                        }
                    }
                    else { etest = true; }
                    if (etest == true && SelectedCustomer.IsValid())
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            client.SetBearerToken(ApplicationVM.token.AccessToken);
                            HttpResponseMessage response = await client.PostAsync("http://localhost:41983/api/customer", new StringContent(input, Encoding.UTF8, "application/json"));
                            if (response.IsSuccessStatusCode)
                            {
                                string output = await response.Content.ReadAsStringAsync();
                                SelectedCustomer.Barcode = Int64.Parse(output);
                                GetCustomers();
                            }
                            else
                            {
                                MessageBox.Show("Could not save customer,");
                            }
                        }
                    }
                    else { MessageBox.Show("Could not save customer,make sure that all filled in values are correct"); }
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
                        MessageBox.Show("Customer Saved");
                        GetCustomers();
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("There is no customer or new customer selected");
            }
        }

        private async void DeleteCustomer()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync("http://localhost:41983/api/customer/" + SelectedCustomer.Barcode);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Could not save customer");
                }
                else
                {
                    MessageBox.Show("Customer Updated");

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
            if (SelectedEmployee.Barcode != 0)
            { c.Barcode = SelectedEmployee.Barcode; }





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
            testE();


        }

        private async void SaveEmployee()
        {
            string input = JsonConvert.SerializeObject(SelectedEmployee);
          
          
            if (SelectedEmployee.Id==0)
            {
                bool etest=false;
                if (Employees.Count > 0) { 
                foreach(Employee e in Employees)
                {
                    if(e.Barcode!=SelectedEmployee.Barcode){
                    etest=true;
                    }
                    else{etest=false;}
                
                }}
                else { etest = true; }
                if (etest == true)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.SetBearerToken(ApplicationVM.token.AccessToken);
                        HttpResponseMessage response = await client.PostAsync("http://localhost:41983/api/Employee", new StringContent(input, Encoding.UTF8, "application/json"));
                        if (response.IsSuccessStatusCode)
                        {
                            string output = await response.Content.ReadAsStringAsync();
                            SelectedEmployee.Barcode = Int64.Parse(output);
                            GetEmployees();
                        }
                        else
                        {
                            Console.WriteLine("error: could not save customer");

                        }
                    }
                }

                else { Console.WriteLine("There is already a employee with the same barcode"); }
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
                HttpResponseMessage response = await client.DeleteAsync("http://localhost:41983/api/Employee/" + SelectedEmployee.Barcode);
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

        public ICommand ViewRegisterUsersCommand
        {
            get { return new RelayCommand(ViewRegisterUsers); }
        }

        private async void ViewRegisterUsers()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:41983/api/EmployeeRegister/"+selected_register.RegisterID);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    EmployeeRegisters = JsonConvert.DeserializeObject<List<EmployeeRegister>>(json);
                    

                }
                else { MessageBox.Show("Could not get Employee_registers"); }
            }
            
        }

    }
}
