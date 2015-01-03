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

namespace Register_Employee.ViewModel
{
    class LoginVM : ObservableObject, IPage
    {
        public LoginVM()
        {
            GetEmployees();

        }
        public string Name
        {
            get { return "First page"; }
        }

        private string _barcode;

        public string Barcode
        {
            get { return _barcode; }
            set { _barcode = value; OnPropertyChanged("Barcode"); }

        }

        private ObservableCollection<Employee> _employeeLogins;

        public ObservableCollection<Employee> EmployeeLogins
        {
            get { return _employeeLogins; }
            set { _employeeLogins = value; OnPropertyChanged("EmployeeLogins"); }
        }

        private Employee _loggedInEmployee;

        public Employee LoggedInEmployee
        {
            get { return _loggedInEmployee; }
            set { _loggedInEmployee = value; OnPropertyChanged("LoggedInEmployee"); }
        }
        
        private bool _logintrue = false;
        
        private string _error;

        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        public ICommand CheckLoginCommand
        {
            get { return new RelayCommand(CheckLogins);}
        
        }
        public async void GetEmployees()
        { 
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:41983/api/LoginRegister");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    EmployeeLogins = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);
                }
            }
        
        }

        public void CheckLogins()
        {
           
            if (EmployeeLogins != null && _logintrue==false) { 

            foreach(Employee e in EmployeeLogins)
            {
                if(_logintrue==false)
            { 
                
                if (e.Barcode==Int64.Parse(Barcode))
                {
                    _logintrue = true;
                    LoggedInEmployee = e;
                    
                    
                }
                else {
                  
            
                }
                }


            }
                if(_logintrue==true)
                {
                    ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                    appvm.LoggedIn = LoggedInEmployee;
                    appvm.ChangePage(new PageTwoVM());
                }
                else { Error="You are not registered in the system";  }
            
            }


            else { Error = "There are no users in the system, please contact helpdesk"; }

            

        }

        
    }

  
}
