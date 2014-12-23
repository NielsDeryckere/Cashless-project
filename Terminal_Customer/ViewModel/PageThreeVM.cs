using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal_Customer.ViewModel
{
    class PageThreeVM:ObservableObject,IPage
    {
        public string Name
        {
            get { return "Terminal"; }
        }
    }
}
