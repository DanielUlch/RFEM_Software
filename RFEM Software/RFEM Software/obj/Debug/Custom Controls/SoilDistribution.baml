   M S B A M L   `   `   `  ����X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbo<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  5      ]3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms%RFEMSoftware.Simulation.Desktop.Forms  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      X TInfrastructureLib, Version=0.1.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbzOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib&RFEMSoftware.Simulation.Infrastructure 5	      E    ?RFEMSoftware.Simulation.Desktop.CustomControls.SoilDistribution   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Nlocal<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls     5      Dform3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms     5      6sys&clr-namespace:System;assembly=mscorlib     5      ainfraOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib     5	      -   5        $��
DistParent��5        � 	Resources  5       �� 
TargetType   HelpEnabled �� BasedOn �� Property �� Value �� ElementName �� Path  RegularDistributionEnum S� 
MethodName S� 
ObjectType  System.Enum	 S� MethodParameters
 M� Type9  3RFEMSoftware.Simulation.Infrastructure.Distribution  FrictionAngleEnum:  4RFEMSoftware.Simulation.Infrastructure.FrictionAngle;   5RFEMSoftware.Simulation.Desktop.Forms.DoubleConverter  DoubleConverterC   =RFEMSoftware.Simulation.Desktop.Forms.EnumToUIStringConverter  UIStringConverter��%  '��       7   '�� x     7   &	 �     &	 K    &	 �    &	 �    �� 5   
   " ��6   8   .�5      ��  �@�Tag 6$   �� $ 
DistParent��$ DistributionTag�5      �� 5   
   " ��6   8   .�5      ��  �@�Tag 6$   �� $ 
DistParent��$ DistributionTag�5      S� 5   
   $ 	GetValues��6=   "  6T   	 5      M� 5      "
  6   5      S� 5   
   $ 	GetValues��67   "  6N   	 5      M� 5      "
  6   5         5"   
   6     5#   
   6'   5%      .��5&      � .+�5'   
   ��  �� Orientation$ Vertical=�6   .+�5)      �� $ 
Horizontal=�6   .+�5+      �� -   5        $��lbDist��5+      ��5,      �� $ 
DistParent��$ DistributionLabel�5.      �� 5/      -   5        $��cboDist��5/      ��50      ��  �� Source# [  �� SelectedValue 51      �� $ 
DistParent��$# Distribution.DistributionType� �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=���53      �� 54      .R�55      �� ��6(   ��  �� 	Converter# [ 56      58      �� 5:      $ 
Horizontal=�6   ��5;      �� 5<      " ��6    .�5=      ��  � y�$ 	Collapsed=�6;    �� Triggers 5>      �� 5?   "    �� Binding 6.   �� $ 
DistParent��$ IsFrictionAngle� �� Value True6o   .a�5@   &   ��  � y�$ Visible=�6C   5A   #   5C      .+�5F      �� -   5        $��lbFrictionAngleType��5F      $$��Selected distribution governs:��5G      �� 5H      -   5        $��cboFrictionAngleType��5H      ��5I      �� # [  5J      �� $ 
DistParent��$ FrictionAngleType�$ PropertyChanged=�$ TwoWay=���5L      �� 5M      .R�5N   "   �� ��6,   �� # [ 5O      5Q      5R      5T      �� 5V      $ Vertical=�6   $��30 0 0 0q�60   .+�5X      �� $ 
Horizontal=�6   .+�5Z      �� $ 
Horizontal=�6!   ��5[      �� 5\      " ��6$   .�5]   "   ��  � y�$ Visible=�6?    5^   "   �� 5_   &      Bounded# Z 5`   .    5_   2   �� $ cboDist��$ SelectedValue�.a�5a   *   ��  � y�$ 	Collapsed=�6G   5b   '   5d      .+�5g      �� -   5        $��lbMean��5g       $��Mean:��5h       �� 5i      -   5        $��txtMean��5i   "   ��5j   "   �� $ 
DistParent��$ Distribution.Mean�$ PropertyChanged=� �� ValidatesOnDataErrors . # [ 
 � Tag 5o   "   �� $ 
DistParent��$ DistributionTag�5p      �� 5q      $ 
Horizontal=�6!   ��5r      �� 5s      " ��6$   .�5t   "   ��  � y�$ Visible=�6?    5u   "   �� 5v   &      Deterministic# Z 5w   .    5v   2   �� $ cboDist��$ SelectedValue�.a�5x   *   ��  � y�$ 	Collapsed=�6G   5y   '   �� 5z   &   # Z 5{   .    5z   2   �� $ cboDist��$ SelectedValue�.a�5|   *   ��  � y�$ 	Collapsed=�6G   5}   '   5      .+�5�      �� -   5        $��lbStdDev��5�       $��Standard deviation:��5�       �� 5�      -	   5        $��	txtStdDev��5�   "   ��5�   "   �� $ 
DistParent��$$ Distribution.StandardDeviation�$ PropertyChanged=� . # [  5�   "   �� $ 
DistParent��$ DistributionTag�5�      5�      �� 5�      $ 
Horizontal=�6   ��5�      �� 5�      " ��6    .�5�      ��  � y�$ Visible=�6;    5�      �� 5�   "   # Z 5�   .    5�   .   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   �� 5�   "      Normal# Z 5�   .    5�   .   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   �� 5�   "      	Lognormal# Z 5�   .    5�   .   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   5�      .+�5�      �� -
   5        $��lbLowerBound��5�      $��Lower bound:��5�      �� 5�      -   5        $��txtLowerBound��5�      ��5�      �� $ 
DistParent��$ Distribution.LowerBound�$ PropertyChanged=� . # [  5�      �� $ 
DistParent��$ DistributionTag��� 5�      -   5        $��lbUpperBound��5�      $��Upper bound:��5�      �� 5�      -   5        $��txtUpperBound��5�      ��5�      �� $ 
DistParent��$ Distribution.UpperBound�$ PropertyChanged=� . # [  5�      �� $ 
DistParent��$ DistributionTag�5�      �� 5�      $ 
Horizontal=�6   ��5�      �� 5�      " ��6    .�5�      ��  � y�$ Visible=�6;    5�      �� 5�   "   # Z 5�   .    5�   .   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   �� 5�   "   # Z 5�   .    5�   .   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   �� 5�   "   # Z 5�   .    5�   .   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   5�      .+�5�      �� -   5        $��
lbLocation��5�      $��	Location:��5�      �� 5�      -   5        $��txtLocation��5�      ��5�      �� $ 
DistParent��$ Distribution.Location�$ PropertyChanged=� . # [  5�      �� $ 
DistParent��$ DistributionTag��� 5�      -   5        $��lbScale��5�      $��Scale:��5�      �� 5�      -   5        $��txtScale��5�      ��5�      �� $ 
DistParent��$ Distribution.Scale�$ PropertyChanged=� . # [  5�      �� $ 
DistParent��$ DistributionTag�5�      5�      5�      5�      5�      