   M S B A M L   `   `   `  ����X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbQ-clr-namespace:RFEMSoftware.Simulation.DesktopRFEMSoftware.Simulation.Desktop  5   	   @    :RFEMSoftware.Simulation.Desktop.Dialogs.ReadDataFileDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:RFEMSoftware.Simulation.Desktop     5   	     � Title$  ReadDataFileDialog��5   	   $	��500��6$   $	��400��61   .��5
      � $	��245��6   $	��370��6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      -   5         �Name$ 
grdColumn1��5   )   $��* �6   6)   .+�5   
   ��  �� Orientation$ Vertical=�6   $��Center=�6,   $��0��6I   .+�5      �� $��Data File Type��6   �� 5      -   5        $��lvDataFileTypes��5      $	��250��6X   ��6.   ��  �� Path$ ProgramTypes�6X   5      � 5   
   $��1��6    � RowDefinitions 5      �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �� $ Vertical=�6   $��0��60   $��Center=�6=   .+�5      �� $��Disabled=�6   $
��Auto=�6F   ��5      ��  �� ElementName$ 
grdColumn1��.��5      �� -   5        $��
lbFilePath��5      $��30��6P   $
��Auto��6\   ��6.   �� $ FilePath�6\   5      �� 5      -   5        $��btnBrowseFiles��5      $��Browse��6/   $
��Auto��6@   $��Right=�5   6   $��3q�6R   5       �� 5!      $ 
Horizontal=�6   $��Right=�62   $��Bottom=�6N   $��1��6i   $��	0 0 10 10q�5"      .+�5#      �� -   5        $��	btnCancel��5#      $��Cancel��6*   $��5 0 5 5q�6;   $��3q�6L   6X   �� 5$      -   5        $��btnOK��5$      $��OK��6&   $��5 0 5 5q�63   $��3q�6D   6P   5%      5&      5'      5)      