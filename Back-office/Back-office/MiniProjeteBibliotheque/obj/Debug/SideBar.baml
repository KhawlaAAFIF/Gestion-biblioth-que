   M S B A M L   `   `   `  ����R  NMiniProjeteBibliotheque, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MiniProjeteBibliothequeMiniProjeteBibliotheque  5      %    MiniProjeteBibliotheque.SideBar   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5      3local%clr-namespace:MiniProjeteBibliotheque   5      .��5      �   � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��1��6   ���#800080 �� CornerRadius$ 0 50 0 0��63   $��	0,0,-13,0q�6K   ._�5      ]� .+�5      �� $��0 50 0 0q�6   .+�5      �� -   5         ��Name$ 	btn_adher��5         
menuButton#��[  60   $
��Left=�6T   .��5      ��  �� Orientation$ 
Horizontal=�6%   .+�5      .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ Human=�64     menuButtonIcon#��[ 6A   �� 5      $��
adhérents��6(   5       5!      �� 5#      -   5        $ btn_emp��5#      #��[  6.   $
��Left=�6R   .��5$      �� $ 
Horizontal=�6%   .+�5%        $ AccountMultipleOutline=�64   #��[ 6R   �� 5&      $��	Employés��6(   5'      5(      �� 5*      -   5        $ 
btn_livres��5*      #��[  61   $
��Left=�6n   .��5+      �� $ 
Horizontal=�6%   .+�5,        $
 Book=�64   #��[ 6@   �� 5-      $��Livres��6(   5.      5/      �� 52      -   5        $ 
btn_reserv��52      #��[  61   $
��Left=�6U   .��53      �� $ 
Horizontal=�6%   .+�54        $ Calendar=�64   #��[ 6D   �� 55      $��Reservations��6(   56      57      �� 58      -   5        $ 
btn_logout��58      #��[  61   $��	7,200,7,3q�6U    J� RenderTransformOrigin$ 
0.459,0.58&�6h   $
��Left=�6�   .��59      �� $ 
Horizontal=�6%   .+�5:        $ Logout=�64   #��[ 6B   �� 5;      $��DECONNEXION��6(   5<      5=      5>      5F      5H      5I      5M      