   M S B A M L   `   `   `  ���� X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbQ-clr-namespace:RFEMSoftware.Simulation.DesktopRFEMSoftware.Simulation.Desktop  o<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  c6clr-namespace:RFEMSoftware.Simulation.Desktop.Commands(RFEMSoftware.Simulation.Desktop.Commands  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089@    :RFEMSoftware.Simulation.Desktop.CustomControls.RFEMToolTip  �� 
TargetType �� Property �� Value �� 
TargetType � RowDefinitions    Title �� Padding    ToolTipMessage	 �� X2	 �� Orientation
 �� Triggers P� Property P� Value �� 
TargetName �� Path �� Self �� RelativeSource�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     7local-clr-namespace:RFEMSoftware.Simulation.Desktop  Ccc<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls  >com6clr-namespace:RFEMSoftware.Simulation.Desktop.Commands  %  '         �� "    .���  � �� �� "   .R�� 
������� �� $
��auto ��� �� $
��auto ��� $
��auto �.+��� $��
TitleBlock��$
��Bold"�
����  �
�������$
��Auto��#��z $��0���� $��Description��$ 10q�$��WrapWithOverflow=�$	��200��$��1��#��z $
��Left=��� 
���   �$��1��$	 220��$��2���� $	 
Horizontal=�$��3��$
��auto��.+��� $��	HelpImage��$��2q�$��25��$��25��$��/Images/ToolTipHelp.png��$��Center=��� $��WrapWithOverflow=�$	��181��$
��auto��$��Bottom=�$��Italic$�RPFor more help right click and select help, or hover over an element and press F1
 P�  �@  	HasErrors . . ��� $ Description�� �� $) #(Validation.Errors)[0].ErrorContent� � ���� $ 
TitleBlock�� � ��
 �  ���� $ 
TitleBlock�� � ��$ Error���� $ 	HelpImage�� � ��$# /Images/ToolTipHelp_Error.png����  �@�DataContext �� $ PlacementTarget�# Z ��  �@  	HasErrors �� $ (Validation.HasError)�