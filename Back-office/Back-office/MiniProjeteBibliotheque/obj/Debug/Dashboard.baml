   M S B A M L   `   `   `  ����R  NMiniProjeteBibliotheque, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MiniProjeteBibliothequeMiniProjeteBibliotheque  5   	   '    !MiniProjeteBibliotheque.Dashboard   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:MiniProjeteBibliotheque     5   	     � Title$  	Dashboard��5   	   $	��450��6   $	��800��6(    � WindowStartupLocation$ CenterScreen=�64   .��5      �� ���#eff2f7 �� CornerRadius$ 15��6"   ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��150 �6#   �� 5      $��* �6#   .+�5      %   MiniProjeteBibliotheque.SideBar  � 5      $��1��6   $��30 20 20 20q�6#    � RowDefinitions 5      �� 5      $
��auto �6$   �� 5      $
��auto �6$   �� 5      $
��auto �6$   �� 5      $
��auto �6$   �� 5      $
��auto �6$   �� 5      $��* �6$   �� 5      $
��auto �6$   .+�5       �� $��	Dashboard��6   $��1��6-   $��32(�6:   $��SemiBold"�6H   $��0 20 0 0q�5!      $
��Left=�6#   ���#1215186>   �� 5#      $��1��6   $��2��6'   ���#dae2ea$��	0 -1 0 10q�6I   �� 5&      $	��150��6   $	��100��6%   $ 15��62   
����p��$��-6,34,406,197q�6^   $��3��6u   $��2��6�   ._�5'      �� -   5         ��Name$ nbrAdr��5'        �� Padding$ 4 25 0 4q�60   $
��Wrap=�6C    �� TextAlignment$ Center=�6W   $��15(�6n   $��SemiBold"�6|   
�������6�   5(      �� 5*      $	��150��6   $	��100��6%   $ 15��62   
����U��$��215,34,215,197q�6^   $��3��6v   $��2��6�   ._�5+      �� -   5        $ nbrEmp��5+       $ 4 25 0 4q�60   $
��Wrap=�6C   $ Center=�6W   $��14(�6n   $��SemiBold"�6|   
�������6�   5,      �� 5.      $	��150��6   $	��100��6%   $ 15��62   
���� ��$��430,34,0,197q�6\   $��3��6r   $��2��6   ._�5/      �� -   5        $ nbrLiv��5/       $ 4 25 0 4q�60   $
��Wrap=�6C   $ Center=�6W   $��15(�6n   $��SemiBold"�6|   
�������6�   50      �� 52      $	��150��6   $	��100��6%   $ 15��62   
���� ��$��215,176,215,55q�6\   $��3��6t   $��2��6�   ._�53      �� -   5        $ nbrRes��53       $ 4 25 0 4q�60   $
��Wrap=�6C   $ Center=�6W   $��14(�6n   $��SemiBold"�6|   
�������6�   54      55      56      57      58      