   M S B A M L   `   `   `  ����?  ;Baza, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:BazaBaza  5   	       Baza.Rejestracja   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Baza     5   	     � Title$  Rejestracja��5   	   $	��450��6   $	��800��6*   .��5	      � .+�5
   
   �� $
��Left=�6   $	��290��6/   $��	10,10,0,0q�6<   
���   �$	��Top=�6^   $	��334��6v   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $	��290��6T   $��
382,10,0,0q�6a   $	��Top=�6u   $	��351��6�   �� 5   
   -   5         ��Name$ lblModel��5      $+��%Wybierz dostępny w tym czasie model:��6"   $
��Left=�6Q   $��
27,119,0,0q�6l   $	��Top=�6�   $	��234��6�   �� 5   
   -   5        $ lblOD��5      $��Od kiedy wypożyczenie��6   $
��Left=�6?   $��	29,20,0,0q�6Z   $	��Top=�6m   �� 5   
   -   5        $ lblDO��5      $��Do kiedy wypożyczenie��6   $
��Left=�6?   $��	29,57,0,0q�6Z   $	��Top=�6m   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ timeWypozyczenie_OD��5      $
��Left=�62   $��
195,21,0,0q�6M   $	��Top=�6a    J� RenderTransformOrigin$ 0.502,0.585&�6y     5   
   -   5        $ timeWypozyczenie_DO��5      $
��Left=�62   $��
195,58,0,0q�6M   $	��Top=�6a   �� 5   
   -   5         ��Name$ lstboxModele��5      $��27,152,501,180q�6S   �� 5   
   -   5        $ lblCENA��5      $'��!Całkowita kwota za wypożyczenie��6!   $
��Left=�6K   $��
27,274,0,0q�6f   $	��Top=�6z   �� 5   
   -   5         ��Name$ txtCenaZaWynajem��5      $
��Left=�6.   $��249,279,0,0q�6I   $
��Wrap=�6^   $
��0zł��6r   $	��Top=�6}   �� 5   
   -	   5        $ lblDanePlatnika��5      $!��Dane osoby wypożyczającej��6)   $
��Left=�6M   $��
400,20,0,0q�6h   $	��Top=�6|   G� 5   
   -
   5         G�Name$
 pswd��5      $
��Left=�6$   $��546,230,0,0q�6?   $	��Top=�6T   $	��160��6l   $��30��6x   �� 5   
   -   5        $ lblHasloDoPlatfromy��5      $ ��Twoje hasło do platformy:��6-   $
��Left=�6Q   $��394,228,0,0q�6l   $	��Top=�6�   �� 5   
   -   5         ��Name$ btnFINALRezerwacja��5      $��Rezerwuj��6-   $
��Left=�6@   $��
10,356,0,0q�6[   $	��Top=�6o   6�   �� 5   
   -   5         ��Name$ checkboxOswiadczenie1��5      $[��UOświadczam że zapoznałem/am się z treścią Regulaminu firmy sp. z o. o. RentCar ��62   $
��Left=�6�   $��
10,316,0,0q�6�   $	��Top=�6�   �� 5   
   -   5        $ checkboxOswiadczenie2��5      $X��RAkceptuję i zobowiązuję się do przestrzegania zapisów zawartych w Regulaminie��62   $
��Left=�6�   $��
10,336,0,0q�6�   $	��Top=�6�   �� 5   
   -   5        	 ��Name$	 txtNrPrawaJazdy��5      $
��Left=�6+   $��546,160,0,0q�6F   
 �� TextWrapping$

 Wrap=�6[   $	��Top=�6o   $	��160��6�   $��30��6�   �� 5   
   -   5        $	 txtNrDowoduOs��5      $
��Left=�6)   $��546,125,0,0q�6D   $

 Wrap=�6Y   $	��Top=�6m   $	��160��6�   $��30��6�   �� 5   
   -   5        $	 txtNazwisko��5      $
��Left=�6'   $��
546,90,0,0q�6B   $

 Wrap=�6V   $	��Top=�6j   $	��160��6�   $��30��6�   �� 5   
   -   5        $	 txtImie��5      $
��Left=�6#   $��
546,55,0,0q�6>   $

 Wrap=�6R   $	��Top=�6f   $	��160��6~   $��30��6�   �� 5   
   -   5        $	 
txtTelefon��5      $
��Left=�6&   $��546,195,0,0q�6A   $

 Wrap=�6V   $	��Top=�6j   $	��160��6�   $��30��6�   �� 5    
   -   5        $ lblTEL��5       $��Telefon kontaktowy +48:��6    $
��Left=�6B   $��394,195,0,0q�6]   $	��Top=�6r   �� 5!   
   -   5        $ 	lblPrawko��5!      $��Nr prawa jazdy:��6#   $
��Left=�6=   $��394,162,0,0q�6X   $	��Top=�6m   �� 5"   
   -   5        $ lblDowodOsobisty��5"      $��Nr dowodu osobistego:��6*   $
��Left=�6J   $��394,127,0,0q�6e   $	��Top=�6z   �� 5#   
   -   5        $ lblNazwisko��5#      $��	Nazwisko:��6%   $
��Left=�69   $��
394,92,0,0q�6T   $	��Top=�6h   �� 5$   
   -   5        $ lblImie��5$      $��Imię:��6!   $
��Left=�61   $��
394,57,0,0q�6L   $	��Top=�6`   �� 5%   
   -   5        $ 	btnAnuluj��5%      $��Anuluj��6$   $
��Left=�65   $��626,356,0,0q�6P   $	��Top=�6e   ���	#FFFF7E7E$��20��6�   $	��107��6�   6�   �� 5&   
   -   5        $ btnWczytajAuta��5&      $ ��Sprawdź dostępność aut��6)   $
��Left=�6J   $��
165,98,0,0q�6e   $	��Top=�6y   6�   5(      5)      