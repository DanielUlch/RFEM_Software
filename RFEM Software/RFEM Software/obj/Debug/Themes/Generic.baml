   M S B A M L   `   `   `  ����X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbQ-clr-namespace:RFEMSoftware.Simulation.DesktopRFEMSoftware.Simulation.Desktop  5      o<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  5      c6clr-namespace:RFEMSoftware.Simulation.Desktop.Commands(RFEMSoftware.Simulation.Desktop.Commands  5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089@    :RFEMSoftware.Simulation.Desktop.CustomControls.RFEMToolTip  �� 
TargetType �� Property �� Value �� 
TargetType � RowDefinitions    Title �� Padding    ToolTipMessage	 �� X2	 �� Orientation
 �� Triggers P� Property P� Value �� 
TargetName �� Path �� Self �� RelativeSource�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      7local-clr-namespace:RFEMSoftware.Simulation.Desktop  5      Ccc<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls  5      >com6clr-namespace:RFEMSoftware.Simulation.Desktop.Commands  5      %H  '         �� 5	      "    6   .�5
   
   ��  � �� 5      �� 5      "   6"   .R�5      � 
������� 5      �� 5      $
��auto �6,   �� 5      �� 5      $
��auto �6,   �� 5      $
��auto �6,   .+�5      �� $��
TitleBlock��6    $
��Bold"�5       
����  �
�������$
��Auto��5       #��z 5       $��0��5       �� 5      $��Description��6$   $ 10q�5   $   $��WrapWithOverflow=�5   $   $	��200��5    $   $��1��5!   $   #��z 5"   $   $
��Left=�5#   $   �� 5$      
���   �$��1��5%      $	 220��5&      $��2��5'      �� 5*      $	 
Horizontal=�6%   $��3��6>   $
��auto��6K   .+�5+      �� $��	HelpImage��6$   $��2q�5,   $   $��25��5-   $   $��25��5.   $   $��/Images/ToolTipHelp.png��5/   $   $��Center=�50   $   �� 51      $��WrapWithOverflow=�6(   $	��181��52   (   $
��auto��53   (   $��Bottom=�54   (   $��Italic$�55   (   RPFor more help right click and select help, or hover over an element and press F16�   56      57      
 59      P� 5:       �@  	HasErrors . . �5;      �� $ Description��6%    6N   �� $) #(Validation.Errors)[0].ErrorContent� � ���� 5<      $ 
TitleBlock��6%    � ��
 �  ��6S   �� 5=      $ 
TitleBlock��6%    � ��$ Error��6P   �� 5>      $ 	HelpImage��6%    � ��$# /Images/ToolTipHelp_Error.png��6N   5?      5A      5C      �� 5D   
    �@�DataContext 6(   �� $ PlacementTarget�# Z �� 5E   
    �@  	HasErrors 6&   �� $ (Validation.HasError)�5F      5N      