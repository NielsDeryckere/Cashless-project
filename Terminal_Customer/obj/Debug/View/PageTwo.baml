   M S B A M L   `   `   `  ����L  HTerminal_Customer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Terminal_Customer.ViewModelTerminal_Customer.ViewModel  5      ?$clr-namespace:Terminal_Customer.ViewTerminal_Customer.View  5      $    Terminal_Customer.View.PageTwo   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8vm)clr-namespace:Terminal_Customer.ViewModel     5      2v$clr-namespace:Terminal_Customer.View     5        � DataContext  5
      +  %Terminal_Customer.ViewModel.PageTwoVM  5   
   .��5      �� .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5"   
   �� 5#      $
��0.5* �6   �� 5$      $��* �6   �� 5%      $
��0.5* �6   �� 5&      $��* �6   .+�5*   
   �� -   5        $��txbRegistration��5*      $��New registration��6+   $��36(�6C   $��2��6Q   $��
10,10,30,0q�6f   �� 5+   
   -   5         ��Name$ txbEid��5+      $��15(�6$   $��1��62   $��3��6?   $��0q�6O   �� 6[   $#�Please put your identitycard ��6_   �� 6�   �� 6�   $*�$in the cardreader and press continue��6�   6�   �� 5,   
   $��First Name: ��6   $��2��6'   $��0��64   $��
10,11,30,9q�6D   �� 5-   
   $��Adress��6   $��3��6!   $��0��6.   $��10,10,30,10q�6>   �� 50   
   $��2��6>   $��1��6K   $��20, 10q�6[   ��6   �� *NewCustomer.CustomerName+6[   �� 51   
   $��3��69   $��1��6F   $��
20,13,20,7q�6V   ��6   �� *NewCustomer.Address+6V   �� 52   
   $��5��6   $��3��6"   $��20, 10q�62   �� 53   
   -   5        $��imgFoto��53      $��2��6   $
��Left=�6/   $	��100��6J   $��	28,11,0,0q�6W   $��2��6j   $��2��6w   $	��Top=�6�   $	��100��6�    J� RenderTransformOrigin$ 0.523,0.451&�6�   ��6�   �� *NewCustomer.Picture+�� 54   
   $��Confirm��6   $��1��6#   $��10,10,179,10q�63   $��5��6I   ��6V   �� *ConfirmCommand+�� 55   
   $��Cancel��6   $��1��6"   $��175,10,10,10q�62   $��5��6H   $�� ��6U   �� 57   
   $��Continue��6   $��2��6H   $��0q�6X   $��1��6c   ��6$   �� *RegisterCommand+6c   59      6   5:      