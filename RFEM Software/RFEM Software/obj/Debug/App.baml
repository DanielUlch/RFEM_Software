   M S B A M L   `   `   `  ����X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbQ-clr-namespace:RFEMSoftware.Simulation.DesktopRFEMSoftware.Simulation.Desktop  5      c6clr-namespace:RFEMSoftware.Simulation.Desktop.Commands(RFEMSoftware.Simulation.Desktop.Commands  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      o<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  5      )    #RFEMSoftware.Simulation.Desktop.App   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ?local-clr-namespace:RFEMSoftware.Simulation.Desktop     5      Fcom6clr-namespace:RFEMSoftware.Simulation.Desktop.Commands     5      6sys&clr-namespace:System;assembly=mscorlib     5      Kcc<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls     5        �� 	Resources  5	         TBHeight  PallateColor1  PallateColor2  PallateColor3  PallateColor4  PallateColor5  PallateColor6  NeutralColor1  PallateBrush1 �� Color 	 PallateBrush2 
 PallateBrush3  PallateBrush4  PallateBrush5  PallateBrush6  NeutralBrush1  HelpEnabled �� 
TargetType �� Property �� Value � DataContext �� Path �� RelativeSource m� Command	 m� CommandParameter  HelpEnabledFrameworkElement1  +System.Windows.Controls.Ribbon.RibbonWindow+  %System.Windows.Controls.Ribbon.Ribbon  VisiableConverter.  (System.Windows.Controls.Ribbon.RibbonTab
 �� 
TargetType �� Property �� 	Converter1  +System.Windows.Controls.Ribbon.RibbonButton6  0System.Windows.Controls.Ribbon.RibbonSplitButton4  .System.Windows.Controls.Ribbon.RibbonTabHeader �� Triggers P� Property P� Value ]� 
Conditions �� Property �� Value0  *System.Windows.Controls.Ribbon.RibbonGroup � ColumnDefinitions � RowDefinitions �� CornerRadius ]� Thumb  styleTabControl��%�  &	        &	      &	 ;     &	 \     &	 }     &	 �     &	 �     &	 �     &	 �     &		     &	
 1    &	 K    &	 e    &	     &	 �    &	 �    7  &	 3    '  �    '�� �    7  '�� �    '�� \    '�� �    '��     '	� Y    '  �    7 
 &	 .	    '  A	    7  7  '  a
    7  '  �
    7  '  5    7 
 '  �    7  '�� %    7  7  7 
 7 
 7 
 'n� �    7 
 &	 �    7  X�5   
   20Q�6+   ��5   
   	#FFFFC6A6��61   ��5   
   	#FFDB7941��61   ��5   
   	#FFBF581F��61   ��5   
   	#FF9C3F0A��61   ��5   
   	#FF8A3200��61   ��5   
   	#FF461C03��61   ��5   
   	White��6-   �� 5   
   # �  60   �� 5   
   # �  60   �� 5   
   # �  60   �� 5   
   # �  60   �� 5   
   # �  60   �� 5   
   # �  60   �� 5   
   # �  60   �� 5!   
   " ��6$   .�5"      ��  �@�ContextMenu 5#      �� 5$       6"   �� $ PlacementTarget� �� *Self+.@�5%      m� $
��Help��6#   $% com:CustomCommands.NewHelpClick��5&   #   	 5'   #   �� $	 Tag�5)      5+      �� 5,       � ��$ Center=�62   �� 5-       � ��$ 5q�6'   �� 5/       � ��8   6+   50      �� 51   
   " �64   .�52      ��  �@�ContextMenu 53      �� 54       6"   �� $ PlacementTarget� �� *Self+.@�55      m� $
��Help��6#   $% com:CustomCommands.NewHelpClick��56   #   	 57   #   �� $	 Tag�59      5;      �� 5<       � ��$ Center=�62   �� 5=       � ��$ 5q�6'   5?      �� 5B   
   "  6   .�5C      ��  � ��$ Segoe UI)�6+   5D      �� 5F   
   " ��6   .�5G      ��  � ��# �   6'   �� 5H       � ��$ 5q�6'   �� 5I       � ��$ 50��6&   �� 5J       � ��$ 12(�6)   �� 5K       � ��$ Center=�62   �� 5L       �@�ContextMenu 5M      �� 5N       6"   �� $ PlacementTarget� �� *Self+.@�5O      m� $
��Help��6#   $% com:CustomCommands.NewHelpClick��5P   #   	 5Q   #   �� $	 Tag�5S      5U      �� 5V       � ��8   6+   5W      �� 5X   
   " ��6   .�5Y      ��  � ��$ 22��6'   �� 5Z       � ��$
 Auto��6&   �� 5[       � ��$ 5q�6'   5\      �� 5]   
   " ��6   .�5^      ��  � ��$ Center=�62   �� 5_       � ��$ 5q�6'   5`      �� 5a   
   " ��6   .�5b      ��  � ��$ 5q�6'   5c      �� 5d   
   " ��6   .�5e      ��  � ��$ 5q�6'   5f      �� 5g   
   " 	�6   .�5h      ��  � ��$ 5q�6'   5i      �� 5o   
   "  6   .�5p      ��  � ��8   6+   �� 5q       � ��
 �   �6+   �� 5r       � �� 6,   Z� 5s      �� 5u   
   6'   �� 5w   
   "  6   .�5x      ��  � ��
 ���� 6+   �� 5y       � ��
 ���� 6,   �� 5z       � �� 5{      �� 5|      "
  6&   .R�5~      � 8��  y�6   ��  �@ 
IsSelected8  .+�5�      �� $��2q�6-   5�      5�      5�      5�      �� 5�   
   "  6   .�5�      ��  � ��8   6+   �� 5�       � ��$
 Bold"�6+   5�      �� 5�   
   "  6   .�5�      ��  � ��8   6+   �� 5�       � ��$
 Bold"�6+   5�      �� 5�   
   "  6   .�5�      ��  � ��8   6+   �� 5�       � ��
 ���� 6,   �� 5�       � �� 5�      �� 5�      "
  6&   .R�5�      �� #��z��6!   #��z��6K   #��z��5�      ._�5�      �� #��z��6/   $��Center=�6R   $��Center=�6o   ��. ��5�      �� *Label+6(   5�       5�      P� 5�       �@ IsRibbonTabSelected . . �5�   "   �� # � 	 6?    � ���� 5�   "   # �  6@    � ��5�      ]� 5�       5�   "   �� 5�   &    �@ IsRibbonTabSelected .  6O   �� 5�   &    �@J�IsMouseOver . 6G   ./�5�   "   �� # � 	 6?    � ���� 5�   "   # �  6@    � ��5�      5�      5�      5�      �� 5�   
   "  6   .�5�      ��  � ��
 ���� 6+   �� 5�       �@ MouseOverBackground
 ���� 64   �� 5�       �@ MouseOverBorderBrush
 ���� 65   �� 5�       � �� 5�      �� 5�      .R�5�      �� 8��  
������ ��6i   �� 5�      5�      5�      �� 5�   
   " ��6   .�5�      ��  � ��8   6+   �� 5�       � �� 5�      �� 5�      "
 ��6&   .R�5�      �  5�      �� 5�   "   $	��178 �63    5�      �� 5�   "   $��12 �60   �� 5�   "   $��* �60   �� 5�   "   $��12 �60   .+�5�      �� $��1��6%   $ 2��62   $��0q�6D   8�� 6X   �� 5�      $��0��6+   $��ScrollBar.LineUpCommand��68   8�� 6[   ]� 5�      $��
PART_Track��61   $��1��6$   ��.  5�   "   n� 5�   &   $��1q�6,   8��  6@   �� 5�   '   5�      �� 5�      $��2��6+   $��ScrollBar.LineDownCommand��68   8�� 6\   5�      5�      5�      5�      �� 5�   
   " n�6   .�5�      ��  � ��8   6+   5�      �� 5�   
   " ��6   .�5�      ��  � ��8   6+   �� 5�       � ��$ 0q�6(   5�      5�      5�      