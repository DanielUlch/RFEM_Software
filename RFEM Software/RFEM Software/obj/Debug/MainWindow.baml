   M S B A M L   `   `   `  ����X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbc6clr-namespace:RFEMSoftware.Simulation.Desktop.Commands(RFEMSoftware.Simulation.Desktop.Commands  5      Q-clr-namespace:RFEMSoftware.Simulation.DesktopRFEMSoftware.Simulation.Desktop  5      o<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  5      0    *RFEMSoftware.Simulation.Desktop.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      Gself6clr-namespace:RFEMSoftware.Simulation.Desktop.Commands     5      ?local-clr-namespace:RFEMSoftware.Simulation.Desktop     5      Kcc<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls     5      -   5        1  +System.Windows.Controls.Ribbon.RibbonWindow   Name$  mainWin��5         PallateBrush3#��[  5
       � WindowState$ 	maximized=�5       � Icon$ Images/IconWork.ico��5       � Title$ RFEM Software��5      $	��350��6+   $	��650��68    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ ApplicationCommands.Close��5      6=     5   
   -   5        $
 Help��5      5        5   
   -   5        $&  self:CustomCommands.NewHelpClick��5      5      .��5   
   � -   5        $��MainGrid��5       � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $��* �6    �� 5      -   5         �Name$ BottomSplitterRow��5   +   $��0 �6    6+   �� 5       -   5        $ ExpanderRow��5    +   $��0 �6    6+   �� 5!      $
��auto �6    .+�5$      0   *RFEMSoftware.Simulation.Desktop.RFEMRibbon  -   5          Name$ 
mainRibbon��5%      $��0��5$      5%      ]� 5'      $��1��6   ��6%   �� *ActualWidth+	 �� ElementName$	 mainWin��.+�5+      �� -	   5        $��	ButtonBar��5+      
 �� Orientation$
 
Horizontal=�6.   $��right=�6G   $��0 0 10 0q�5,      #��[  5-       � LayoutTransform 5.      �� 5/       �� Angle$ 90Q�6*   .+�51      �� -
   5        $��helpPaneButton��51      $��	Help Pane��63     PallateBrush5#��[ 6G   
�������$��5 0 5 0q�53      54      55      � 57      -   5        $��
parentGrid��57       � IsSharedSizeScope . $
��auto��6G   .+�59      � -   5        $��layer1��59       � ColumnDefinitions 5:      �� 5;      $��* �6/   .+�5=      A� $��0��6&   .+�5>      �� -   5         ��Name$ 
tabControl��5>   %   $��10 0 0 0q�5?   %     styleTabControl#��[ 5A   %   ��5@      �� ��5D      �� 5E   "    �� 
TargetType" ��6(   .�5F   &   ��  �� Property � �� �� Value 6?   ��  �� Path$ MasterTab.CustomHeader��� 5G   &    � �� 6@   �� $ MasterTab.Tab� �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� 5H   &    � ��$ 0 0 0 0q�6@   5I   #   5K      5L      5M      � 5O      -   5        $��layer2��5O      $y�	collapsed=�6)    5P      �� 5Q      �� 5R       z� SharedSizeGroup$ column1��6/   $
��auto �6I   .+�5T      � $��1��6   #��[  5U       5V      �� 5W   "   $
��auto �60   �� 5X   "   .+�5Z      ]� $��0��6(   .+�5[   "   �� -   5        $��helpPanePin��5[   )   $��26��6<   $��right=�5\   %   
�������.��5^   &   �� -   5        $��helpPanePinImage��5^   ,   $��/Images/Pin.png��6D   5`   #   �� 5a   "   $��	Help Pane��6(   $
��Left=�6<   5c      A� 5e      $��1��6*   .+�5f   "   ?   9RFEMSoftware.Simulation.Desktop.CustomControls.RFEMReader  -   5          Name$ 
HelpReader��5f   0   5g      5i      �� 5j      $��5��6'   $��1��61   $
��Left=�6A   #��[  6\   5k      5l      5n      �� 5p      -   5        $��BottomSplitter��5p   :   $��2��6   {�. $��5��6P   $��Stretch=�6[   
������ 6y   � 5t      -   5        $��
BottomGrid��5t      $��3��5u      $y�	Collapsed=�5v      $
��auto��5w       5x      �� 5y      $
��auto �6$   �� 5z      $��* �6$   .+�5|      �� -   5        $��btnBottomCollapser��5|      $��0��5}      $��30��5~      $��30��5      $
��Left=�5�      
������ 
������ ��5�      �� 5�      $��Images/Collapse.png��6    5�      �� 5�      $
��Auto��6   $��1��5�      .��5�      �� .+�5�      �� ��6$   �� $ NumberOfLogicalProcessors� �� StringFormat$. (This computer has {0} logical processors���� 5�      $
 
Horizontal=�6%   $��Center=�6>   .+�5�      �� $
 Vertical=�6)   $��Center=�6@   .+�5�   "   �� $��Queue��6(   �� 5�   "   -   5        $��
lvSimQueue��5�   +   ��5�   '   �� $ SimQueue�$ OneWay=�5�      �� 5�      $
 Vertical=�6)   $��Center=�6@   .+�5�   "   �� $��In Progress��6(   &   System.Windows.Controls.DataGrid  5�   "   -   5        $��dgSimWorkers��5�   +      AutoGenerateColumns .  ��5�   '   �� $ 
SimWorkers�$ OneWay=�   Columns 5�   &   0  *System.Windows.Controls.DataGridTextColumn  5�   *   ,  &System.Windows.Controls.DataGridColumn   Header$ Status��6=      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6M   �� $ State�$ OneWay=�6y     5�   *   $	 Job��6=    .  6J   �� $ CurrentSimBaseName�$ OneWay=�6�   4	  .System.Windows.Controls.DataGridTemplateColumn	  5�   *   $ Progress��6A    .  	  CellTemplate 5�   .   �� 5�   2   .R�5�   6   � $	��160��6B   $��15��5�   >    � Value 5�   >   �� *ProgressPercentage+$ OneWay=�5�   3   5�   +     5�   *   $ Current Operation��6=    .  6X   �� $ CurrentOperation�$ OneWay=�6�     5�   *   $ Details��6=    .  6N   �� $ ProgressDetails�$ OneWay=�6�   5�   #   5�      �� 5�      $
 Vertical=�6)   $��Center=�6@   .+�5�   "   �� $��History��6(   �� 5�   "   -   5        $��lvSimHistory��5�   +   ��6?   �� $ 
SimHistory�5�      �� 5�      -   5        $��btnClearHistory��5�   !   $��0��5�   !   $��30��5�   !   $��30��5�   !   $
��Left=�5�   !   
������ 
������ ��5�      �� 5�   "   $��Images/Broom.png��6(   5�      5�      5�      5�      5�      �� 5�      -   5        $��BottomStatusBar��5�   @   $��4��6   $��Center=�6%   #��[  6W   .@�5�      �� -   5        $��btnBottomExpander��5�      $��30��5�      $��30��5�      
������ 
������ ��5�      �� 5�      $��Images/Expand.png��6   5�      �� 5�      $��Status:��6   �� 5�      -   5          ��Name$  	lblStatus��5�      ��6+   �� $% FeaturedWorker.CurrentOperation�$ OneWay=�� 5�      -   5        ! �Name$! progressBar��5�      $	��160��5�      $��15��5�      $y�Visible=�5�       5�      �� $' !FeaturedWorker.ProgressPercentage�$ OneWay=��� 5�      -   5        $��lblSimDetails��5�      $y�Visible=�6r   ��6-   �� $$ FeaturedWorker.ProgressDetails�$ OneWay=�6r   5�      5�      5�      