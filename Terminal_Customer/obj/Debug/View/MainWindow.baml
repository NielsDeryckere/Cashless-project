   M S B A M L   `   `   `  ����L  HTerminal_Customer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Terminal_Customer.ViewModelTerminal_Customer.ViewModel  5   	   ?$clr-namespace:Terminal_Customer.ViewTerminal_Customer.View  5   	   "    Terminal_Customer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   8vm)clr-namespace:Terminal_Customer.ViewModel     5   	   2v$clr-namespace:Terminal_Customer.View     5   	     � Title$  
MainWindow��5   	   $	��768��6   $
��1024��6)    � 
ResizeMode$ NoResize=�67    � DataContext 5      /  )Terminal_Customer.ViewModel.ApplicationVM  5   
    � 	Resources 5
       �� DataType7  1Terminal_Customer.ViewModel.LoginScreenCustomerVM-   'Terminal_Customer.View.TerminalCustomer+  %Terminal_Customer.ViewModel.PageTwoVM$   Terminal_Customer.View.PageTwo-  'Terminal_Customer.ViewModel.PageThreeVM&    Terminal_Customer.View.PageThree��%�   (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5      .��5      � .+�5   
   �� $��1��6   ��6&   �� *CurrentPage+5      5      