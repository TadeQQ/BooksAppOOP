   M S B A M L   `   `   `  ����C  ?BooksApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:BooksAppBooksApp  5   	   %    BooksApp.EdytujKsiążkęWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:BooksApp     5   	     � Title$  Edytuj książkę��5   	   $	��450��6    $	��800��6-   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Edytuj książkę��6   $��24(�6*   $
��Bold"�68   $��10q�6J   �� 5   
   $��1��6   $��10q�6"   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0 0 0 10q�62   .+�5      �� $��Tytuł:��6   $	��100��6*   $��Center=�66   �� 5      -   5         ��Name$ tytułTextBox��5      $	��300��60   5      �� 5      $ 
Horizontal=�6   $��0 0 0 10q�62   .+�5      �� $��Imię autora:��6   $	��100��60   $��Center=�6<   �� 5      -   5        $ imieAutoraTextBox��5      $	��300��65   5      �� 5      $ 
Horizontal=�6   $��0 0 0 10q�62   .+�5      �� $��Nazwisko autora:��6   $	��100��64   $��Center=�6@   �� 5      -   5        $ nazwiskoAutoraTextBox��5      $	��300��69   5      �� 5!      $ 
Horizontal=�6   $��0 0 0 10q�62   .+�5"      �� $��Wydawnictwo:��6   $	��100��60   $��Center=�6<   �� 5#      -   5        $ wydawnictwoTextBox��5#      $	��300��66   5$      �� 5&      $ 
Horizontal=�6   $��0 0 0 10q�62   .+�5'      �� $��
Kategoria:��6   $	��100��6.   $��Center=�6:   �� 5(      -   5        $ kategoriaTextBox��5(      $	��300��64   5)      �� 5+      -   5        $��Zapisz��5+      $	��100��6&   $��Center=�62   $
��0 20q�6j   5,      5-      5.      