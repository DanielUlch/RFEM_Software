   M S B A M L   `   `   `  ���� X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbQ-clr-namespace:RFEMSoftware.Simulation.DesktopRFEMSoftware.Simulation.Desktop  c6clr-namespace:RFEMSoftware.Simulation.Desktop.Commands(RFEMSoftware.Simulation.Desktop.Commands  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem o<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  )    #RFEMSoftware.Simulation.Desktop.App   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ?local-clr-namespace:RFEMSoftware.Simulation.Desktop     Fcom6clr-namespace:RFEMSoftware.Simulation.Desktop.Commands     6sys&clr-namespace:System;assembly=mscorlib     Kcc<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls       �� 	Resources     TBHeight  PallateColor1  PallateColor2  PallateColor3  PallateColor4  PallateColor5  PallateColor6  NeutralColor1  PallateBrush1 �� Color 	 PallateBrush2 
 PallateBrush3  PallateBrush4  PallateBrush5  PallateBrush6  NeutralBrush1  HelpEnabled �� 
TargetType �� Property �� Value � DataContext �� Path �� RelativeSource m� Command	 m� CommandParameter  HelpEnabledFrameworkElement1  +System.Windows.Controls.Ribbon.RibbonWindow+  %System.Windows.Controls.Ribbon.Ribbon  VisiableConverter.  (System.Windows.Controls.Ribbon.RibbonTab
 �� 
TargetType �� Property �� 	Converter1  +System.Windows.Controls.Ribbon.RibbonButton6  0System.Windows.Controls.Ribbon.RibbonSplitButton4  .System.Windows.Controls.Ribbon.RibbonTabHeader �� Triggers P� Property P� Value ]� 
Conditions �� Property �� Value0  *System.Windows.Controls.Ribbon.RibbonGroup � ColumnDefinitions � RowDefinitions �� CornerRadius ]� Thumb��%�  &	        &	      &	      &	 2     &	 E     &	 X     &	 k     &	 ~     &	 �     &		 �     &	
 �     &	 �     &	 �     &	 �     &	 �     &	 �     7  &	 �    '  �    '�� �    7  '�� �    '�� O    '�� �    '�� �    '	� �    '  �    7 
 &	 >    '  C    7  7  '  �    7  '      7  '  G    7 
 '  �    7  '�� �    7  7  7 
 7 
 7 
 'n� �	    7 
 '�� �	    7  X�20Q���	#FFFFA969����	#FFFF8932����	#FFFD740F����	#FFC55400����	#FFA74700����	#FF471E00����	White���� # �  �� # �  �� # �  �� # �  �� # �  �� # �  �� # �  �� " ��.���  �@�ContextMenu ��  �� $ PlacementTarget� �� *Self+.@�m� $
��Help��$% com:CustomCommands.NewHelpClick��	 �� $	 Tag���  � ��$ Center=���  � ��$ 5q���  � ��8   �� " �.���  �@�ContextMenu ��  �� $ PlacementTarget� �� *Self+.@�m� $
��Help��$% com:CustomCommands.NewHelpClick��	 �� $	 Tag���  � ��$ Center=���  � ��$ 5q��� "  .���  � ��$ Segoe UI)��� " ��.���  � ��# �   ��  � ��$ 5q���  � ��$ 50����  � ��$ 12(���  � ��$ Center=���  �@�ContextMenu ��  �� $ PlacementTarget� �� *Self+.@�m� $
��Help��$% com:CustomCommands.NewHelpClick��	 �� $	 Tag���  � ��8   �� " ��.���  � ��$ 22����  � ��$
 Auto����  � ��$ 5q��� " ��.���  � ��$ Center=���  � ��$ 5q��� " ��.���  � ��$ 5q��� " ��.���  � ��$ 5q��� " 	�.���  � ��$ 5q��� "  .���  � ��8   ��  � ��
 �   ���  � �� Z� �� �� "  .���  � ��
 ���� ��  � ��
 ���� ��  � �� �� "
  .R�� 8��  y���  �@ 
IsSelected8  .+��� $��2q��� "  .���  � ��8   ��  � ��$
 Bold"��� "  .���  � ��8   ��  � ��$
 Bold"��� "  .���  � ��8   ��  � ��
 ���� ��  � �� �� "
  .R��� #��z��#��z��#��z��._��� #��z��$��Center=�$��Center=���. ���� *Label+ P�  �@ IsRibbonTabSelected . . ��� # � 	  � ���� # �   � ��]�  ��  �@ IsRibbonTabSelected .  ��  �@J�IsMouseOver . ./��� # � 	  � ���� # �   � ���� "  .���  � ��
 ���� ��  �@ MouseOverBackground
 ���� ��  �@ MouseOverBorderBrush
 ���� ��  � �� �� .R��� 8��  
������ ���� �� " ��.���  � ��8   ��  � �� �� "
 ��.R��  �� $	��178 � �� $��12 ��� $��* ��� $��12 �.+��� $��1��$ 2��$��0q�8�� �� $��0��$��ScrollBar.LineUpCommand��8�� ]� $��
PART_Track��$��1����.  n� $��1q�8��  �� �� $��2��$��ScrollBar.LineDownCommand��8�� �� " n�.���  � ��8   �� " ��.���  � ��8   ��  � ��$ 0q�