   M S B A M L   `   `   `  ���� X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbc6clr-namespace:RFEMSoftware.Simulation.Desktop.Commands(RFEMSoftware.Simulation.Desktop.Commands  Q-clr-namespace:RFEMSoftware.Simulation.DesktopRFEMSoftware.Simulation.Desktop  o<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  0    *RFEMSoftware.Simulation.Desktop.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Gself6clr-namespace:RFEMSoftware.Simulation.Desktop.Commands     ?local-clr-namespace:RFEMSoftware.Simulation.Desktop     Kcc<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls     -   1  +System.Windows.Controls.Ribbon.RibbonWindow   Name$  mainWin��   PallateBrush3#��[   � WindowState$ 	maximized=� � Icon$ Images/IconWork.ico�� � Title$ RFEM Software��$	��350��$	��650�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$ ApplicationCommands.Close��  -   $
 Help��  -   $&  self:CustomCommands.NewHelpClick��.��� -   $��MainGrid�� � RowDefinitions �� $
��auto ��� $��* ��� -    �Name$ BottomSplitterRow��$��0 ��� -   $ ExpanderRow��$��0 ��� $
��auto �.+�0   *RFEMSoftware.Simulation.Desktop.RFEMRibbon  -     Name$ 
mainRibbon��$��0��]� $��1������ *ActualWidth+	 �� ElementName$	 mainWin��.+��� -	   $��	ButtonBar��
 �� Orientation$
 
Horizontal=�$��right=�$��0 0 10 0q�#��[   � LayoutTransform ��  �� Angle$ 90Q�.+��� -
   $��helpPaneButton��$��	Help Pane��  PallateBrush5#��[ 
�������$��5 0 5 0q�� -   $��
parentGrid�� � IsSharedSizeScope . $
��auto��.+�� -   $��layer1�� � ColumnDefinitions �� $��* �.+�A� $��0��.+��� -    ��Name$ 
tabControl��$��10 0 0 0q����� � -   $��layer2��$y�	collapsed=� �� ��  z� SharedSizeGroup$ column1��$
��auto �.+�� $��1��#��[   �� $
��auto ��� .+�]� $��0��.+��� -   $��helpPanePin��$��26��$��right=�
�������.���� -   $��helpPanePinImage��$��/Images/Pin.png���� $��	Help Pane��$
��Left=�A� $��1��.+�?   9RFEMSoftware.Simulation.Desktop.CustomControls.RFEMReader  -     Name$ 
HelpReader���� $��5��$��1��$
��Left=�#��[  �� -   $��BottomSplitter��$��2��{�. $��5��$��Stretch=�
������ � -   $��
BottomGrid��$��3��$y�	Collapsed=�$
��auto�� �� $
��auto ��� $��* �.+��� -   $��btnBottomCollapser��$��0��$��30��$��30��$
��Left=�
������ 
������ ���� $��Images/Collapse.png���� $
��Auto��$��1��.���� .+��� ����  �� Path$ NumberOfLogicalProcessors� �� StringFormat$. (This computer has {0} logical processors���� $
 
Horizontal=�$��Center=�.+��� $
 Vertical=�$��Center=�.+��� $��Queue���� -   $��
lvSimQueue������ $ SimQueue� �� Mode$ OneWay=��� $
 Vertical=�$��Center=�.+��� $��In Progress��&   System.Windows.Controls.DataGrid  -   $��dgSimWorkers��   AutoGenerateColumns .  ���� $ 
SimWorkers�$ OneWay=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Status��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� $ State�$ OneWay=�  $	 Job�� .  �� $ CurrentSimBaseName�$ OneWay=�4	  .System.Windows.Controls.DataGridTemplateColumn	  $ Progress�� .  	  CellTemplate �� .R�� $	��160��$��15�� � Value �� *ProgressPercentage+$ OneWay=�  $ Current Operation�� .  �� $ CurrentOperation�$ OneWay=�  $ Details�� .  �� $ ProgressDetails�$ OneWay=��� $
 Vertical=�$��Center=�.+��� $��History���� -   $��lvSimHistory������ $ 
SimHistory��� -   $��BottomStatusBar��$��4��$��Center=�#��[  .@��� -   $��btnBottomExpander��$��30��$��30��
������ 
������ ���� $��Images/Expand.png���� $��Status:���� -    ��Name$ 	lblStatus������ $% FeaturedWorker.CurrentOperation�$ OneWay=�� -    �Name$ progressBar��$	��160��$��15��$y�Visible=� �� $' !FeaturedWorker.ProgressPercentage�$ OneWay=��� -   $��lblSimDetails��$y�Visible=����� $$ FeaturedWorker.ProgressDetails�$ OneWay=�