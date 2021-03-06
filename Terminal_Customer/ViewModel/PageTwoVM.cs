﻿using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using models;
using be.belgium.eid;
using Aspose.BarCode.WPF;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;


namespace Terminal_Customer.ViewModel
{
    class PageTwoVM: ObservableObject, IPage
    {
        public PageTwoVM()
        {
            GetCustomers();
        }

        public string Name
        {
            get { return "Registration customer"; }
        }

        private Customer _newCustomer=new Customer();

        public Customer NewCustomer
        {
            get { return _newCustomer; }
            set { _newCustomer = value; OnPropertyChanged("NewCustomer"); }
        }

        private Customer _testcustomer;

        public Customer TestCustomer
        {
            get { return _testcustomer; }
            set { _testcustomer = value; }
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
            set { _error = value; }
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
       public ICommand ConfirmCommand
        {
            get { return new RelayCommand(Confirm); }

        }

       private async void Confirm()
       {
           try
           {        ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                   appvm.LoggedInCustomer = NewCustomer;
                   string input = JsonConvert.SerializeObject(NewCustomer);
               TestCustomer = (from p in Customers where p.Barcode == NewCustomer.Barcode select p).First();
               if (TestCustomer == null)
               {
                  
                   using (HttpClient client = new HttpClient())
                   {
                       
                       HttpResponseMessage response = await client.PostAsync("http://localhost:41983/api/CustomerTerminal", new StringContent(input, Encoding.UTF8, "application/json"));
                       if (response.IsSuccessStatusCode)
                       {
                           string output = await response.Content.ReadAsStringAsync();
                        
                          
                       }
                       else
                       {
                           MessageBox.Show("could not save customer");
                       }
                   }
                   appvm.ChangePage(new PageThreeVM());

               }
               else if(TestCustomer.Active==false)
               {
                   using (HttpClient client = new HttpClient())
                   {

                       HttpResponseMessage response = await client.PutAsync("http://localhost:41983/api/ExistingCustomer", new StringContent(input, Encoding.UTF8, "application/json"));
                       if (response.IsSuccessStatusCode)
                       {
                           string output = await response.Content.ReadAsStringAsync();


                       }
                       else
                       {
                           MessageBox.Show("could not save customer");
                       }
                   }
                   appvm.ChangePage(new PageThreeVM());


               }

               else
               {
                   MessageBox.Show("Failed to create new customer");
               }

           }
           catch (Exception ex)
           {

               Error = "Couldn't create new customer";
           }
       }

        public ICommand RegisterCommand
        {
            get { return new RelayCommand(register); }
        }

        private void register()
        {

            try
            {
                string barcode = "";
                Customer c = new Customer();
                if (NewCustomer.Barcode != 0)
                { c.Barcode = NewCustomer.Barcode; }





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
                            c.Picture = bytearray;


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
                bb.Save(c.CustomerName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                c.Barcode = Int64.Parse(barcode);
                NewCustomer = c;
            }
            catch (Exception)
            {

                MessageBox.Show("Please insert a valid identitycard and try again");
            }
        
 
        
        
        }

    }
}
