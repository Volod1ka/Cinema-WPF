   M S B A M L   `   `   `  ����F  BCinemaAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:CinemaAdmin.WindowsCinemaAdmin.Windows  5   	   '    !CinemaAdmin.Windows.SessionWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:CinemaAdmin.Windows     5   	   -   5          � Title$   ��5   	   $	��400��6   $	��360��6   $	��400��6+   $	��360��6;   $��14(�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   
������  J� SnapsToDevicePixels .  � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � AllowsTransparency . $��6q�5   	   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      CaptionHeight$ 0Q�6      ResizeBorderThickness$ 0q�6)   .��5      �� 	 �� CornerRadius$	 	0,0,10,10��6      
Background#��[  6&   ._�5   
   � .+�5      ��   ShadowColor#��[ 6   $��4��6<   $	��Top=�6G   $��0,40,0,0q�6_   � 5      -   5        
 �Name$
 GridTitleBar��5      $��40��6)     MainBackground#��[ 65   $	��Top=�6b   .+�5      �� -   5         ��Name$ 
ButtonExit��5      $	��✕��6-     ButtonTitleStyle#��[ 69   6c   5      �� 5        LabelStyle1#��[ 6   $��Назва зали��69   $��	40,80,0,0q�6N   $
��Left=�6a   $	��120��6|   $	��Top=�6�   �� 5       -   5         ��Name$ ComboBoxHall��5       $��30��6-   $��181,80,40,0q�69   $	��Top=�6N   6f   �� 5!      #��[ 6   $��Назва фільму��69   $��
40,115,0,0q�6P   $	��Top=�6d   $
��Left=�6|   $	��120��6�   �� 5"      -   5        $ ComboBoxFilm��5"      $��30��6-   $��181,115,40,0q�69   $	��Top=�6O   6g   �� 5#      #��[ 6   $��День сеансу��69   $��
40,150,0,0q�6O   $	��Top=�6c   $
��Left=�6{   $	��120��6�   �� 5$      -   5        $ ComboBoxDay��5$      $��30��6,   $��181,150,40,0q�68   $	��Top=�6N   6f   �� 5%      #��[ 6   $��Час сеансу��69   $��
40,184,0,0q�6N   $	��Top=�6b   $
��Left=�6z   $	��120��6�   �� 5&      -   5        $ ComboBoxHour��5&      $��30��6-   $��0,185,152,0q�69   $	��Top=�6N   $��Right=�6f   $��44��6�   6�   �� 5'      #��[ 6   $��:��69   $��0,185,140,0q�6E   $	��Top=�6Z   $��Right=�6r   $��12��6�   �� 5(      -   5        $ ComboBoxMinute��5(      $��30��6/   $��
0,185,96,0q�6;   $	��Top=�6O   $��Right=�6g   $��44��6�   6�   �� 5)      #��[ 6   $��:��69   $��
0,185,84,0q�6E   $	��Top=�6Y   $��Right=�6q   $��12��6�   �� 5*      -	   5        $ ComboBoxSecond��5*      $��30��6/   $��
0,185,40,0q�6;   $	��Top=�6O   $��Right=�6g   $��44��6�   6�   �� 5+      #��[ 6   $��Вартість��69   $��
40,220,0,0q�6L   $	��Top=�6`   $
��Left=�6x   $	��120��6�   �� 5,      -
   5         ��Name$ TextBoxPrice��5,        TextBoxInputStyle#��[ 6,   $��181,220,40,0q�6W   $	��Top=�6m   $
��Auto��6�    �� 	MaxLength$ 50��6�   6�   �� 5.      -   5        $ 
ButtonSave��5.        ButtonStyle#��[ 6)   $��Зберегти��6N   $��181,0,40,86q�6a   $��Bottom=�6v   {�.  6�   5/      50      51      