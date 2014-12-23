using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using models;
using be.belgium.eid;

namespace Terminal_Customer.ViewModel
{
    class PageTwoVM: ObservableObject, IPage
    {

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
        

        public ICommand RegisterCommand
        {
            get { return new RelayCommand(register); }
        }

        private void register()
        {
            Customer test = new Customer();
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
                 
                        
                       test.CustomerName = card.getID().getFirstName() + " " + card.getID().getSurname();
                       test.Address = card.getID().getStreet() + " " + card.getID().getMunicipality();

                    }
                }
            }
            BEID_ReaderSet.releaseSDK();
            NewCustomer = test;
 
        
        
        }

    }
}
